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
        }
    }
}
