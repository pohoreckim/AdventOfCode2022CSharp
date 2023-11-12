using Utils;

// Task input
bool nextChar(ref string s, out char c)
{
    c = (char)0;
    if (s.Length == 0) return false;
    else
    {
        c = s[0];
        s = s.Substring(1);
    }
    return true;
}
List<object> parseToken(ref string input)
{
    string buffor = string.Empty;
    List<object> result = new List<object>();
    while(nextChar(ref input, out char c))
    {
        if (c == '[')
        {
            result.Add(parseToken(ref input));
        }
        else if (c == ']')
        {
            if(buffor != string.Empty) result.Add(int.Parse(buffor));
            return result;
        }
        else if (char.IsDigit(c)) { buffor += c; }
        else if (c == ',')
        {
            if (buffor != string.Empty) result.Add(int.Parse(buffor));
            buffor = string.Empty;
        }
    }
    return result;
}

string input = InputLoader.LoadInput();
var pairs = input.Split("\n\n");

List<(List<object> p1, List<object> p2)> packets = new List<(List<object> p1, List<object> p2)>();

foreach (var pair in pairs)
{
    var tokens = pair.Split('\n').Take(2).Select(x => x.Substring(1, x.Length - 1)).ToArray();
    var firstPacket = parseToken(ref tokens[0]);
    var secondPacket = parseToken(ref tokens[1]);
    packets.Add((firstPacket, secondPacket));
}

// Part One

int Compare(object left, object right)
{
    if(left.GetType() == typeof(List<object>) && right.GetType() == typeof(List<object>))
    {
        int bound = Math.Min(((List<object>)left).Count, ((List<object>)right).Count);
        for (int i = 0; i < bound; i++)
        {
            var res = Compare(((List<object>)left)[i], ((List<object>)right)[i]);
            if (res == -1 || res == 1) return res;
        }
        return (((List<object>)left).Count == ((List<object>)right).Count) ? 0 :
        (((List<object>)left).Count > ((List<object>)right).Count) ? -1 : 1 ;
    }
    else if(left.GetType() == typeof(int) && right.GetType() == typeof(int))
    {
        if ((int)left < (int)right)
            return 1;
        else if ((int)left > (int)right)
            return -1;
        else
            return 0;
    }
    else
    {
        if(left.GetType() == typeof(int))
        {
            return Compare(new List<object>() { left }, right);
        }
        else
        {
            return Compare(left, new List<object>() { right });
        }
    }
}

List<int> correct = new List<int>();
int id = 1;
foreach (var packet in packets)
{
    if(Compare(packet.p1, packet.p2) == 1) correct.Add(id);
    id++;
}

int result = correct.Sum();
Console.WriteLine($"Part One answer: {result}");

// Part Two

List<List<object>> allPackets = new List<List<object>>();
(string divPacketOne, string divPacketTwo) = ("[[2]]", "[[6]]");
(List<object> div1, List<object> div2) = (parseToken(ref divPacketOne), parseToken(ref divPacketTwo));
allPackets.Add(div1);
allPackets.Add(div2);

foreach (var packet in packets)
{
    allPackets.Add(packet.p1);
    allPackets.Add(packet.p2);
}

int Comp(object left, object right)
{
    return Compare(right, left);
}

allPackets.Sort(Comp);

result = (allPackets.IndexOf(div1) + 1) * (allPackets.IndexOf(div2) + 1);

Console.WriteLine($"Part Two answer: {result}");