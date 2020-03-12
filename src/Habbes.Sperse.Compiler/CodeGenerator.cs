using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Habbes.Sperse.Compiler
{
    public class CodeGenerator
    {
        private ISyntaxTree root;
        private DynamicMethod compiledMethod;
        private ILGenerator il;
        
        public CodeGenerator(ISyntaxTree root)
        {
            this.root = root;
            compiledMethod = new DynamicMethod("Sample",
                typeof(int),
                new[] {typeof(int)});
            il = compiledMethod.GetILGenerator();
        }

        public Func<int, int> Compile()
        {
            switch (root.Token.Type)
            {
                case TokenType.Identifier:
                    CompileId(root);
                    break;    
                case TokenType.IntConstant:
                    CompileInt(root);
                    break;
                default:
                    throw new Exception("Invalid expression");
            }
            il.Emit(OpCodes.Ret);
            return compiledMethod.CreateDelegate(typeof(Func<int, int>)) as Func<int, int>;
        }

        private void CompileCall(ISyntaxTree tree)
        {
            var callee = GetMethod(tree.Token.Value, tree.Children.Count());
            foreach (var child in tree.Children)
            {
                if (child.Token.Type == TokenType.Identifier)
                {
                    CompileId(child);
                }
                else
                {
                    CompileInt(child);
                }
            }
            il.Emit(OpCodes.Call, callee);
        }

        private void CompileInt(ISyntaxTree tree)
        {
            int value = Convert.ToInt32(tree.Token.Value);
            il.Emit(OpCodes.Ldc_I4, value);
        }

        private void CompileId(ISyntaxTree tree)
        {
            if (tree.Children.Any())
            {
                CompileCall(tree);
                return;
            }

            il.Emit(OpCodes.Ldarg_0);
        }

        private MethodInfo GetMethod(string name, int numArgs)
        {
            var method = typeof(Builtins).GetMethod(name);
            if (method != null)
            {
                return method;
            }

            var nameParts = name.Split('.');
            var typeName = string.Join('.', nameParts.SkipLast(1));
            var methodName = nameParts.TakeLast(1).First();
            var paramTypes = new Type[numArgs];
            for (int i = 0; i < numArgs; i++)
            {
                paramTypes[i] = typeof(int);
            }
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {

                var type = assembly.GetType(typeName);
                if (type != null)
                {
                    method = type.GetMethod(methodName, paramTypes);
                    if (method != null)
                    {
                        return method;
                    }
                }
            }
            
            throw new Exception($"Unknown method {name}");
        }
        
    }
}