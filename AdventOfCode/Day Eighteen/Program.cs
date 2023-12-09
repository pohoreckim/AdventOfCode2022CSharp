using Day_Eighteen;
using System.Threading.Channels;
using Utils;

// Task input
string input = InputLoader.LoadInput();

var lines = input.Split('\n').SkipLast(1);

// Part One
List<Face> RemoveDuplicates(List<Cube> cubes)
{
    List<Face> faces = new List<Face>();
    foreach (var cube in cubes)
    {
        foreach (var face in cube.Faces)
        {
            int index = faces.FindIndex(x => x.Middle == face.Middle);
            if (index > -1)
            {
                faces.RemoveAt(index);

            }
            else
            {
                faces.Add(face);
            }
        }
    }
    return faces;
}

List<Cube> cubes = new List<Cube>();
foreach (var line in lines)
{
    var tokens = line.Split(',').Select(x => int.Parse(x)).ToArray();
    cubes.Add(new Cube(new Point3D(tokens[0] + 0.5, tokens[1] + 0.5, tokens[2] + 0.5), true));
}
List<Face> blockFaces = RemoveDuplicates(cubes);

int result = blockFaces.Count;
Console.WriteLine($"Part One answer: {result}");

// Part Two

List<Side> sides = new List<Side>();
foreach (var face in blockFaces)
{
    Side side = new Side(face);
    for (int i = 0; i < sides.Count; i++)
    {
        if (Face.Dist(sides[i].Face, side.Face) == 1.0 && Point3D.DotProduct(sides[i].Face.NormalVector, side.Face.NormalVector) != -1.0)
        {
            sides[i].AddNeighbour(side);
            side.AddNeighbour(sides[i]);
        }
    }
    sides.Add(side);
}

sides.ForEach(x => x.CleanUp());

List<Side> FloodFill(Side startSide)
{
    List<Side> open = new List<Side>();
    List<Side> closed = new List<Side>();
    open.Add(startSide);
    while(open.Count > 0)
    {
        Side current = open[0];
        closed.Add(current);
        open.RemoveAt(0);
        foreach (var neighbour in current.Neighbours) 
        { 
            if(!closed.Any(x => x.Face == neighbour.Face) && !open.Any(x => x.Face == neighbour.Face))
            {
                open.Add(neighbour);
            }
        }
    }
    return closed;
}

result = 0;
while(sides.Count > 0)
{
    List<Side> connected = FloodFill(sides[0]);
    if(connected.Count > result)
    {
        result = connected.Count;
    }
    foreach (var side in connected)
    {
        sides.Remove(side);
    }
}


Console.WriteLine($"Part One answer: {result}");