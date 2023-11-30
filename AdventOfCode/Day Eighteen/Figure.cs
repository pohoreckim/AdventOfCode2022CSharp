using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_Eighteen
{
    internal class Figure
    {
        public List<Cube> Cubes { get; private set; }
        public int FacesCount { get { return Cubes.Select(x => x.Faces.Count).Sum();} }
        public Figure()
        {
            Cubes = new List<Cube>();
        }
        public Figure(Cube cube) : this()
        {
            Cubes.Add(cube);
        }
        public Figure(List<Cube> cubes)
        {
            Cubes = cubes;
        }
        public bool AddCube(Cube cube)
        {
            if(CanAddCube(cube))
            {
                Cubes.Add(cube);
                return true;
            }
            return false;
        }
        private bool CanAddCube(Cube cube)
        {
            foreach (Cube c in Cubes)
            {
                if(Cube.Distance(c, cube) == 1.0)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool CanMerge(Figure f1, Figure f2)
        {
            foreach(Cube c1 in f1.Cubes)
            {
                foreach (Cube c2 in f2.Cubes)
                {
                    if(c1 == c2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static Figure Merge(Figure f1, Figure f2)
        {
            HashSet<Cube> cubes = new HashSet<Cube>();
            foreach (var cube in f1.Cubes)
            {
                cubes.Add(cube);
            }
            foreach (var cube in f2.Cubes)
            {
                cubes.Add(cube);
            }
            return new Figure(cubes.ToList());
        }
    }
}
