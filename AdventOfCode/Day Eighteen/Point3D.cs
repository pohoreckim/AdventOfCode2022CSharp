using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_Eighteen
{
    internal class Point3D
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }
        public Point3D() => new Point3D(0.0, 0.0, 0.0);
        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public override bool Equals(object? obj)
        {
            return obj is Point3D d &&
                   X == d.X &&
                   Y == d.Y &&
                   Z == d.Z;
        }
        public static bool operator ==(Point3D d1, Point3D d2) => d1.Equals(d2);
        public static bool operator !=(Point3D d1, Point3D d2) { return !(d1 == d2); }
        public static Point3D operator -(Point3D point) => new Point3D(-point.X, -point.Y, -point.Z);  
        public static double DotProduct(Point3D p1, Point3D p2)
        {
            return p1.X * p2.X + p1.Y * p2.Y + p1.Z * p2.Z;
        }
    }
}
