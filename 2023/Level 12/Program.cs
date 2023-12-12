string[] input = File.ReadAllLines("input.txt");

var ws = input.Select(line => line.Split()).ToList();
var rows = ws.Select(x => (x[0], x[1].Split(',').Select(int.Parse).ToList())).ToList();

long counter1 = 0;
foreach (var row in rows)
{
    counter1 += GetResult(row.Item1, row.Item2, new Dictionary<(string, int, int), long>());
}

Console.WriteLine("Part 1: " + counter1);

long counter2 = 0;
foreach (var row in rows)
{
    var t = string.Join("?", Enumerable.Repeat(row.Item1, 5));
    var x = string.Join(",", Enumerable.Repeat(string.Join(",", row.Item2), 5)).Split(",").Select(int.Parse).ToList();

    counter2 += GetResult(t, x, new Dictionary<(string, int, int), long>());
}

Console.WriteLine("Part 2: " + counter2);
Console.ReadKey();

long GetResult(string inputString, List<int> sizes, Dictionary<(string, int, int), long> cache)
{
    inputString += ".";

    return Calculate(inputString, 0, 0, cache);

    long Calculate(string currentString, int groupsDone, int numbersDoneInGroup, Dictionary<(string, int, int), long> cache)
    {
        if (string.IsNullOrEmpty(currentString))
        {
            return groupsDone == sizes.Count && numbersDoneInGroup == 0 ? 1 : 0;
        }

        var key = (currentString, groupsDone, numbersDoneInGroup);

        if (cache.TryGetValue(key, out var cachedResult))
        {
            return cachedResult;
        }

        long numbersSolved = 0;
        var possible = currentString[0] == '?' ? new[] { '.', '#' } : new[] { currentString[0] };

        foreach (var c in possible)
        {
            if (c == '#')
            {
                numbersSolved += Calculate(currentString.Substring(1), groupsDone, numbersDoneInGroup + 1, cache);
            }
            else
            {
                if (numbersDoneInGroup > 0)
                {
                    if (groupsDone < sizes.Count && sizes[groupsDone] == numbersDoneInGroup)
                    {
                        numbersSolved += Calculate(currentString.Substring(1), groupsDone + 1, 0, cache);
                    }
                }
                else
                {
                    numbersSolved += Calculate(currentString.Substring(1), groupsDone, 0, cache);
                }
            }
        }

        cache[key] = numbersSolved;
        return numbersSolved;
    }
}