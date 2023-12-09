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
        public Point3D Mask(Point3D p)
        {
            return new Point3D(p.X == 1 ? X : 0, p.Y == 1 ? Y : 0, p.Z == 1 ? Z : 0);
        }
        public Point3D Neg()
        {
            return new Point3D(X != 0 ? 0 : 1, Y != 0 ? 0 : 1, Z != 0 ? 0 : 1);
        }
        public static bool operator ==(Point3D d1, Point3D d2) => d1.Equals(d2);
        public static bool operator !=(Point3D d1, Point3D d2) => !(d1 == d2);
        public static Point3D operator -(Point3D point) => new Point3D(-point.X, -point.Y, -point.Z);
        public static Point3D operator +(Point3D point1, Point3D point2) => new Point3D(point1.X + point2.X, point1.Y + point2.Y, point1.Z + point2.Z);
        public static Point3D operator -(Point3D point1, Point3D point2) => point1 + (-point2);
        public static double Dist(Point3D p1, Point3D p2)
        {
            return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y) + Math.Abs(p1.Z - p2.Z);
        }
        public static double DotProduct(Point3D p1, Point3D p2)
        {
            return p1.X * p2.X + p1.Y * p2.Y + p1.Z * p2.Z;
        }
    }
}
