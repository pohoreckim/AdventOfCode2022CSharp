// See https://aka.ms/new-console-template for more information
using System.Collections;
using Utils;

string input = InputLoader.LoadInput();
List<int> sums = new List<int>();
input.Split('\n').Select(x => { try { return Convert.ToInt32(x); } catch (Exception) { return -1; } })
    .Aggregate(0, (a, b) => { if (b != -1) return a + b; else sums.Add(a); return 0; });

int maxValue = sums.Max();

Console.WriteLine($"Max value: {maxValue}");

int threeMaxValues = sums.OrderByDescending(x => x).Take(3).Sum();  

Console.WriteLine(threeMaxValues.ToString());

