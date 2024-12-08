string[] input = File.ReadAllLines("input.txt");

var antennas = new Dictionary<char, List<Position>>();

for (int y = 0; y < input.Length; y++)
{
    for (int x = 0; x < input.Length; x++)
    {
        var position = input[y][x];
        if (position != '.')
        {
            if (!antennas.ContainsKey(position))
            {
                antennas[position] = [];
            }

            antennas[position].Add(new Position(y, x));
        }
    }
}

Part1(antennas, input.Length);
Part2(antennas, input.Length);

Console.ReadKey();

static void Part1(Dictionary<char, List<Position>> antennas, int maxGridSize)
{
    var antinodes = new HashSet<Position>();

    foreach (var frequency in antennas.Keys)
    {
        var locations = antennas[frequency];

        foreach (var pos1 in locations)
        {
            foreach (var pos2 in locations)
            {
                if (pos1 == pos2)
                {
                    continue;
                }

                var yDiff = pos1.Y - pos2.Y;
                var xDiff = pos1.X - pos2.X;

                Position antinode1 = pos1.Move(yDiff, xDiff);
                if (!antinode1.IsOutOfBounds(maxGridSize))
                {
                    antinodes.Add(antinode1);
                }

                Position antinode2 = pos2.Move(-yDiff, -xDiff);
                if (!antinode2.IsOutOfBounds(maxGridSize))
                {
                    antinodes.Add(antinode2);
                }
            }
        }
    }

    Console.WriteLine("Part 1:" + antinodes.Count);
}

static void Part2(Dictionary<char, List<Position>> antennas, int maxGridSize)
{
    var antinodes = new HashSet<Position>();

    foreach (var frequency in antennas.Keys)
    {
        var locations = antennas[frequency];

        foreach (var pos1 in locations)
        {
            foreach (var pos2 in locations)
            {
                if (pos1 == pos2)
                {
                    continue;
                }

                var yDiff = pos1.Y - pos2.Y;
                var xDiff = pos1.X - pos2.X;

                Position antinode1 = pos1.Move(yDiff, xDiff);
                while (!antinode1.IsOutOfBounds(maxGridSize))
                {
                    antinodes.Add(antinode1);
                    antinode1 = antinode1.Move(yDiff, xDiff);
                }

                Position antinode2 = pos2.Move(-yDiff, -xDiff);
                while (!antinode2.IsOutOfBounds(maxGridSize))
                {
                    antinodes.Add(antinode2);
                    antinode2 = antinode2.Move(-yDiff, -xDiff);
                }

                antinodes.Add(pos1);
                antinodes.Add(pos2);
            }
        }
    }

    Console.WriteLine("Part 2:" + antinodes.Count);
}

record Position(int Y, int X)
{
    public Position Move(int y, int x) => new Position(Y + y, X + x);

    public bool IsOutOfBounds(int size) => Y < 0 || Y >= size || X < 0 || X >= size;
}