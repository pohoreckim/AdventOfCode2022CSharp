using Utils;

// Task input
string input = InputLoader.LoadInput();

// Part One

Func<char, int> charToPriority = (char c) =>
{
    return char.IsUpper(c) ? c - 'A' + 27 : c - 'a' + 1;
};

Func<string, string, string> findDuplicates = (string s1, string s2) =>
{
    List<char> duplicates = new List<char>();
    foreach (char c in s1)
    {
        if (s2.Contains(c) && !duplicates.Contains(c))
            duplicates.Add(c);
    }
    return duplicates.Select(c => c.ToString()).Aggregate((a, b) => a + b);
};

Func<string, int> stringToPriority = (string s) =>
{
    return s.Select(x => charToPriority(x)).Sum();
};

var sum = input.Split('\n').SkipLast(1).Select(x => findDuplicates(x.Substring(0, x.Length / 2), x.Substring(x.Length /2))).Select(x => stringToPriority(x)).Sum();

Console.WriteLine(sum);

// Part Two

int i = 0;
List<string> results = new List<string>();
input.Split('\n').SkipLast(1).Aggregate((s1, s2) =>
{
    i = (i + 1) % 3;
    string result = s2;
    if (i != 0)
    {
        result = findDuplicates(s1, s2);
        if (i == 2)
            results.Add(result);
    }
    return result;
});

sum = results.Select(x => stringToPriority(x)).Sum();

Console.WriteLine(sum);
