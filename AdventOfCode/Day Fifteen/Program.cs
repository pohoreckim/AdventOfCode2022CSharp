using Day_Fifteen;
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

List<LinearFunction> increasingFunctions = new List<LinearFunction>();
List<LinearFunction> decresingFunctions = new List<LinearFunction>();

foreach (var obj in objects)
{
    int dist = manhatanDistance(obj.sensor, obj.beacon) + 1;
    increasingFunctions.Add(new LinearFunction(1, obj.sensor.y - obj.sensor.x + dist));
    increasingFunctions.Add(new LinearFunction(1, obj.sensor.y - obj.sensor.x - dist));
    decresingFunctions.Add(new LinearFunction(-1, obj.sensor.y + obj.sensor.x + dist));
    decresingFunctions.Add(new LinearFunction(-1, obj.sensor.y + obj.sensor.x - dist));
}

List<(int x, int y)> aspiringPoints = new List<(int x, int y)>();

foreach (var iFun in increasingFunctions)
{
    foreach (var dFun in decresingFunctions)
    {
        aspiringPoints.Add(LinearFunction.FindIntersection(iFun, dFun));
    }
}

int lowerBound = 0;
int upperBound = 4_000_000;
aspiringPoints = aspiringPoints.Where(p => p.x >= lowerBound && p.x <= upperBound && p.y >= lowerBound && p.y <= upperBound).ToList();

for (int i = aspiringPoints.Count - 1; i >= 0; i--)
{
    foreach (var obj in objects)
    {
        if (manhatanDistance(aspiringPoints[i], obj.sensor) <= manhatanDistance(obj.sensor, obj.beacon))
        {
            aspiringPoints.RemoveAt(i); 
            break;
        }
    }
}

aspiringPoints = aspiringPoints.Distinct().ToList();

long frequency = aspiringPoints[0].x * (long)upperBound + aspiringPoints[0].y;

Console.WriteLine($"Part Two answer: {frequency}");
