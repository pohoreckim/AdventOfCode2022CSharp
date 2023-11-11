using Day_Twelve;
using System.Runtime.InteropServices;
using System.Security;
using Utils;

// Task input
string input = InputLoader.LoadInput();

var lines = input.Split('\n');
(int height, int width) = (lines.Length, lines[0].Length);
int[,] map = new int[height, width];
(int x, int y) startCoor = (0, 0);
(int x, int y) endCoor = (0, 0);

int j = 0;
foreach (var line in lines)
{
	for (int i = 0; i < line.Length; i++)
	{
		if (line[i] == 'S')
		{
			startCoor = (j, i);
		}
		if (line[i] == 'E')
		{
			endCoor = (j, i);
		}
		map[j, i] = char.IsLower(line[i]) ? line[i] - 'a' : line[i] == 'S' ? line[i] - 'S' : 'z' - 'a';
	}
	j++;
}

// Part One

int AStart(Node start, Node end)
{
	start.G = 0;
	List<Node> open = new List<Node>() { start };
	List<Node> closed = new List<Node>();
	Node? current = null;

    while (open.Count > 0)
	{
		int minVal = open.Min(x => x.F(end));
        current = open.Where(x => x.F(end) == minVal).First();
		open.Remove(current);
		closed.Add(current);
		if (current.Equals(end))
		{
			end.Parent = current.Parent;
			break;
		}
		else
		{
			List<Node> neighbours = current.GetNeighbours();
			foreach (Node node in neighbours)
			{
				if (!closed.Contains(node))
				{
					if (!open.Contains(node))
					{
						open.Add(node);
					}
					if (node.G > current.G + 1)
					{
						node.G = current.G + 1;
						node.Parent = current;
					}
				}
			}
		}
	}
	return current.Equals(end) ? current.G : -1;
}

Node.InitializeMap(map);
Node start = new(startCoor);
Node end = new(endCoor);

int result = AStart(start, end);

Console.WriteLine($"Part One answer: {result}");

// Part Two

List<Node> path = new List<Node>();

Node current = end;
while (!current.Equals(start)) 
{
	path.Add(current);
	current = current.Parent;
}
path.Add(current);
path.Reverse();

var newStart = path.Where(x => map[x.X, x.Y] == 0);
var heights = path.Select(x => map[x.X, x.Y]).ToList();


Console.WriteLine($"Part Two answer: {result}");



