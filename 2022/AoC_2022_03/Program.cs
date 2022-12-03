string[] input = File.ReadAllLines("input.txt");

PartOne(input);
PartTwo(input);

static void PartOne(string[] input)
{
    List<char> alphabet = new();
    "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower().ToList().ForEach(alphabet.Add);
    "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList().ForEach(alphabet.Add);

    int result = 0;
    foreach (string line in input)
    {
        var partOne = line[..(line.Length / 2)];
        var partTwo = line[(line.Length / 2)..];

        foreach (var charakter in partOne)
        {
            if (partTwo.Contains(charakter))
            {
                result += alphabet.IndexOf(charakter) + 1;
                break;
            }
        }
    }

    Console.WriteLine(result);
}

static void PartTwo(string[] input)
{
    List<char> alphabet = new();
    "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower().ToList().ForEach(alphabet.Add);
    "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList().ForEach(alphabet.Add);

    int result = 0;
    for (int i = 0; i < input.Length / 3; i++)
    {
        var group = input.Skip(i * 3).Take(3);
        foreach (var charakter in group.First())
        {
            if (group.Skip(1).First().Contains(charakter) && group.Last().Contains(charakter))
            {
                result += alphabet.IndexOf(charakter) + 1;
                break;
            }
        }
    }

    Console.WriteLine(result);
}