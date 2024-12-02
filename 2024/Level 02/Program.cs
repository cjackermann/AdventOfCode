string[] input = File.ReadAllLines("input.txt");

Part1(input);
Part2(input);

Console.ReadKey();

static void Part1(string[] input)
{
    long result = 0;
    foreach (string line in input)
    {
        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();

        if (Check(parts))
        {
            result++;
        }
    }

    Console.WriteLine("Part1: " + result);
}

static void Part2(string[] input)
{
    long result = 0;
    foreach (string line in input)
    {
        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();

        for (int x = 0; x < parts.Count; x++)
        {
            var tmpParts = parts.ToList();
            tmpParts.RemoveAt(x);

            if (Check(tmpParts))
            {
                result++;
                break;
            }
        }
    }

    Console.WriteLine("Part2: " + result);
}

static bool Check(List<long> parts)
{
    bool? increase = null;
    bool isGood = true;
    long leftCheck;
    long rightCheck;

    for (int i = 0; i < parts.Count - 1; i++)
    {
        leftCheck = parts[i];
        rightCheck = parts[i + 1];

        var diff = Math.Abs(leftCheck - rightCheck);
        if (diff < 1 || diff > 3)
        {
            isGood = false;
            break;
        }

        if (leftCheck > rightCheck && increase == true)
        {
            isGood = false;
            break;
        }
        else if (leftCheck < rightCheck && increase == false)
        {
            isGood = false;
            break;
        }
        else if (increase == null)
        {
            increase = leftCheck < rightCheck;
        }
    }

    return isGood;
}