using Utils;

// Task input
string input = InputLoader.LoadInput();

// Part One
List<int> sums = new List<int>();
input.Split('\n').Select(x => { try { return Convert.ToInt32(x); } catch (Exception) { return -1; } })
    .Aggregate(0, (a, b) => { if (b != -1) return a + b; else sums.Add(a); return 0; });
int maxValue = sums.Max();

Console.WriteLine($"Max value: {maxValue}");

// Part Two
int threeMaxValues = sums.OrderByDescending(x => x).Take(3).Sum();  

Console.WriteLine($"Sum of three highest values: {threeMaxValues}");

