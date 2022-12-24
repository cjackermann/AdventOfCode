using System.Text.RegularExpressions;

string input = File.ReadAllText("input.txt");

int result = PartOne(input);
Console.WriteLine("Stage 1: " + result);

result = PartTwo(input);
Console.WriteLine("Stage 2: " + result);

static int PartOne(string input)
{
    for (int i = 0; i < input.Length - 1; i++)
    {
        if (input[i] == (input[i + 1] + 32))
        {
            input = input.Remove(i, 2);
            i = -1;
        }
        else if (input[i] == (input[i + 1] - 32))
        {
            input = input.Remove(i, 2);
            i = -1;
        }
    }

    return input.Length;
}

static int PartTwo(string input)
{
    var diffCharakters = input.ToLower().Distinct().ToList();
    int best = int.MaxValue;
    foreach (char charakter in diffCharakters)
    {
        char big = (char)(charakter - 32);
        string tmpInput = Regex.Replace(input, $@"[{charakter}]|[{big}]", string.Empty);

        int result = PartOne(tmpInput);
        if (result < best)
        {
            best = result;
        }
    }

    return best;
}
