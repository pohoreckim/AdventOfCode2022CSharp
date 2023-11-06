using Utils;
using Day_Seven;

// Task input
string input = InputLoader.LoadInput();

var lines = input.Split('\n');
Dir currentDir = new("root", null);
Dir root = currentDir;
for (int i = 1; i < lines.Length; i++)
{
    var tokens = lines[i].Split(' ');
    if (tokens[0] == "$")
    {
        if (tokens[1] == "cd")
        {
            if (tokens[2] == "..")
                currentDir = currentDir.ParentDir;
            else
                currentDir = currentDir.Subdirectories.Find(x => x.Name == tokens[2]);
        }
    }
    else if (tokens[0] == "dir")
    {
        currentDir.Subdirectories.Add(new Dir(tokens[1], currentDir));
    }
    else if (int.TryParse(tokens[0], out int value)) currentDir.AddFile(tokens[1], value);
}

// Part One

int size = 100_000;
Func<Dir, int> checkIfSizeExceeds = (Dir dir) =>
{
    return dir.GetMemoryUsage() >= size ? 0 : dir.GetMemoryUsage();
};

int result = root.ApplyFun(checkIfSizeExceeds);

Console.WriteLine($"Part One answear: {result}");

// Part Two

int diskSpace = 70_000_000;
int requiredSpace = 30_000_000;

List<(int size, string anme)> list = new();
root.DirToList(ref list);
list = list.OrderByDescending(x => x.size).ToList();

int spaceToFree = requiredSpace - (diskSpace - list[0].size);
result = list[list.FindLastIndex(x => x.size > spaceToFree)].size;

Console.WriteLine($"Part Two answear: {result}");