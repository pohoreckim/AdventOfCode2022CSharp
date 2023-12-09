using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_Eighteen
{
    internal class Side
    {
        public Face Face { get; private set; }
        public List<Side> Neighbours { get; private set; }
        public Side(Face face)
        {
            Face = face;
            Neighbours = new List<Side>();
        }
        public void AddNeighbour(Side side)
        {
            Neighbours.Add(side);
        }
        public void CleanUp()
        {
            Point3D target = Face.Middle + Face.NormalVector;
            List<int> toRemove = new List<int>();
            List<Point3D> vectors = new List<Point3D>();
            for (int i = 0; i < Neighbours.Count; i++)
            {
                Point3D v = Neighbours[i].Face.Middle - Face.Middle;
                vectors.Add(v.Mask(Face.NormalVector.Neg()));
            }
            for (int i = 0; i < Neighbours.Count; i++)
            {
                for (int j = 0; j < Neighbours.Count; j++)
                {
                    if (i != j && vectors[i] == vectors[j])
                    {
                        if (Point3D.Dist(target, Neighbours[i].Face.Middle) < Point3D.Dist(target, Neighbours[j].Face.Middle))
                        {
                            toRemove.Add(j);
                        }
                        else
                        {
                            toRemove.Add(i);
                        }
                    }
                }
            }
            foreach (var id in toRemove.Distinct().OrderByDescending(x => x))
            {
                Neighbours.RemoveAt(id);
            }
        }
    }
}
