string[] input = File.ReadAllLines("input.txt");

long counter1 = 0;
long counter2 = 0;
foreach (var line in input)
{
    var dict = new Dictionary<int, List<long>>
    {
        { 0, line.Split(' ').Select(long.Parse).ToList() }
    };

    int tmpCounter = 1;
    while (true)
    {
        var lastEntry = dict.Last();
        var newList = new List<long>();
        for (int i = 0; i < lastEntry.Value.Count - 1; i++)
        {
            newList.Add(lastEntry.Value[i + 1] - lastEntry.Value[i]);
        }

        dict.Add(tmpCounter, newList);
        if (newList.All(x => x == 0))
        {
            break;
        }

        tmpCounter++;
    }

    counter1 += PartOne(tmpCounter, dict);
    counter2 += PartTwo(tmpCounter, dict);
}

Console.WriteLine("Part 1: " + counter1);
Console.WriteLine("Part 2: " + counter2);
Console.ReadKey();

long PartOne(int tmpCounter, Dictionary<int, List<long>> dict)
{
    for (int i = tmpCounter; i > 0; i--)
    {
        var currentItem = dict[i].Last();
        dict[i - 1].Add(dict[i - 1].Last() + currentItem);
    }

    return dict[0].Last();
}

long PartTwo(int tmpCounter, Dictionary<int, List<long>> dict)
{
    for (int i = tmpCounter; i > 0; i--)
    {
        var currentItem = dict[i].First();
        dict[i - 1].Insert(0, dict[i - 1].First() - currentItem);
    }

    return dict[0].First();
}