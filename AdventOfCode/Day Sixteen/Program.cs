using Day_Sixteen;
using Utils;

// Task input

string input = InputLoader.LoadInput();

List<(string name, int rate, List<string> neighbours)> vertexList = new List<(string name, int rate, List<string> neighbours)>();

foreach (var line in input.Split('\n').SkipLast(1))
{
    var tokens = line.Replace('=', ' ').Split(' ');
    List<string> neighbours = new List<string>(tokens.Skip(10).Select(x => x.TrimEnd(',')));
    vertexList.Add((tokens[1], int.Parse(tokens[5].TrimEnd(';')), neighbours));
}

List<(string, int, List<(string, int)>)> compressVertices(List<(string name, int rate, List<string> neighbours)> vertexList, string startVertex)
{
    List<(string name, int rate, List<(string, int)> neighbours)> compactVerList = new List<(string, int, List<(string, int)>)>();
    foreach (var vertex in vertexList)
    {
        if (vertex.rate <= 0 && vertex.name != startVertex) continue;
        List<(string name, int dist)> neighbours = new List<(string name, int dist)>();
        List<(string name, int level)> open = vertex.neighbours.Select((x) => (new string(x), 1)).ToList();
        List<string> closed = new List<string>();
        while (open.Count > 0)
        {
            var current = vertexList.Find(x => x.name == open[0].name);
            if (current.rate > 0)
            {
                neighbours.Add(open[0]);
            }
            else
            {
                foreach (var neighbour in current.neighbours)
                {
                    if (closed.Contains(neighbour)) continue;
                    else if (neighbour != vertex.name)
                    {
                        open.Add((neighbour, open[0].level + 1));
                    }
                }
            }
            closed.Add(open[0].name);
            open.RemoveAt(0);
        }
        compactVerList.Add((vertex.name, vertex.rate, neighbours));
    }
    return compactVerList;
}

// Part One

string startVertex = "AA";
List<(string name, int rate, List<(string, int)> neighbours)> compactVerList = compressVertices(vertexList, startVertex);
DecisionTree tree = new DecisionTree(compactVerList, startVertex, 30);
int result = tree.BestLeafValue;
Console.WriteLine($"Part One answer: {result}");

// Part Two

List<(string, int, List<string>)> RemoveVertices(List<string> path)
{
    List<(string name, int rate, List<string> neighbours)> result = new List<(string, int, List<string>)>();
    foreach (var vertex in vertexList)
    {
        int rate = path.Contains(vertex.name) ? 0 : vertex.rate;
        result.Add((vertex.name, rate, vertex.neighbours));
    }
    return result;
}

DecisionTree myTree = new DecisionTree(compactVerList, startVertex, 26);
int bestSolo = myTree.BestLeafValue;

var remainingVertList = RemoveVertices(myTree.BestPath);
var remainingVertices = compressVertices(remainingVertList, startVertex);

DecisionTree remainingTree = new DecisionTree(remainingVertices, startVertex, 26);
int remainingFlow = remainingTree.BestLeafValue;

void DFS(int minScore, Node n, int cumulativeValue, List<string> path, ref List<(List<string> path, int value)> result)
{
    cumulativeValue += n.Value;
    path.Add(n.Name);
    if (n.Level == 0 || n.Children.Count == 0)
    {
        if (cumulativeValue > minScore)
        {
            result.Add((path, cumulativeValue));
        }
    }
    else
    {
        foreach (var child in n.Children)
        {
            DFS(minScore, child, cumulativeValue, path.Select(x => new string(x)).ToList(), ref result);
        }
    }
}

List<(List<string> path, int value)> paths = new List<(List<string>, int)>();
DFS(remainingFlow, myTree.Root!, 0, new List<string>(), ref paths);

result = 0;
foreach (var p in paths)
{
    remainingVertList = RemoveVertices(p.path);
    remainingVertices = compressVertices(remainingVertList, startVertex);

    var elephantTree = new DecisionTree(remainingVertices, startVertex, 26);
    int sum = p.value + elephantTree.BestLeafValue;
    result = sum > result ? sum : result;
}

Console.WriteLine($"Part Two answer: {result}");
