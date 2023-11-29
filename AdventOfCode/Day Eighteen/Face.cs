using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_Eighteen
{
    internal class Face
    {
        public Point3D Middle { get; private set; }
        public Point3D NormalVector { get; private set; }
        public Face(Point3D middle, Point3D normalVector)
        {
            Middle = middle;
            NormalVector = normalVector;
        }
    }
}
