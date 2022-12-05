string[] input = File.ReadAllLines("input.txt");

var dict = new Dictionary<int, List<string>>
{
    { 1, new List<string> { "P", "Z", "M", "T", "R", "C", "N" } },
    { 2, new List<string> { "Z", "B", "S", "T", "N", "D" } },
    { 3, new List<string> { "G", "T", "C", "F", "R", "Q", "H", "M" } },
    { 4, new List<string> { "Z", "R", "G" } },
    { 5, new List<string> { "H", "R", "N", "Z" } },
    { 6, new List<string> { "D", "L", "Z", "P", "W", "S", "H", "F" } },
    { 7, new List<string> { "M", "G", "C", "R", "Z", "D", "W" } },
    { 8, new List<string> { "Q", "Z", "W", "H", "L", "F", "J", "S" } },
    { 9, new List<string> { "N", "W", "P", "Q", "S" } },
};

//PartOne(input, dict);
PartTwo(input, dict);

static void PartOne(string[] input, Dictionary<int, List<string>> dict)
{
    foreach (var line in input)
    {
        var blocks = line.Split(' ');
        int from = int.Parse(blocks[3]);
        int to = int.Parse(blocks[5]);
        int amount = int.Parse(blocks[1]);

        if (dict.TryGetValue(from, out List<string> values))
        {
            var neededValues = values.Take(amount).ToList();
            dict[from].RemoveRange(0, amount);
            neededValues.ForEach(d => dict[to].Insert(0, d));
        }
    }

    dict.Select(d => d.Value.FirstOrDefault()).ToList().ForEach(Console.Write);
}

static void PartTwo(string[] input, Dictionary<int, List<string>> dict)
{
    foreach (var line in input)
    {
        var blocks = line.Split(' ');
        int from = int.Parse(blocks[3]);
        int to = int.Parse(blocks[5]);
        int amount = int.Parse(blocks[1]);

        if (dict.TryGetValue(from, out List<string> values))
        {
            var neededValues = values.Take(amount).ToList();
            dict[from].RemoveRange(0, amount);
            dict[to].InsertRange(0, neededValues);
        }
    }

    dict.Select(d => d.Value.FirstOrDefault()).ToList().ForEach(Console.Write);
}
