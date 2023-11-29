using Day_Eighteen;
using Utils;

// Task input
string input = InputLoader.LoadInput();

var lines = input.Split('\n').SkipLast(1);

// Part One

Func<(double x, double y, double z), List<Face>> getCubeFaces = (p) =>
{
    double sideLength = 1.0;
    List<Face> faces = new List<Face>();
    (double xMid, double yMid, double zMid) = (p.x + sideLength / 2, p.y + sideLength / 2, p.z + sideLength / 2);
    faces.Add(new Face(new Point3D(xMid, yMid, p.z), new Point3D(0, 0, -1)));
    faces.Add(new Face(new Point3D(xMid, yMid, p.z + sideLength), new Point3D(0, 0, 1)));
    faces.Add(new Face(new Point3D(xMid, p.y, zMid), new Point3D(0, -1, 0)));
    faces.Add(new Face(new Point3D(xMid, p.y + sideLength, zMid), new Point3D(0, 1, 0)));
    faces.Add(new Face(new Point3D(p.x, yMid, zMid), new Point3D(-1, 0, 0)));
    faces.Add(new Face(new Point3D(p.x + sideLength, yMid, zMid), new Point3D(1, 0, 0)));
    return faces;
};

List<Face> blockFaces = new List<Face>();
foreach (var line in lines)
{
    var tokens = line.Split(',').Select(x => int.Parse(x)).ToArray();
    var cubeFaces = getCubeFaces((tokens[0], tokens[1], tokens[2]));
    foreach (var face in cubeFaces)
    {
        int index = blockFaces.FindIndex(x => x.Middle == face.Middle);
        if (index > -1)
        {
            blockFaces.RemoveAt(index);
        }
        else
        {
            blockFaces.Add(face);
        }
    }
}

int result = blockFaces.Count;
Console.WriteLine($"Part One answer: {result}");

// Part Two