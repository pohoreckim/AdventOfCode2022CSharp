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

string startVertex = "AA";
List<(string name, int rate, List<(string, int)> neighbours)> compactVerList = new List<(string name, int rate, List<(string, int)> neighbours)>();

foreach (var vertex in vertexList)
{
    if (vertex.rate <= 0 && vertex.name != startVertex) continue;
    List<(string name, int dist)> neighbours = new List<(string name, int dist)>();
    List<(string name, int level)> open = vertex.neighbours.Select((x) => (new string(x), 1)).ToList();
    List<string> closed = new List<string> ();
    while(open.Count > 0)
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

// Part One

DecisionTree tree = new DecisionTree(compactVerList, "AA", 30);


int result = tree.LeafValues.Max();
Console.WriteLine($"Part One answer: {result}");

// Part Two