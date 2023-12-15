string input = File.ReadAllText("input.txt");

long counter1 = 0;
foreach (var part in input.Split(','))
{
    long tmpCounter = 0;
    foreach (var item in part)
    {
        tmpCounter += item;
        tmpCounter *= 17;
        tmpCounter %= 256;
    }

    counter1 += tmpCounter;
}

Console.WriteLine("Part 1: " + counter1);

Dictionary<int, List<(string, long)>> dict = Enumerable.Range(0, 256).ToDictionary(x => x, x => new List<(string, long)>());
foreach (var part in input.Split(','))
{
    int tmpCounter = 0;
    var tmpString = string.Join("", part.Where(x => x >= 97 && x <= 122));

    foreach (var item in tmpString)
    {
        tmpCounter += item;
        tmpCounter *= 17;
        tmpCounter %= 256;
    }

    if (part.Contains('-'))
    {
        if (dict.TryGetValue(tmpCounter, out List<(string, long)>? value))
        {
            var tmp = value?.FirstOrDefault(x => x.Item1 == tmpString);
            if (tmp != null)
            {
                value.Remove(tmp.Value);
            }
        }
    }
    else
    {
        var left = part.Split('=')[0];
        var right = long.Parse(part.Split('=')[1]);

        if (!dict[tmpCounter].Any(x => x.Item1 == tmpString))
        {
            dict[tmpCounter].Add((left, right));
        }
        else
        {
            var item = dict[tmpCounter].First(x => x.Item1 == tmpString);
            var idx = dict[tmpCounter].IndexOf(item);
            dict[tmpCounter].Remove(item);
            dict[tmpCounter].Insert(idx, (left, right));
        }
    }
}

long counter2 = 0;
foreach (var box in dict.Where(x => x.Value.Any()))
{
    long tmpCounter = 0;
    for (int i = 0; i < box.Value.Count; i++)
    {
        tmpCounter += (box.Key + 1) * (i + 1) * box.Value[i].Item2;
    }

    counter2 += tmpCounter;
}

Console.WriteLine("Part 2: " + counter2);
Console.ReadKey();