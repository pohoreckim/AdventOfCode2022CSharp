using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_Eleven
{
    internal class Monkey
    {
        public Queue<int> Items { get; }
        public Test Test { get; }
        public Func<int, int> Operation { get; }
        public Monkey(Queue<int> items, Test test, Func<int, int> operation)
        {
            Items = items;
            Test = test;
            Operation = operation;
        }
        public void CatchItem(int item)
        {
            Items.Enqueue(item);
        }
        public bool HasItems()
        {
            return Items.Count > 0;
        }
        public (int, int) InspectNextItem(bool ridiculousness = false)
        {
            int worryLevel = Operation(Items.Dequeue());
            worryLevel /= ridiculousness ? 1 : 3;
            int testResult = Test.RunTest(worryLevel);
            return (worryLevel, testResult);
        }
    }
}
