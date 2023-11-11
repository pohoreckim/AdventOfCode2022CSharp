using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_Twelve
{
    internal class Node
    {
        private static int[,] _map;
        public int X { get; private set; }
        public int Y { get; private set; }
        public int G { get; set; }
        public Node? Parent { get; set; }
        public Node((int x, int y) p) : this(p.x, p.y) { }
        public Node(int x, int y)
        {
            X = x;
            Y = y;
            G = int.MaxValue;
        }
        public int F(Node n) { return F(n.X, n.Y); }
        public int F((int x, int y) p) { return F(p.x, p.y); }
        public int F(int x, int y)
        {
            Func<(int x, int y), (int x, int y), int> manhattanMetric = (p1, p2) =>
            {
                return Math.Abs(p1.x - p2.x) + Math.Abs(p1.y - p2.y);
            };

            return G + manhattanMetric((x, y), (X, Y));
        }
        public List<Node> GetNeighbours()
        {
            List<Node> result = new List<Node>();
            for (int i = -1; i < 2; i+=2)
            {
                if (X + i > 0 && X + i < _map.GetLength(0) && _map[X, Y] + 1 >= _map[X + i, Y])
                    result.Add(new Node((X + i, Y)));
                if (Y + i > 0 && Y + i < _map.GetLength(1) && _map[X, Y] + 1 >= _map[X, Y + i])
                    result.Add(new Node((X, Y + i)));
            }
            return result;
        }
        public static bool InitializeMap(int[,] map)
        {
            if (_map == null)
            {
                _map = map;
                return true;
            }
            return false;
        }
        public override bool Equals(object? obj)
        {
            return obj is Node node &&
                   X == node.X &&
                   Y == node.Y;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
