string input = File.ReadAllText("input.txt");

Dictionary<long, long> stones = input.Split(" ").ToDictionary(long.Parse, x => 1L);

for (int i = 0; i < 75; i++)
{
    Dictionary<long, long> newStones = stones.ToDictionary(x => x.Key, x => x.Value);
    foreach (var stone in stones)
    {
        if (stone.Key == 0)
        {
            if (newStones.TryGetValue(1, out long count))
            {
                newStones[1] = count + stone.Value;
            }
            else
            {
                newStones.Add(1, stone.Value);
            }
        }
        else if (stone.Key.ToString().Length % 2 == 0)
        {
            var firstStone = long.Parse(stone.Key.ToString().Substring(0, stone.Key.ToString().Length / 2));
            var secondStone = long.Parse(stone.Key.ToString().Substring(stone.Key.ToString().Length / 2));

            if (newStones.TryGetValue(firstStone, out long count1))
            {
                newStones[firstStone] = count1 + stone.Value;
            }
            else
            {
                newStones.Add(firstStone, stone.Value);
            }

            if (newStones.TryGetValue(secondStone, out long count2))
            {
                newStones[secondStone] = count2 + stone.Value;
            }
            else
            {
                newStones.Add(secondStone, stone.Value);
            }
        }
        else
        {
            var newKey = stone.Key * 2024;
            if (newStones.TryGetValue(newKey, out var count))
            {
                newStones[newKey] = count + stone.Value;
            }
            else
            {
                newStones.Add(newKey, stone.Value);
            }
        }

        if (newStones.TryGetValue(stone.Key, out var value))
        {
            if (value == stone.Value)
            {
                newStones.Remove(stone.Key);
            }
            else
            {
                newStones[stone.Key] = value - stone.Value;
            }
        }
    }

    stones = newStones;

    if (i == 24)
    {
        Console.WriteLine("Part 1: " + stones.Sum(x => x.Value));
    }
}

Console.WriteLine("Part 2: " + stones.Sum(x => x.Value));
Console.ReadKey();