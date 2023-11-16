using Utils;
using Day_Seventeen;

// Task input

string input = InputLoader.LoadInput().Trim();

List<string> stoneShapes = new List<string> { "####", ".#.\n###\n.#.", "..#\n..#\n###", "#\n#\n#\n#", "##\n##" };

StonesEnumerator stones = new StonesEnumerator(stoneShapes);
HotGasEnumerator hotGas = new HotGasEnumerator(input);
Chamber chamber = new Chamber(7, 10);
chamber.Simulation(stones, hotGas, 2022);


    
// Part One

int result = 0;
Console.WriteLine($"Part One answer: {result}");

// Part Two

Console.WriteLine($"Part Two answer: {result}");