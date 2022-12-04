string[] input = File.ReadAllLines("input.txt");

//PartOne(input);
PartTwo(input);

static void PartOne(string[] input)
{
    int elveCounter = 0;
    Dictionary<int, int> elves = new();
    foreach (var line in input)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            elveCounter++;
        }
        else
        {
            if (elves.ContainsKey(elveCounter))
            {
                elves[elveCounter] += int.Parse(line);
            }
            else
            {
                elves.Add(elveCounter, int.Parse(line));
            }
        }
    }

    Console.WriteLine(elves.Max(d => d.Value));
}

static void PartTwo(string[] input)
{
    int elveCounter = 0;
    Dictionary<int, int> elves = new();
    foreach (var line in input)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            elveCounter++;
        }
        else
        {
            if (elves.ContainsKey(elveCounter))
            {
                elves[elveCounter] += int.Parse(line);
            }
            else
            {
                elves.Add(elveCounter, int.Parse(line));
            }
        }
    }

    Console.WriteLine(elves.OrderByDescending(d => d.Value).Take(3).Sum(d => d.Value));
}