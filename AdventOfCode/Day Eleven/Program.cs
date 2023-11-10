using Day_Eleven;
using System.Numerics;
using Utils;


// Task input
string input = InputLoader.LoadInput();
var monkeyDesc = input.Split("\n\n");

Func<ulong, ulong> functionFactory(string operation, string a)
{
    if (ulong.TryParse(a, out ulong argument))
    {
        switch (operation)
        {
            case "*":
                return (x) =>
                {
                    checked
                    {
                        return x * argument;
                    }
                };
            case "+":
                return (x) =>
                {
                    checked
                    {
                        return x + argument;
                    }
                };
        }
    }
    return (x) =>
    {
        checked
        {
            return x * x;
        }
    };
}

Monkey[] loadPuzzle(string[] monkeyDesc)
{
    int id = 0;
    Monkey[] monkeys = new Monkey[monkeyDesc.Length];
    foreach (var monekyInput in monkeyDesc)
    {
        var lines = monekyInput.Split("\n");
        List<ulong> items = lines[1].Replace(',', ' ').Split(' ').Where(x => ulong.TryParse(x, out _)).Select(x => ulong.Parse(x)).ToList();
        var operationTokens = lines[2].Split(' ');
        var testDivisor = lines[3].Split(' ').Where(x => ulong.TryParse(x, out _)).Select(x => ulong.Parse(x)).ToArray()[0];
        var ifTrue = lines[4].Split(' ').Where(x => int.TryParse(x, out _)).Select(x => int.Parse(x)).ToArray()[0];
        var ifFalse = lines[5].Split(' ').Where((x) => int.TryParse(x, out _)).Select(x => int.Parse(x)).ToArray()[0];
        monkeys[id++] = new Monkey(new Queue<ulong>(items), new Test(testDivisor, ifTrue, ifFalse), functionFactory(operationTokens[6], operationTokens[7]));
    }
    return monkeys;
}

// Part One

Monkey[] monkeys = loadPuzzle(monkeyDesc);
long[] monkeyInspCount = new long[monkeys.Length];
int roundsCount = 20;

for (int i = 0; i < roundsCount; i++)
{
    for (int j = 0; j < monkeys.Length; j++)
    {
        while (monkeys[j].HasItems())
        {
            try
            {
                (ulong item, int nextMonkey) = monkeys[j].InspectNextItem(3);
                monkeys[nextMonkey].CatchItem(item);
                monkeyInspCount[j]++;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

var counts = monkeyInspCount.OrderByDescending(x => x).ToArray();
long result = counts[0] * counts[1];

Console.WriteLine($"Part one: {result}");

// Part Two:

monkeys = loadPuzzle(monkeyDesc);
monkeyInspCount = new long[monkeys.Length];
roundsCount = 10_000;

//mod_all = 1
//    for div_by in [m["div_by"] for m in monkeys.values()]:
//        mod_all *= div_by

ulong moduloAll = 1UL;
moduloAll = monkeys.Aggregate(moduloAll, (x, y) => x *= y.Test.GetDivisor());

for (int i = 0; i < roundsCount; i++)
{
    Console.WriteLine(i);
    for (int j = 0; j < monkeys.Length; j++)
    {
        while (monkeys[j].HasItems())
        {
            try
            {
                (ulong item, int nextMonkey) = monkeys[j].InspectNextItem(moduloAll, true);
                monkeys[nextMonkey].CatchItem(item);
                monkeyInspCount[j]++;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

counts = monkeyInspCount.OrderByDescending(x => x).ToArray();
result = counts[0] * counts[1];

Console.WriteLine($"Part two: {result}");

