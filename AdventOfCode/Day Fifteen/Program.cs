using Utils;

// Task input


string input = InputLoader.LoadInput();

List<((int x, int y) sensor, (int x, int y) beacon)> objects = new();

foreach (var line in input.Split('\n').SkipLast(1))
{
    var tokens = line.Replace(',', ' ').Replace(':', ' ').Replace('=', ' ').Split(' ');
    var coordinates = tokens.Where(x => int.TryParse(x, out _)).Select(x => int.Parse(x)).ToList();
    objects.Add(((coordinates[0], coordinates[1]), (coordinates[2], coordinates[3])));
}

Func<(int x, int y), (int x, int y), int> manhatanDistance = (p1, p2) =>
{
    return Math.Abs(p1.x - p2.x) + Math.Abs(p1.y - p2.y);
};

// Part One

bool IsInRange(int x, int from, int to)
{
    return (x >= from && x <= to);
}

HashSet<int> checkRow(List<((int x, int y) sensor, (int x, int y) beacon)> objects, int goalRow)
{
    HashSet<int> goal = new HashSet<int>();
    foreach (var pair in objects)
    {
        int dist = manhatanDistance(pair.sensor, pair.beacon);
        if (pair.sensor.y + dist > goalRow && pair.sensor.y - dist < goalRow)
        {
            var d = Math.Abs(pair.sensor.y - goalRow);
            int length = 2 * dist + 1 - d * 2;
            int startPos = pair.sensor.x - dist + d;

            
                foreach (var x in Enumerable.Range(startPos, length))
                {
                    goal.Add(x);
                }
            
        }
    }
    return goal;
}

int goalRow = 2_000_000;
HashSet<int> goal = checkRow(objects, goalRow);
int beaconsInRange = objects.Where(x => x.beacon.y == goalRow).Select(x => x.beacon).Distinct().Count();
int result = goal.Count() - beaconsInRange;
Console.WriteLine($"Part One answer: {result}");

// Part Two

List<((int x, int y) sensor, int range)> sensors = new List<((int, int), int)>();
List<(int x, int y)> beacons = new List<(int x, int y)>();

foreach (var obj in objects)
{
    sensors.Add((obj.sensor, manhatanDistance(obj.sensor, obj.beacon)));
    beacons.Add(obj.beacon);
}

int upperBound = 4_000_000;
List<(int, int)> possiblePoints = new List<(int, int)>();
for (int i = 0; i < upperBound; i++)
{
    for (int j = 0; j < upperBound; j++)
    {
        bool addFlag = true;
        foreach (var s in sensors)
        {
            if(s.sensor.x == i && s.sensor.y == j) { addFlag = false; break; }
            if(manhatanDistance((i, j), s.sensor) <= s.range) { addFlag = false; break; }
        }
        foreach (var b in beacons)
        {
            if(b.x == i && b.y == j) { addFlag = false; break; }
        }
        if (addFlag)
            possiblePoints.Add((i, j));

    }
}

Console.WriteLine($"Part Two answer: {result}");
