var actions = File.ReadAllText("input.txt").ToArray();
string[] tmpRocks = File.ReadAllText("input2.txt").Split("\r\n\r\n");

var rocks = new List<IEnumerable<Point>>();
foreach (var rock in tmpRocks)
{
    var tmpRock = new List<Point>();
    var data = rock.Split("\r\n").Reverse().ToArray();
    for (int y = 0; y < data.Length; y++)
    {
        for (int x = 0; x < data[0].Length; x++)
        {
            if (data[y][x] == '#')
            {
                tmpRock.Add(new Point(x, y));
            }
        }
    }

    rocks.Add(tmpRock);
}

PartOne(actions, rocks);

static void PartOne(char[] actions, List<IEnumerable<Point>> rocks)
{
    HashSet<Point> stones = new(Enumerable.Range(0, 7).Select(d => new Point(d, 0)));
    long maxPoint = 0;
    int actionIndex = 0;
    long rockCount = 0;

    while (rockCount < 2022)
    {
        int rockIndex = Convert.ToInt32(rockCount % 5);
        var rock = rocks[rockIndex].Select(d => d with { X = d.X + 2, Y = d.Y + maxPoint + 4 });

        while (true)
        {
            if (actions[actionIndex] == '>')
            {
                rock = ShiftRight(rock);
                if (stones.Intersect(rock).Any())
                {
                    rock = ShiftLeft(rock);
                }
            }
            else
            {
                rock = ShiftLeft(rock);
                if (stones.Intersect(rock).Any())
                {
                    rock = ShiftRight(rock);
                }
            }

            actionIndex = (actionIndex + 1) % actions.Length;
            rock = ShiftDown(rock);

            if (stones.Intersect(rock).Any())
            {
                rock = ShiftUp(rock);
                foreach (var point in rock)
                {
                    stones.Add(point);
                }

                maxPoint = stones.Select(d => d.Y).Max();
                break;
            }
        }

        rockCount++;
    }

    Console.WriteLine(maxPoint);
}

static IEnumerable<Point> ShiftDown(IEnumerable<Point> rock) => rock.Select(d => d with { Y = d.Y - 1 });

static IEnumerable<Point> ShiftLeft(IEnumerable<Point> rock)
{
    if (rock.Any(d => d.X == 0))
    {
        return rock;
    }

    return rock.Select(d => d with { X = d.X - 1 });
}

static IEnumerable<Point> ShiftRight(IEnumerable<Point> rock)
{
    if (rock.Any(d => d.X == 6))
    {
        return rock;
    }

    return rock.Select(d => d with { X = d.X + 1 });
}

static IEnumerable<Point> ShiftUp(IEnumerable<Point> rock) => rock.Select(d => d with { Y = d.Y + 1 });

public record Point(long X, long Y);