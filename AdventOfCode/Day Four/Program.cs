using Utils;

// Task input
string input = InputLoader.LoadInput();

// Part One

Func<(int from, int to), (int from, int to), bool> ifFulllyContains = (x, y) =>
{
    return (x.from >= y.from && x.to <= y.to) || (x.from <= y.from && x.to >= y.to);
};

var count = input.Split('\n').SkipLast(1).Select(x =>
{
    var limits = x.Split(",").Select(y => (y.Split('-'))).ToArray();
    return ifFulllyContains((int.Parse(limits[0][0]), int.Parse(limits[0][1])), (int.Parse(limits[1][0]), int.Parse(limits[1][1])));
}).Where(x => x).Count();

Console.WriteLine($"Part One answer: {count}");

// Part Two

Func<(int from, int to), (int from, int to), bool> ifOverlaps = (x, y) =>
{
    return (x.from >= y.from && x.from <= y.to) || (x.to >= y.from && x.to <= y.to) || (y.from >= x.from && y.from <= x.to) || (y.to >= x.from && y.to <= x.to);
};

count = input.Split('\n').SkipLast(1).Select(x =>
{
    var limits = x.Split(",").Select(y => (y.Split('-'))).ToArray();
    return ifOverlaps((int.Parse(limits[0][0]), int.Parse(limits[0][1])), (int.Parse(limits[1][0]), int.Parse(limits[1][1])));
}).Where(x => x).Count();

Console.WriteLine($"Part Two answer: {count}");