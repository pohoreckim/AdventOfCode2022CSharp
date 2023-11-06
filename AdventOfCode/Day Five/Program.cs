using Utils;

// Task input
string input = InputLoader.LoadInput();

Func<List<string>, Stack<char>[]> initialLayout = (drawing) =>
{
    int stacksCount = drawing[drawing.Count - 1].Split(' ').Where(x => int.TryParse(x, out _)).Select(x => int.Parse(x)).Max();
    Stack<char>[] stacks = Enumerable.Range(0, stacksCount).Select(x => new Stack<char>()).ToArray();

    for (int i = drawing.Count - 2; i >= 0; i--)
    {
        var line = drawing[i].Split(' ').Select(x => x.Trim(new char[] { '[', ']' }))
            .Aggregate((x, y) => Equals(y, string.Empty) ? x + '$' : x + y).Replace("$$$$", "$").ToCharArray();
        for (int j = 0; j < line.Length; j++)
        {
            if (!line[j].Equals('$'))
                stacks[j].Push(line[j]);
        }
    }
    return stacks;
};

var tokens = input.Split('\n').SkipLast(1).ToList();
int inputSepIndex = tokens.FindIndex(0, x => x == "");

// Part One

var stacks = initialLayout(tokens.Take(inputSepIndex).ToList());

for (int i = inputSepIndex + 1; i < tokens.Count; i++)
{
    var numbers = tokens[i].Split(' ').Where(x => int.TryParse(x, out _)).Select(x => int.Parse(x)).ToArray();
    for (int j = 0; j < numbers[0]; j++)
    {
        char create = stacks[numbers[1] - 1].Pop();
        stacks[numbers[2] - 1].Push(create);
    }
}

Console.WriteLine($"Part One answear: {stacks.Aggregate("", (s, x) => s += x.Peek())}");

// Part Two

stacks = initialLayout(tokens.Take(inputSepIndex).ToList());

for (int i = inputSepIndex + 1; i < tokens.Count; i++)
{
    Stack<char> intermediate = new Stack<char>();
    var numbers = tokens[i].Split(' ').Where(x => int.TryParse(x, out _)).Select(x => int.Parse(x)).ToArray();
    for (int j = 0; j < numbers[0]; j++)
    {
        char create = stacks[numbers[1] - 1].Pop();
        intermediate.Push(create);
    }
    for (int j = 0; j < numbers[0]; j++)
    {
        char create = intermediate.Pop();
        stacks[numbers[2] - 1].Push(create);
    }
}

Console.WriteLine($"Part Two answear: {stacks.Aggregate("", (s, x) => s += x.Peek())}");

