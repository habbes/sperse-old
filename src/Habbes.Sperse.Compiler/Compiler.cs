using System;
using System.Reflection.Emit;

namespace Habbes.Sperse.Compiler
{
    public class Compiler
    {
        public static Func<int, int> GenerateMethod(string name, int multiplier)
        {
            var method = new DynamicMethod(
                name,
                typeof(int),
                new[] { typeof(int) });

            var il = method.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldc_I4, multiplier);
            il.Emit(OpCodes.Mul);
            il.Emit(OpCodes.Ret);

            var fn = method.CreateDelegate(typeof(Func<int, int>)) as Func<int, int>;
            return fn;
        }
    }
}
