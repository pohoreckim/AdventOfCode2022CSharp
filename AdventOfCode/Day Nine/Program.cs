using System.ComponentModel;
using System.Security.Cryptography;
using Utils;

// Task input
string input = InputLoader.LoadInput();

Func<(int x, int y), (int x, int y), int> findDistance = (p1, p2) =>
{
    return Math.Abs(p1.x - p2.x) + Math.Abs(p1.y - p2.y);
};

Func<(int x, int y), string, (int, int)> makeMove = (p, direction) =>
{
    switch (direction.ToUpper())
    {
        case "U":
            p.y++;
            break;
        case "D":
            p.y--;
            break;
        case "L":
            p.x--;
            break;
        case "R":
            p.x++;
            break;
        default:
            break;
    }
    return (p.x, p.y);
};

Func<(int x, int y), (int x, int y), (int, int)> moveByVector = (p, v) =>
{
    return (p.x + v.x, p.y + v.y);
};

Func<(int x, int y), (int x, int y), (int, int)> calVector  = (to, from) =>
{
    int x = to.x - from.x;
    int y = to.y - from.y;
    return (x > 0 ? Math.Min(x, 1) : Math.Max(x, -1), y > 0 ? Math.Min(y, 1) : Math.Max(y, -1));
};

(int x, int y) head = (0, 0);
(int x, int y) tail = (0, 0);

HashSet<(int, int)> visited = new HashSet<(int, int)>
{
    tail
};

foreach (var line in input.Split('\n').SkipLast(1))
{
    var tokens = line.Split(' ');
    (string direction, int stepsCount) = (tokens[0], int.Parse(tokens[1]));
    for (int i = 0; i < stepsCount; i++)
    {
        head = makeMove(head, direction);
        int dist = findDistance(head, tail);
        if (dist > 2 || (dist == 2 && (head.x == tail.x || head.y == tail.y)))
        {
            tail = moveByVector(tail, calVector(head, tail));
        }
        visited.Add(tail);
    }
}

int result = visited.Count;
Console.WriteLine($"Part One answear: {result}");

// Part Two

(int x, int y)[] rope = Enumerable.Repeat((0, 0), 10).ToArray();
visited.Clear();

foreach (var line in input.Split('\n').SkipLast(1))
{
    var tokens = line.Split(' ');
    (string direction, int stepsCount) = (tokens[0], int.Parse(tokens[1]));
    for (int i = 0; i < stepsCount; i++)
    {
        rope[0] = makeMove(rope[0], direction);
        for (int j = 1; j < rope.Length; j++) 
        {
            int dist = findDistance(rope[j - 1], rope[j]);
            if (dist > 2 || (dist == 2 && (rope[j - 1].x == rope[j].x || rope[j - 1].y == rope[j].y)))
            {
                rope[j] = moveByVector(rope[j], calVector(rope[j - 1], rope[j]));
            }

        }
        visited.Add(rope[rope.Length - 1]);
    }
}

result = visited.Count;
Console.WriteLine($"Part Two answear: {result}");