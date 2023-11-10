using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Day_Eleven
{
    internal class Monkey
    {
        private Queue<ulong> _items;
        private Func<ulong, ulong> _operation;
        public Test Test { get; }
        public Monkey(Queue<ulong> items, Test test, Func<ulong, ulong> operation)
        {
            _items = items;
            Test = test;
            _operation = operation;
        }
        public void CatchItem(ulong item)
        {
            _items.Enqueue(item);
        }
        public bool HasItems()
        {
            return _items.Count > 0;
        }
        public (ulong, int) InspectNextItem(ulong divider, bool moduloDivide = false)
        {
            ulong worryLevel = _operation(_items.Dequeue());
            worryLevel = moduloDivide ? worryLevel % divider : worryLevel / divider;
            int testResult = Test.RunTest(worryLevel);
            return (worryLevel, testResult);
        }
    }
}
