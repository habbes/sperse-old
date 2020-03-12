using System;
using Habbes.Sperse.Compiler;

namespace Habbes.Sperse.SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var compiler = new SperseCompiler();

            string input = "";
            Console.WriteLine("Enter expression (exit to quit):");
            input = Console.ReadLine();
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
        
        public int SampleMethod(int x)
        {
            return x * 2;
        }
    }
}
