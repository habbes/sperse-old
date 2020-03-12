using System;
using Habbes.Sperse.Compiler;

namespace Habbes.Sperse.SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = Compiler.Compiler.GenerateMethod("Double", 2);
            var quad = Habbes.Sperse.Compiler.Compiler.GenerateMethod("Quadruple", 4);

            Console.WriteLine("Result of double 10 = {0}", db(10));
            Console.WriteLine("Result of quadruple 10 = {0}", quad(10));

            string input = "";
            Console.WriteLine("Enter expression (exit to quit):");
            input = Console.ReadLine();
            while (input != "exit")
            {
                var lexer = new Lexer(input);
                var parse = new Parser(lexer.GetTokenStream());
                try
                {
                    var tree = parse.Parse();
                    Console.WriteLine(tree);
                    var codeGen = new CodeGenerator(tree);
                    var method = codeGen.Compile();
                    Console.WriteLine("Result on x := 2: {0}", method(2));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error {e}");
                }
                
                Console.WriteLine("Enter expression (exit to quit):");
                input = Console.ReadLine();
            }
            
        }
    }
}
