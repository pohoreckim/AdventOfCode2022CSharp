using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_Seventeen
{
    internal class Stone
    {
        private List<(int x, int y)> _rocks;
        public int Width { get; private set; }
        public int Height { get; private set; }
        public List<(int x, int y)> Rocks => _rocks;
        public int X { get; private set; }
        public int Y { get; private set; }
        public Stone(string s)
        {
            _rocks = new List<(int x, int y)>();
            var lines = s.Split('\n');
            X = 0;
            Y = 0;
            Width = lines[0].Length;
            Height = lines.Length;
            for (int i = lines.Length - 1; i >= 0; i--)
            {
                char[] chars = lines[i].ToCharArray();
                for (int j = 0; j < chars.Length; j++)
                {
                    if (chars[j].Equals(Globals.rock)) _rocks.Add((j, lines.Length - i - 1));
                }
            }
        }
        public void Move((int x, int y) vector)
        {
            for (int i = 0; i < _rocks.Count; i++)
            {
                _rocks[i] = (_rocks[i].x + vector.x, _rocks[i].y + vector.y);
            }
            X += vector.x;
            Y += vector.y;
        }
    }
}
