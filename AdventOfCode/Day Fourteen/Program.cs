using Day_Fourteen;
using Utils;

// Task input

string input = InputLoader.LoadInput();
var lines = input.Split('\n').SkipLast(1);
(int minHeight, int minWidth) = (int.MaxValue, int.MaxValue);
(int maxHeight, int maxWidth) = (int.MinValue, int.MinValue);

List<List<(int x, int y)>> rockPaths = new List<List<(int, int)>>();
foreach (var line in lines)
{
	List<(int, int)> path = new List<(int, int)>();
    var coordinates = line.Split("->").Select(x => x.Trim());
	foreach (var coordinate in coordinates)
	{

		var coor = coordinate.Split(',');
		if (int.TryParse(coor[0], out int x) && int.TryParse(coor[1],out int y))
		{
			minHeight = Math.Min(y, minHeight);
			maxHeight = Math.Max(y, maxHeight);
			maxWidth = Math.Max(x, maxWidth);
			minWidth = Math.Min(x, minWidth);

			path.Add((x, y));
		}

	}
	rockPaths.Add(path);
}

// Part One

Scan scan = new Scan(maxWidth - minWidth + 3, maxHeight + 2, minWidth - 1, 0);

foreach (var rockPath in rockPaths)
{
    rockPath.Aggregate((p1, p2) =>
    {
        scan.DrawLine('#', p1, p2);
        return p2;
    });
}

(int x, int y) sandSource = (500, 0);
scan.DrawPoint('+', sandSource.x, sandSource.y);

while (scan.DropSandUnit(sandSource)) { }

// scan.Draw();

int result = scan.SandUnitsCount;
Console.WriteLine($"Part One answer: {result}");

// Part Two

maxHeight = maxHeight + 2;
minWidth = sandSource.x - maxHeight - 1;
scan = new Scan(2 * maxHeight + 3, maxHeight + 1, minWidth, 0);

foreach (var rockPath in rockPaths)
{
    rockPath.Aggregate((p1, p2) =>
    {
        scan.DrawLine('#', p1, p2);
        return p2;
    });
}

scan.DrawPoint('+', sandSource.x, sandSource.y);
scan.DrawLine('#', (minWidth + 1, maxHeight), (minWidth + 2 * maxHeight + 1, maxHeight));

while (scan.DropSandUnit(sandSource)) { }

//scan.Draw();

result = scan.SandUnitsCount;
Console.WriteLine($"Part Two answer: {result}");