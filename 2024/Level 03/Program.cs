using System.Text.RegularExpressions;

string input = File.ReadAllText("input.txt");

Part1(input);
Part2(input);

Console.ReadKey();

static void Part1(string input)
{
    string pattern = @"mul\((\d+),(\d+)\)";
    var matches = Regex.Matches(input, pattern);

    long result = 0;
    foreach (Match match in matches)
    {
        var x = long.Parse(match.Groups[1].Value);
        var y = long.Parse(match.Groups[2].Value);

        result += x * y;
    }

    Console.WriteLine("Part1: " + result);
}

static void Part2(string input)
{
    string pattern = @"(do\(\)|don't\(\))|mul\((\d+),(\d+)\)";
    var matches = Regex.Matches(input, pattern);

    long result = 0;
    bool increaseResult = true;
    for (int i = 0; i < matches.Count; i++)
    {
        Match match = matches[i];

        if (match.Value == "don't()")
        {
            increaseResult = false;
            continue;
        }
        else if (match.Value == "do()")
        {
            increaseResult = true;
            continue;
        }
        else if (increaseResult)
        {
            var x = long.Parse(match.Groups[2].Value);
            var y = long.Parse(match.Groups[3].Value);

            result += x * y;
        }
    }

    Console.WriteLine("Part2: " + result);
}