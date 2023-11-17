using Utils;
using Day_Seventeen;

// Task input

string input = InputLoader.LoadInput().Trim();

List<string> stoneShapes = new List<string> { "####", ".#.\n###\n.#.", "..#\n..#\n###", "#\n#\n#\n#", "##\n##" };

// Part One

StonesEnumerator stones = new StonesEnumerator(stoneShapes);
HotGasEnumerator hotGas = new HotGasEnumerator(input);
Console.WriteLine($"{hotGas.Length}");
Chamber chamber = new Chamber(7, 10);
chamber.Simulation(stones, hotGas, 2022);


ulong result = chamber.Height;
Console.WriteLine($"Part One answer: {result}");

// Part Two

stones = new StonesEnumerator(stoneShapes);
hotGas = new HotGasEnumerator(input);
chamber = new Chamber(7, 10);
//chamber.Simulation(stones, hotGas, 1_000_000_000_000);

result = chamber.Height;
Console.WriteLine($"Part Two answer: {result}");