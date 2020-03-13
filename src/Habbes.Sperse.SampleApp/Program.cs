using System;
using Habbes.Sperse.Compiler;

namespace SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var compiler = new SperseCompiler();


            Console.WriteLine("Enter expression (exit to quit):");
            string input = Console.ReadLine();
            while (input != "exit")
            {
                try
                {
                    var method = compiler.Compile(input);
                    Console.WriteLine("Result on x := 2: {0}", method.DynamicInvoke(2));
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
