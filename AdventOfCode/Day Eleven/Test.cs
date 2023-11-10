using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_Eleven
{
    internal class Test
    {
        private int _divisor;
        private int _ifTrue;
        private int _ifFalse;
        public Test(int divisor, int ifTrue, int ifFalse)
        {
            _divisor = divisor;
            _ifTrue = ifTrue;
            _ifFalse = ifFalse;
        }
        public int RunTest(int value)
        {
            return (value % _divisor) == 0 ? _ifTrue : _ifFalse;
        }
    }
}
