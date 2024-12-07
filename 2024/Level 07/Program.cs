string[] input = File.ReadAllLines("input.txt");

long resultPart1 = 0;
long resultPart2 = 0;
foreach (string line in input)
{
    var parts = line.Split(":");
    long targetValue = long.Parse(parts[0]);
    List<long> values = parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();

    if (DoCalculationPart1(targetValue, values.First(), values, 1))
    {
        resultPart1 += targetValue;
    }

    if (DoCalculationPart2(targetValue, values.First(), values, 1))
    {
        resultPart2 += targetValue;
    }
}

Console.WriteLine("Part 1: " + resultPart1);
Console.WriteLine("Part 2: " + resultPart2);
Console.ReadKey();

static bool DoCalculationPart1(long targetValue, long currentValue, List<long> values, int idx)
{
    var mulValue = currentValue * values[idx];
    var addValue = currentValue + values[idx];

    if (mulValue == targetValue || addValue == targetValue)
    {
        return true;
    }
    else if (values.Count == idx + 1)
    {
        return false;
    }
    else
    {
        return DoCalculationPart1(targetValue, mulValue, values, idx + 1) || DoCalculationPart1(targetValue, addValue, values, idx + 1);
    }
}

static bool DoCalculationPart2(long targetValue, long currentValue, List<long> values, int idx)
{
    var mulValue = currentValue * values[idx];
    var addValue = currentValue + values[idx];
    var combineValue = long.Parse(currentValue.ToString() + values[idx].ToString());

    if (mulValue == targetValue || addValue == targetValue || combineValue == targetValue)
    {
        return true;
    }
    else if (values.Count == idx + 1)
    {
        return false;
    }
    else
    {
        return DoCalculationPart2(targetValue, mulValue, values, idx + 1) || DoCalculationPart2(targetValue, addValue, values, idx + 1) || DoCalculationPart2(targetValue, combineValue, values, idx + 1);
    }
}