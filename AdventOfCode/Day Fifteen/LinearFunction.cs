using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_Fifteen
{
    internal class LinearFunction
    {
        public int A { get; private set; }
        public int B { get; private set; }
        public bool Incresing { get { return A > 0; } }
        public int F(int x)
        {
            return A * x + B;
        }
        public LinearFunction(int a, int b)
        {
            A = a;
            B = b;
        }
        public static (int, int) FindIntersection(LinearFunction f1, LinearFunction f2)
        {
            int x = (f2.B - f1.B) / (f1.A - f2.A);
            return (x, f1.F(x));
        }

        public override bool Equals(object? obj)
        {
            return obj is LinearFunction function &&
                   A == function.A &&
                   B == function.B;
        }
    }
}
