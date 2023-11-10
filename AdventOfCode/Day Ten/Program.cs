using Utils;

// Task input
string input = InputLoader.LoadInput();

// Part One

(int currentCycle, int value) = (0, 1);
List<(int cycle, int value)> log = new List<(int, int)>()
{
    (currentCycle, value)
};

foreach (var line in input.Split('\n').SkipLast(1))
{
    var tokens = line.Split(' ');
    if (tokens.Length == 1 && tokens[0].Equals("noop")) { currentCycle++; }
    else if (tokens.Length > 1 && tokens[0].Equals("addx"))
    {
        currentCycle += 2;
        value += int.Parse(tokens[1]);
    }
    log.Add((currentCycle, value));
}

int result = 0;
(int n, int step, int shift) = (6, 40, 20);
foreach (var point in Enumerable.Range(0, n).Select(x => x * step + shift))
{
    result += point * log.FindLast(x => x.cycle < point).value;
}

Console.WriteLine($"Part One answear: {result}");

// Part Two
static IEnumerable<string> SplitText(string text, int length)
{
    for (int i = 0; i < text.Length; i += length)
    {
        yield return text.Substring(i, Math.Min(length, text.Length - i));
    }
}

int cyclesCount = 240;
string message = "";
for (int i = 1; i <= cyclesCount; i++)
{
    int spritePos = log.FindLast(x => x.cycle < i).value;
    if (((i - 1) % step) == spritePos || Math.Abs(spritePos - ((i - 1) % step)) == 1)
    {
        message += "#";
    }
    else message += ".";
}

Console.WriteLine();
foreach (var line in SplitText(message, step))
{
    Console.WriteLine(line);
}