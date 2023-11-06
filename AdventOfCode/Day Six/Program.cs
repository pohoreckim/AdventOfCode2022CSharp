using System.Collections;
using System.Security.Cryptography.X509Certificates;
using Utils;

// Task input
string input = InputLoader.LoadInput();

//Part One 
char[] chars = input.ToCharArray();

Func<Queue<char>, int, bool> checkIfDuplicates = (queue, len) =>
{
    return len == queue.Select(x => x).Distinct().Count();
};

Func<char[], int, int> findMarker = (x, len) =>
{
    Queue<char> slidingWindow = new Queue<char>();
    for (int i = 0; i < chars.Length; i++)
    {
        slidingWindow.Enqueue(chars[i]);
        if(slidingWindow.Count > len - 1)
        {
            if (checkIfDuplicates(slidingWindow, len))
                return i + 1;
            slidingWindow.Dequeue();
        }
    }
    return -1;
};

int markerIndex = findMarker(chars, 4);
Console.WriteLine($"Part One answear: {markerIndex}");

// Part Two

markerIndex = findMarker(chars, 14);
Console.WriteLine($"Part Two answear: {markerIndex}");
