using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_Seven
{
    internal class Dir
    {
        public string Name { get; }
        public List<Dir> Subdirectories { get; set; }
        public List<(string name, int size)> Files { get; set; }
        public Dir? ParentDir { get; }
        public Dir(string name, Dir parentDir) : this(name, new List<Dir>(), new List<(string, int)>(), parentDir) { }
        public Dir(string name, List<Dir> subdirectories, List<(string, int)> files, Dir parentDir)
        {
            Subdirectories = subdirectories;
            Files = files;
            Name = name;
            ParentDir = parentDir;
        }
        public void AddDirectory(Dir dir)
        {
            Subdirectories.Add(dir);
        }
        public void AddFile(string name, int size)
        {
            AddFile((name, size));
        }
        public void AddFile((string, int) file)
        {
            Files.Add(file);
        }
        public int GetMemoryUsage()
        { 
            return Subdirectories.Select(x => x.GetMemoryUsage()).Sum() + Files.Select(x => x.size).Sum();
        }
        public int ApplyFun(Func<Dir, int> fun)
        {
            return fun(this) + Subdirectories.Select(x => x.ApplyFun(fun)).Sum();
        }
        public void DirToList(ref List<(int, string)> result)
        {
            result.Add((GetMemoryUsage(), Name));
            foreach (var subDir in Subdirectories)
            {
                subDir.DirToList(ref result);
            }
        }
    }
}
