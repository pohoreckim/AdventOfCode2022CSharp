using Utils;
using Day_Seventeen;
using System.Linq;

// Task input

string input = InputLoader.LoadInput().Trim();

List<string> stoneShapes = new List<string> { "####", ".#.\n###\n.#.", "..#\n..#\n###", "#\n#\n#\n#", "##\n##" };

// Part One

StonesEnumerator stones = new StonesEnumerator(stoneShapes);
HotGasEnumerator hotGas = new HotGasEnumerator(input);
Chamber chamber = new Chamber(7, 10);
chamber.Simulation(stones, hotGas, 2022);
ulong result = chamber.Height;
Console.WriteLine($"Part One answer: {result}");

// Part Two

stones = new StonesEnumerator(stoneShapes);
hotGas = new HotGasEnumerator(input);
chamber = new Chamber(7, 10);

// Each stone encounters at least 3 hot gas jets
// With 2 * len(jets) / 3 stones we examine all the jets twice
// Should be enough to find a cycle if exists 

var history = chamber.Simulation(stones, hotGas, (ulong)(2 * hotGas.Length / 3), false);

(int m, int n, int k) FindMaxCycle(List<(int hDiff, int stonePos, int gasPos)> collection)
{
    (int m, int n, int k) maxK = (-1, -1, -1);
    for (int i = 0; i < collection.Count; i++)
    {
        for (int j = i + 1; j < collection.Count; j++)
        {
            if (collection[i] == collection[j])
            {
                int k = 1;
                while(i + k < collection.Count && j + k < collection.Count && collection[i + k] == collection[j + k])
                {
                    k++;
                }
                if (k > maxK.k) maxK = (i, j, k); 
            }
        }
    }
    return maxK;
}

(int loopStart, int loopEnd, _) = FindMaxCycle(history);
int loopLen = loopEnd - loopStart;

var preLoop = history.Take(loopStart).Sum(x => x.hDiff);
var loop = history.Skip(loopStart).Take(loopLen).Sum(x => x.hDiff);

ulong stonesCount = 1_000_000_000_000;
ulong loopsCount = (stonesCount - (ulong)loopStart) / (ulong)loopLen;
ulong stonesLeft = stonesCount - (ulong)loopStart - (ulong)loopLen * loopsCount;

var postLoop = history.Skip(loopStart).Take((int)stonesLeft).Sum(x => x.hDiff);

result = (ulong)preLoop + (ulong)postLoop + (ulong)loop * loopsCount;

Console.WriteLine($"Part Two answer: {result}");