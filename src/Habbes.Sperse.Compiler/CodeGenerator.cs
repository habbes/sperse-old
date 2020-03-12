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
        private DynamicMethod method;
        private ILGenerator il;
        
        public CodeGenerator(ISyntaxTree root)
        {
            this.root = root;
            method = new DynamicMethod("Sample",
                typeof(int),
                new[] {typeof(int)});
            il = method.GetILGenerator();
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
            return method.CreateDelegate(typeof(Func<int, int>)) as Func<int, int>;
        }
        
        private void CompileCall(ISyntaxTree tree)
        {
            var callee = typeof(Builtins).GetMethod(tree.Token.Value);
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
        
        
    }
}