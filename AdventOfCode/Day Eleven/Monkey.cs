using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_Eleven
{
    internal class Monkey
    {
        public Queue<ulong> Items { get; }
        public Test Test { get; }
        public Func<ulong, ulong> Operation { get; }
        public Monkey(Queue<ulong> items, Test test, Func<ulong, ulong> operation)
        {
            Items = items;
            Test = test;
            Operation = operation;
        }
        public void CatchItem(ulong item)
        {
            Items.Enqueue(item);
        }
        public bool HasItems()
        {
            return Items.Count > 0;
        }
        public (ulong, int) InspectNextItem(bool ridiculousness = false)
        {
            ulong worryLevel = Operation(Items.Dequeue());
            worryLevel /= ridiculousness ? 1UL : 3UL;
            int testResult = Test.RunTest(worryLevel);
            return (worryLevel, testResult);
        }
    }
}
