using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_Fourteen
{
    internal class Scan
    {
        private char[,] _drawing;
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int OffsetX { get; private set; }
        public int OffsetY { get; private set; }
        public int SandUnitsCount { get; private set; }
        public Scan(int width, int height, int offsetX, int offsetY)
        {
            Width = width;
            Height = height;
            OffsetX = offsetX;
            OffsetY = offsetY;
            _drawing = new char[Width, Height];
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    _drawing[i, j] = '.';
                }
            }
            SandUnitsCount = 0;
        }
        public void DrawLine(char c, (int x, int y) p1, (int x, int y) p2)
        {
            int xDiff = Math.Abs(p1.x - p2.x);
            int yDiff = Math.Abs(p1.y - p2.y);
            if (xDiff > 0 && yDiff == 0)
            {
                (int from, int to) = p1.x > p2.x ? (p2.x, p1.x) : (p1.x, p2.x);
                for (int i = from; i <= to; i++)
                {
                    DrawPoint(c, i, p1.y);
                }
            }
            else if (yDiff > 0 && xDiff == 0)
            {
                (int from, int to) = p1.y > p2.y ? (p2.y, p1.y) : (p1.y, p2.y);
                for (int i = from; i <= to; i++)
                {
                    DrawPoint(c, p1.x, i);
                }
            }
            else if(xDiff > 0 && yDiff > 0)
            {
                throw new Exception("Line is not horizontal, nor vertical.");
            }
        }
        public void DrawPoint(char c, int x, int y)
        {
            _drawing[x - OffsetX, y - OffsetY] = c;
        }
        public void DrawPoint(char c, (int x, int y) p)
        {
            DrawPoint(c, p.x, p.y);
        }
        public void Draw()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Console.Write(_drawing[j, i]);
                }
                Console.WriteLine();
            }
        }
        public bool DropSandUnit((int x, int y) p)
        {
            (int, int) source = p;
            while(FreeFall(ref p)) { }
            if (p.y == Height - 1)
            {
                return false;
            }
            else
            {
                DrawPoint('o', p);
                SandUnitsCount++;
                if (p == source) return false;
            }
            return true;
        }
        private bool FreeFall(ref (int x, int y) p)
        {
            if (!IsFree(p) || p.y == Height - 1)
                return false;
            else
            {
                if(IsFree((p.x, p.y + 1)))
                {
                    p = (p.x, p.y + 1);
                    return true;
                }
                else if(IsFree((p.x - 1, p.y + 1)))
                {
                    p = (p.x - 1, p.y + 1);
                    return true;
                }
                else if (IsFree((p.x + 1, p.y + 1)))
                {
                    p = (p.x + 1, p.y + 1);
                    return true;
                }
            }
            return false;
        }
        private bool IsFree((int x, int y) p)
        {
            return _drawing[p.x - OffsetX, p.y - OffsetY] == '.' || _drawing[p.x - OffsetX, p.y - OffsetY] == '+';
        }
    }
}
