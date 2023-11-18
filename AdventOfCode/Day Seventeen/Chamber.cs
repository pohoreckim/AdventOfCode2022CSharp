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
        public List<char[]> Cave { get => _chamber; }
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
        private bool StoneSimulation(Stone stone, char gasJet)
        {
            (int x, int y)[] directions = new (int x, int y)[] { gasJet == '<' ? (-1, 0) : (1, 0), (0, -1) };
            bool result = false;
            for (int i = 0; i < directions.Length; i++)
            {
                result = CanMove(stone, directions[i]);
                if(result) stone.Move(directions[i]);
            }
            return result;
        }
        public List<(int hDiff, int stone, int gas)> Simulation(StonesEnumerator stones, HotGasEnumerator hotGas, ulong stonesCount, bool prune = true)
        {
            List<(int h, int s, int g)> result = new List<(int h, int s, int g)>();
            for (ulong i = 0; i < stonesCount; i++)
            {
                Stone currentStone = stones.GetNext();
                char gasJet = hotGas.GetNext();
                currentStone.Move(SpawnPoint);
                while (StoneSimulation(currentStone, gasJet))
                {
                    gasJet = hotGas.GetNext();
                }
                AddStone(currentStone);
                int prevHeight = _pileHeight;
                _pileHeight = Math.Max(currentStone.Y + currentStone.Height, _pileHeight);
                result.Add((_pileHeight - prevHeight, stones.Position, hotGas.Position));
                // Calculate space to be added above highest rock.
                int spaceNeed = _heightBuffor - (_chamber.Count - _pileHeight);
                if (spaceNeed > 0) AddSpace(spaceNeed);
                // Check if new fully filled level is created and prune chamber if so.
                if (prune)
                {
                    int pruneLevel = GetFullLevel(currentStone);
                    if (pruneLevel > 0)
                    {
                        Prune(pruneLevel);
                    }
                }
            }
            return result;
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
