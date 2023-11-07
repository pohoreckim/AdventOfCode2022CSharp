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
    forest.GetEnumerable(i, Direction.RIGHT).ToList().Aggregate(-1, checkRow);
    forest.GetEnumerable(i, Direction.LEFT).ToList().Select(x => (rows - 1 - x.Item1, x.Item2, x.Item3)).Aggregate(-1, checkRow);
}

int result = visibleTrees.Cast<bool>().ToList().Where(x => x).Count();
Console.WriteLine($"Part One answear: {result}");

// Part Two

Func<(int row, int col, int val), int> lookLeft = (tree) =>
{
    int dist = 0;
    try
    {
        var firstToLeft = forest.Trees
            .Where((x) => x.row == tree.row && x.column < tree.col)
            .OrderByDescending(x => x.column)
            .First(x => x.value >= tree.val);
        dist = tree.col - firstToLeft.column;
    }
    catch (InvalidOperationException)
    {
        dist = tree.col;
    }
    return dist;
};

Func<(int row, int col, int val), int> lookRight = (tree) =>
{
    int dist = 0;
    try
    {
        var firstToRight = forest.Trees
            .Where((x) => x.row == tree.row && x.column > tree.col)
            .OrderBy(x => x.column)
            .First(x => x.value >= tree.val);
        dist = firstToRight.column - tree.col;
    }
    catch (InvalidOperationException)
    {
        dist = cols - tree.col - 1;
    }
    return dist;
};

Func<(int row, int col, int val), int> lookUp = (tree) =>
{
    int dist = 0;
    try
    {
        var firstToLeft = forest.Trees
            .Where((x) => x.row < tree.row && x.column == tree.col)
            .OrderByDescending(x => x.row)
            .First(x => x.value >= tree.val);
        dist = tree.row - firstToLeft.row;
    }
    catch (InvalidOperationException)
    {
        dist = tree.row;
    }
    return dist;
};

Func<(int row, int col, int val), int> lookDown = (tree) =>
{
    int dist = 0;
    try
    {
        var firstToLeft = forest.Trees
            .Where((x) => x.row > tree.row && x.column == tree.col)
            .OrderBy(x => x.row)
            .First(x => x.value >= tree.val);
        dist = firstToLeft.row - tree.row;
    }
    catch (InvalidOperationException)
    {
        dist = rows - tree.row - 1;
    }
    return dist;
};

result = 0;
var aspiringTrees = forest.Trees.Where((x) => x.row > 0 && x.row < rows - 1 && x.column > 0 && x.column < cols - 1);

foreach (var tree in aspiringTrees)
{
    int score = lookLeft(tree) * lookRight(tree) * lookUp(tree) * lookDown(tree);
    result = score > result ? score : result;
}

Console.WriteLine($"Part Two answear: {result}");
