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
        public ulong Height { get { return (ulong)_pileHeight + _levelsPruned; }}
        private int _pileHeight;
        private ulong _levelsPruned;
        private int _heightBuffor;
        private (int, int) SpawnPoint { get {  return (2, _pileHeight + 3); } }

        public Chamber(int chamberWidth, int heightBuffor)
        {
            _chamberWidth = chamberWidth;
            _chamber = new List<char[]>();
            _heightBuffor = heightBuffor;
            AddSpace(heightBuffor);
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
        public void Simulation(StonesEnumerator stones, HotGasEnumerator hotGas, ulong stonesCount, bool prune=true) 
        {
            for (ulong i = 0; i < stonesCount; i++)
            {
                stones.MoveNext();
                hotGas.MoveNext();
                Stone currentStone = stones.Current;
                currentStone.Move(SpawnPoint);
                while (true)
                {
                    (int x, int y) direction = hotGas.Current == '<' ? (-1, 0) : (1, 0);
                    if (CanMove(currentStone, direction))
                    {
                        currentStone.Move(direction);
                    }
                    direction = (0, -1);
                    if (CanMove(currentStone, direction))
                    {
                        currentStone.Move(direction);
                        hotGas.MoveNext();
                    }
                    else break;
                }
                AddStone(currentStone);
                _pileHeight = Math.Max(currentStone.Y + currentStone.Height, _pileHeight);
                int spaceNeed = _heightBuffor - (_chamber.Count - _pileHeight);
                if (spaceNeed > 0) AddSpace(spaceNeed);
                if (prune)
                {
                    int pruneLevel = GetFullLevel(currentStone);
                    if (pruneLevel > 0)
                    {
                        Prune(pruneLevel);
                    }
                }
            }
        }
        private bool CheckLevel(int level)
        {
            return _chamber[level].All(x => x == Globals.rock);
        }
        private int GetFullLevel(Stone stone)
        {
            for (int i = stone.Y + stone.Height - 1; i >= stone.Y; i--)
            {
                if(CheckLevel(i)) return i;
            }
            return -1;
        }
        private void Prune(int level)
        {
            _chamber = _chamber.Skip(level + 1).ToList();
            _levelsPruned += (ulong)level + 1;
            _pileHeight -= level + 1;
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
        public List<int> FullLevels()
        {
            List<int> levels = new List<int>();
            for (int i = 0; i < _chamber.Count; i++)
            {
                if (_chamber[i].All(x => x == Globals.rock)) levels.Add(i);
            }
            return levels;
        }
    }
}
