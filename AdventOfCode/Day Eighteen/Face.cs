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
        public static bool operator ==(Face left, Face right) => left.Middle == right.Middle && left.NormalVector == right.NormalVector;
        public static bool operator !=(Face left, Face right) => !(left == right);
        public static double Dist(Face f1, Face f2)
        {
            return Point3D.Dist(f1.Middle, f2.Middle);
        }
    }
}
