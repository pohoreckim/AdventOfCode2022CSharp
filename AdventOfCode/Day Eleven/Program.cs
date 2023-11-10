using Day_Eleven;
using Utils;


// Task input
string input = InputLoader.LoadInput();
var monkeyDesc = input.Split("\n\n");

Func<int, int> functionFactory(string operation, string a)
{
    if (int.TryParse(a, out int argument))
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
        List<int> items = lines[1].Replace(',', ' ').Split(' ').Where(x => int.TryParse(x, out _)).Select(x => int.Parse(x)).ToList();
        var operationTokens = lines[2].Split(' ');
        var testDivisor = lines[3].Split(' ').Where(x => int.TryParse(x, out _)).Select(x => int.Parse(x)).ToArray()[0];
        var ifTrue = lines[4].Split(' ').Where(x => int.TryParse(x, out _)).Select(x => int.Parse(x)).ToArray()[0];
        var ifFalse = lines[5].Split(' ').Where((x) => int.TryParse(x, out _)).Select(x => int.Parse(x)).ToArray()[0];
        monkeys[id++] = new Monkey(new Queue<int>(items), new Test(testDivisor, ifTrue, ifFalse), functionFactory(operationTokens[6], operationTokens[7]));
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
            (int item, int nextMonkey) = monkeys[j].InspectNextItem();
            monkeys[nextMonkey].CatchItem(item);
            monkeyInspCount[j]++;
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

for (int i = 0; i < roundsCount; i++)
{
    for (int j = 0; j < monkeys.Length; j++)
    {
        while (monkeys[j].HasItems())
        {
            (int item, int nextMonkey) = monkeys[j].InspectNextItem(true);
            monkeys[nextMonkey].CatchItem(item);
            monkeyInspCount[j]++;
        }
    }
}

counts = monkeyInspCount.OrderByDescending(x => x).ToArray();
result = counts[0] * counts[1];

Console.WriteLine($"Part two: {result}");

