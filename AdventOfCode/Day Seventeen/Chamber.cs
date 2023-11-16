using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_Seventeen
{
    internal class Chamber
    {
        private int _chamberWidth;
        private List<char[]> _chamber;
        public int Height { get; private set; }
        private int _pileHeight;
        private int _levelsPruned;
        private (int, int) SpawnPoint { get {  return (2, _pileHeight + 3); } }

        public Chamber(int chamberWidth, int startHeight)
        {
            _chamberWidth = chamberWidth;
            _chamber = new List<char[]>();
            Height = startHeight;
            AddSpace(Height);
            _levelsPruned = 0;
            _pileHeight = 0;
        }
        private void AddSpace(int levels) 
        {
            for (int i = 0; i < levels; i++)
            {
                _chamber.Add(Enumerable.Repeat(Globals.air, _chamberWidth).ToArray());
            }
        }
        public void Simulation(StonesEnumerator stones, HotGasEnumerator hotGas, int stonesCount) 
        {
            for (int i = 0; i < stonesCount; i++)
            {
                stones.MoveNext();
                hotGas.MoveNext();
                Stone currentStone = stones.Current;
                currentStone.Move(SpawnPoint);
                (int x, int y) direction = hotGas.Current == '<' ? (-1, 0) : (1, 0);
                while (true)
                {
                    if(CanMove(currentStone, direction))
                    {
                        currentStone.Move(direction);
                    }
                    direction = (0, -1);
                    if (CanMove(currentStone, direction))
                    {
                        currentStone.Move(direction);
                        hotGas.MoveNext();
                        direction = hotGas.Current == '<' ? (-1, 0) : (1, 0);
                    }
                    else break;
                }
                AddStone(currentStone);
                //Draw();
                _pileHeight = Math.Max(currentStone.Y + currentStone.Height, _pileHeight);
                int spaceNeed = 10 - (_chamber.Count - _pileHeight);
                if (spaceNeed > 0) AddSpace(spaceNeed);
            }
        }
        private void AddStone(Stone stone)
        {
            foreach(var rock in stone.Rocks)
            {
                _chamber[rock.y][rock.x] = Globals.rock;
            }
        }
        public void Draw() 
        {
            for (int i = _chamber.Count - 1; i >= 0; i--)
            {
                string level = "|" + new string(_chamber[i]) + "|";
                Console.WriteLine(level);
            }
            Console.WriteLine($"+{new string('-', _chamberWidth)}+");
        }
        private bool CanMove(Stone stone, (int x, int y) direction)
        {
            foreach (var rock in stone.Rocks)
            {
                int x = rock.x + direction.x;
                int y = rock.y + direction.y;
                if(x < 0 || y < 0 || x >= _chamberWidth || _chamber[y][x] != Globals.air)   return false;
            }
            return true;   
        }
    }
}
