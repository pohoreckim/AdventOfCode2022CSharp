using Utils;

// Task input
string input = InputLoader.LoadInput();

// Part One
Func<string, int> shapeToInt = (string s) =>
{
    char c = char.Parse(s);
    return c > 'D' ? c - 'X' : c - 'A';
};

Func<string, int>  roundScoreCal = (token) =>
{
    var shapes = token.Split(' ');
    int score = (shapeToInt(shapes[0]) + 1) % 3 == shapeToInt(shapes[1]) ? 6 : shapeToInt(shapes[0]) == shapeToInt(shapes[1]) ? 3 : 0;
    return score + shapeToInt(shapes[1]) + 1;
};

var result = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(x => roundScoreCal(x)).Sum();

Console.WriteLine($"Part One answer: {result}");

// Part Two

Func<string, string> translate = (token) =>
{
    var shapes = token.Split(' ');
    char move;
    switch (shapes[1])
    {
        case "X":
            move = (char)((shapeToInt(shapes[0]) + 2) % 3 + 'X');
            break;
        case "Z":
            move = (char)((shapeToInt(shapes[0]) + 1) % 3 + 'X');
            break;
        default:
            move = (char)(shapeToInt(shapes[0]) + 'X');
            break;
    }
    return shapes[0] + ' ' + move;
};

result = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(x => translate(x)).Select(x => roundScoreCal(x)).Sum();

var tmp = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(x => translate(x));

Console.WriteLine($"Part Two answer: {result}");
