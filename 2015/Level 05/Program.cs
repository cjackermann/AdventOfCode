using System.Text.RegularExpressions;

string[] input = File.ReadAllLines("input.txt");

PartOne(input);
PartTwo(input);

static void PartOne(string[] input)
{
    int count = 0;
    foreach (var line in input)
    {
        int vowelCount = line.Count(d => d is 'a' or 'e' or 'i' or 'o' or 'u');
        bool hasDoubleLetter = Regex.Match(line, "(.)\\1{1,}").Success;
        bool isGood = !line.Contains("ab") && !line.Contains("cd") && !line.Contains("pq") && !line.Contains("xy");

        if (vowelCount >= 3 && hasDoubleLetter && isGood)
        {
            count++;
        }
    }

    Console.WriteLine(count);
}

static void PartTwo(string[] input)
{
    int count = 0;
    foreach (var line in input)
    {
        var dict = new Dictionary<string, int>();
        for (int i = 0; i < line.Length; i++)
        {
            if (i + 1 < line.Length)
            {
                var x = line[i].ToString() + line[i + 1].ToString();
                if (dict.ContainsKey(x))
                {
                    dict[x]++;
                }
                else
                {
                    dict.Add(x, 1);
                }

                if (i + 2 < line.Length && line[i] == line[i + 2] && line[i + 1] == line[i + 2])
                {
                    i++;
                }
            }
        }

        bool part2Match = false;
        for (int i = 0; i < line.Length; i++)
        {
            if (i + 2 < line.Length && line[i] == line[i + 2])
            {
                part2Match = true;
                break;
            }
        }

        if (part2Match && dict.MaxBy(d => d.Value).Value > 1)
        {
            count++;
        }
    }

    Console.WriteLine(count);
}