using Utils;

// Task input
string input = InputLoader.LoadInput();

// Part One
Func<string, string, int> shapeToScore = (string a, string b) =>
{
    return char.Parse(a) - char.Parse(b);
};

Func<string, int>  roundScoreCal = (token) =>
{
    var shapes = token.Split(' ');
    int score = (shapeToScore(shapes[0], "A") + 1) % 3 == shapeToScore(shapes[1], "X") ? 6 : shapeToScore(shapes[0], "A") == shapeToScore(shapes[1], "X") ? 3 : 0;
    return score + shapeToScore(shapes[1], "X") + 1;
};

var tokens = input.Split('\n').SkipLast(1).Select(x => roundScoreCal(x)).Sum();

Console.WriteLine($"Part One answer: {tokens}");
