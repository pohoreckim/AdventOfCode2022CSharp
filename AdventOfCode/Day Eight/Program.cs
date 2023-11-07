using Utils;
using Day_Eight;
using System.Net.Http.Headers;
using System.Collections.Generic;

// Task input
string input = InputLoader.LoadInput();

var tokens = input.Split('\n').SkipLast(1).ToList();
int rows = tokens.Count;
int cols = tokens[0].Length;
Forest forest = new Forest();

for (int i = 0; i < tokens.Count; i++)
{
    var heights = tokens[i].ToCharArray();
    for (int j = 0; j < tokens[i].Length; j++)
    {
        forest.Trees.Add((i, j, heights[j] - '0'));
    }
}

// Part One

bool[,] visibleTrees = new bool[rows, cols];
Action<int, (int row, int col, int height)> checkLine = (limit, x) =>
{
    visibleTrees[x.row, x.col] = x.height > limit ? true : visibleTrees[x.row, x.col];
};

Func<int, (int index, int height, int lineIndex), int> checkCol = (limit, x) =>
{
    checkLine(limit, (x.index, x.lineIndex, x.height));
    return x.height > limit ? x.height : limit;
};

Func<int, (int index, int height, int lineIndex), int> checkRow = (limit, x) =>
{
    checkLine(limit, (x.lineIndex, x.index, x.height));
    return x.height > limit ? x.height : limit;
};

for (int i = 0; i < cols; i++)
{
    forest.GetEnumerable(i, Direction.DOWN).ToList().Aggregate(-1, checkCol);
    forest.GetEnumerable(i, Direction.UP).ToList().Select(x => (cols - 1 - x.Item1, x.Item2, x.Item3)).Aggregate(-1, checkCol);
}
for (int i = 0; i < rows; i++)
{
    forest.GetEnumerable(i, Direction.LEFT).ToList().Aggregate(-1, checkRow);
    forest.GetEnumerable(i, Direction.RIGHT).ToList().Select(x => (rows - 1 - x.Item1, x.Item2, x.Item3)).Aggregate(-1, checkRow);
}

//foreach (var x in forest.GetEnumerable(0, Direction.LEFT))
//{
//    Console.WriteLine($"X: {x.Item3}, Y: {x.Item1}, Height: {x.Item2}");
//}

//foreach (var x in forest.GetEnumerable(1, Direction.UP))
//{
//    Console.WriteLine($"X: {x.Item1}, Y: {x.Item3}, Height: {x.Item2}");
//}

int result = visibleTrees.Cast<bool>().ToList().Where(x => x).Count();
Console.WriteLine($"Part One answear: {result}");

// Part Two


Console.WriteLine($"Part Two answear: {result}");
