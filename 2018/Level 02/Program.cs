string[] input = File.ReadAllLines("input.txt");

int two = 0;
int three = 0;
foreach (var line in input)
{
    var test = line.GroupBy(d => d);
    two += test.Where(d => d.Count() == 2).Any() ? 1 : 0;
    three += test.Where(d => d.Count() == 3).Any() ? 1 : 0;
}

Console.WriteLine(two * three);

Console.WriteLine(GetPartTwo(input));

static string GetPartTwo(string[] input)
{
    foreach (var line1 in input)
    {
        foreach (var line2 in input)
        {
            if (line1 == line2)
            {
                continue;
            }

            List<int> different = new();
            for (int i = 0; i < line1.Length; i++)
            {
                if (line1[i] != line2[i])
                {
                    different.Add(i);
                }
            }

            if (different.Count == 1)
            {
                var res = line1.ToList();
                res.RemoveAt(different.First());
                string result = string.Empty;
                res.ForEach(d => result += d);
                return result;
            }
        }
    }

    return null;
}