string[] input = File.ReadAllLines("input.txt");

PartOne(input);
PartTwo(input);

Console.ReadKey();

void PartOne(string[] input)
{
    var games = from line in input
                let blocks = line.Split(new char[] { ':', '|' })
                let left = blocks[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                let right = blocks[2].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                select new { left, right };

    var result = games.Where(x => x.left.Intersect(x.right).Any()).Select(x => x.left.Intersect(x.right).Count()).ToList();

    double counter = 0;
    foreach (var res in result)
    {
        counter += Math.Pow(2, res - 1);
    }

    Console.WriteLine("Part 1: " + counter);
}

void PartTwo(string[] input)
{
    var games = (from line in input
                 let blocks = line.Split(new char[] { ':', '|' })
                 let left = blocks[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                 let right = blocks[2].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                 select new { left, right }).ToList();

    long counter = 0;
    Dictionary<int, long> dict = new Dictionary<int, long>();

    for (int i = 0; i < games.Count; i++)
    {
        var winningNumbers = games[i].left.Intersect(games[i].right).Count();
        long gameCount = 1;

        if (dict.TryGetValue(i, out long value))
        {
            gameCount += value;
            dict.Remove(i);
        }

        for (int x = 1; x <= winningNumbers; x++)
        {
            if (dict.ContainsKey(i + x))
            {
                dict[i + x] += gameCount;
            }
            else
            {
                dict[i + x] = gameCount;
            }
        }

        counter += gameCount;
    }

    Console.WriteLine("Part 2: " + counter);
}