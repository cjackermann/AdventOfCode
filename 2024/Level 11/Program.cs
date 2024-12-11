using System.Collections.Concurrent;

string input = File.ReadAllText("input.txt");

var stones = new ConcurrentDictionary<long, long>(input.Split(" ").ToDictionary(long.Parse, x => 1L));

for (int i = 1; i <= 75; i++)
{
    var newStones = new ConcurrentDictionary<long, long>();
    foreach (var stone in stones)
    {
        long stone1 = 1;
        long? stone2 = null;

        if (stone.Key.ToString().Length % 2 == 0)
        {
            stone1 = long.Parse(stone.Key.ToString().Substring(0, stone.Key.ToString().Length / 2));
            stone2 = long.Parse(stone.Key.ToString().Substring(stone.Key.ToString().Length / 2));
        }
        else if (stone.Key != 0)
        {
            stone1 = stone.Key * 2024;
        }

        newStones.AddOrUpdate(stone1, stone.Value, (key, oldValue) => oldValue + stone.Value);
        if (stone2 != null)
        {
            newStones.AddOrUpdate(stone2.Value, stone.Value, (key, oldValue) => oldValue + stone.Value);
        }
    }

    stones = newStones;

    if (i == 25)
    {
        Console.WriteLine("Part 1: " + stones.Sum(x => x.Value));
    }
}

Console.WriteLine("Part 2: " + stones.Sum(x => x.Value));
Console.ReadKey();