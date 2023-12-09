using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_Eighteen
{
    internal class Cube
    {
        private static double sideLength = 1.0;
        public Point3D Middle { get; private set; }
        public List<Face> Faces { get; private set; }
        public Cube(Point3D middle, bool full)
        {
            Middle = middle;
            Faces = full ? GetAllFaces(middle) : new List<Face>();
        }
        public bool AddFace(Face face)
        {
            if (Faces.Contains(face)) return false; 
            Faces.Add(face); 
            return true;
        }
        public static double Distance(Cube c1, Cube c2)
        {
            return Math.Abs(c1.Middle.X - c2.Middle.X) + Math.Abs(c1.Middle.Y - c2.Middle.Y) + Math.Abs(c1.Middle.Z - c2.Middle.Z);
        }
        public static List<Face> GetAllFaces(Point3D middle)
        {
            List<Face> faces = new List<Face>();
            faces.Add(new Face(new Point3D(middle.X, middle.Y, middle.Z - (sideLength / 2)), new Point3D(0, 0, -1)));
            faces.Add(new Face(new Point3D(middle.X, middle.Y, middle.Z + (sideLength / 2)), new Point3D(0, 0, 1)));
            faces.Add(new Face(new Point3D(middle.X, middle.Y - (sideLength / 2), middle.Z), new Point3D(0, -1, 0)));
            faces.Add(new Face(new Point3D(middle.X, middle.Y + (sideLength / 2), middle.Z), new Point3D(0, 1, 0)));
            faces.Add(new Face(new Point3D(middle.X - (sideLength / 2), middle.Y, middle.Z), new Point3D(-1, 0, 0)));
            faces.Add(new Face(new Point3D(middle.X + (sideLength / 2), middle.Y, middle.Z), new Point3D(1, 0, 0)));
            return faces;
        }
    }
}
