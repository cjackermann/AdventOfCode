string input = File.ReadAllText("input.txt");

Dictionary<long, long> stones = input.Split(" ").ToDictionary(long.Parse, x => 1L);

for (int i = 1; i <= 75; i++)
{
    Dictionary<long, long> newStones = stones.ToDictionary(x => x.Key, x => x.Value);
    foreach (var stone in stones)
    {
        long firstStone = 1;
        long? secondStone = null;

        if (stone.Key.ToString().Length % 2 == 0)
        {
            firstStone = long.Parse(stone.Key.ToString().Substring(0, stone.Key.ToString().Length / 2));
            secondStone = long.Parse(stone.Key.ToString().Substring(stone.Key.ToString().Length / 2));
        }
        else if (stone.Key != 0)
        {
            firstStone = stone.Key * 2024;
        }

        AddOrUpdateStone(newStones, firstStone, stone.Value);
        if (secondStone != null)
        {
            AddOrUpdateStone(newStones, secondStone.Value, stone.Value);
        }

        RemoveOrUpdateStone(newStones, stone.Key, stone.Value);
    }

    stones = newStones;

    if (i == 25)
    {
        Console.WriteLine("Part 1: " + stones.Sum(x => x.Value));
    }
}

Console.WriteLine("Part 2: " + stones.Sum(x => x.Value));
Console.ReadKey();

static void AddOrUpdateStone(Dictionary<long, long> stones, long key, long value)
{
    if (stones.TryGetValue(key, out long count1))
    {
        stones[key] = count1 + value;
    }
    else
    {
        stones.Add(key, value);
    }
}

static void RemoveOrUpdateStone(Dictionary<long, long> stones, long key, long value)
{
    if (stones.TryGetValue(key, out var count))
    {
        if (count == value)
        {
            stones.Remove(key);
        }
        else
        {
            stones[key] = count - value;
        }
    }
}