using Day_Eighteen;
using Utils;

// Task input
string input = InputLoader.LoadInput();

var lines = input.Split('\n').SkipLast(1);

// Part One
List<Face> RemoveDuplicates(List<Cube> cubes)
{
    List<Face> faces = new List<Face>();
    HashSet<Face> duplicates = new HashSet<Face>();
    foreach (var cube in cubes)
    {
        foreach (var face in cube.Faces)
        {
            int index = faces.FindIndex(x => x.Middle == face.Middle);
            if (index > -1)
            {
                faces.RemoveAt(index);
                duplicates.Add(face);
            }
            else
            {
                faces.Add(face);
            }
        }
    }
    return faces;
}

List<Point3D> cubesMiddle = new List<Point3D>();
foreach (var line in lines)
{
    var tokens = line.Split(',').Select(x => int.Parse(x)).ToArray();
    cubesMiddle.Add(new Point3D(tokens[0] + 0.5, tokens[1] + 0.5, tokens[2] + 0.5));
}
List<Face> blockFaces = RemoveDuplicates(cubesMiddle.Select(x => new Cube(x, true)).ToList());


int result = blockFaces.Count;
Console.WriteLine($"Part One answer: {result}");

// Part Two

List<Cube> cubes = new List<Cube>();
foreach (var face in blockFaces)
{
    Point3D middle = face.Middle + 0.5 * face.NormalVector;
    int index = cubes.FindIndex(x => x.Middle == middle);
    Face oppositeFace = new Face(face.Middle, -face.NormalVector);
    if (index > -1)
    {
        cubes[index].AddFace(oppositeFace);
    }
    else
    {
        var cube = new Cube(middle, false);
        cube.AddFace(oppositeFace);
        cubes.Add(cube);
    }
}

int check1 = cubes.Select(x => x.Faces.Count).Sum();

List<Figure> figures = new List<Figure>();
foreach (var cube in cubes)
{
    bool ifAdded = false;
    foreach (var figure in figures)
    {
        if(figure.AddCube(cube))
        {
            ifAdded = true;
        }
    }
    if (!ifAdded)
    {
        figures.Add(new Figure(cube));
    }
}

for (int i = figures.Count - 2; i > 0; i--)
{
    for (int j = figures.Count - 1; j > i; j--)
    {
        if (Figure.CanMerge(figures[i], figures[j]))
        {
            figures[i] = Figure.Merge(figures[i], figures[j]);
            figures.RemoveAt(j);
        }
    }
}

for (int i = figures.Count - 1; i >= 0; i--)
{
    var allFaces = RemoveDuplicates(figures[i].Cubes.Select(x => new Cube(x.Middle, true)).ToList());
    if(allFaces.Count != figures[i].FacesCount)
    {
        figures.RemoveAt(i);
    }
}


result = result - figures.Select(x => x.FacesCount).Sum();
Console.WriteLine($"Part One answer: {result}");