using System;
using System.Reflection.Emit;

namespace Habbes.Sperse.Compiler
{
    public class SperseCompiler
    {
        public Delegate Compile(string source)
        {
            var lexer = new Lexer(source);
            var parser = new Parser(lexer.GetTokenStream());
            var codeGen = new CodeGenerator(parser.Parse());
            return codeGen.Compile();
        }
    }
}
