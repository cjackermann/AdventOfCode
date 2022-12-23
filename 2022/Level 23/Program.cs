string[] input = File.ReadAllLines("input.txt");

List<Point> elfes = new();
for (int y = 0; y < input.Length; y++)
{
    for (int x = 0; x < input[0].Length; x++)
    {
        if (input[y][x] == '#')
        {
            elfes.Add(new Point(x, y));
        }
    }
}

var result = PartOne(elfes.ToHashSet());
int xMin = result.Min(d => d.X);
int xMax = result.Max(d => d.X) + 1;
int yMin = result.Min(d => d.Y);
int yMax = result.Max(d => d.Y) + 1;
Console.WriteLine("Stage 1: " + ((xMax - xMin) * (yMax - yMin) - result.Count));

int maxRound = PartTwo(elfes.ToHashSet());
Console.WriteLine("Stage 2: " + maxRound);

Console.ReadKey();

HashSet<Point> PartOne(HashSet<Point> elfes)
{
    Direction direction = Direction.North;
    for (int i = 0; i < 10; i++)
    {
        var possibleNextPositions = new Dictionary<Point, Point>();
        foreach (var elfe in elfes)
        {
            CheckNextMove(direction, elfes, elfe, possibleNextPositions);
        }

        var singlePoints = possibleNextPositions.GroupBy(d => d.Value).Where(d => d.Count() == 1).ToDictionary(d => d.First().Key, d => d.Key);
        foreach (var proposal in singlePoints)
        {
            elfes.Remove(proposal.Key);
            elfes.Add(proposal.Value);
        }

        direction = direction == Direction.East ? Direction.North : direction + 1;
    }

    return elfes;
}

int PartTwo(HashSet<Point> elfes)
{
    Direction direction = Direction.North;
    int result = 0;

    while (true)
    {
        result++;
        var possibleNextPositions = new Dictionary<Point, Point>();
        foreach (var elfe in elfes)
        {
            CheckNextMove(direction, elfes, elfe, possibleNextPositions);
        }

        var singlePoints = possibleNextPositions.GroupBy(d => d.Value).Where(d => d.Count() == 1).ToDictionary(d => d.First().Key, d => d.Key);
        if (singlePoints.Count == 0)
        {
            break;
        }

        foreach (var proposal in singlePoints)
        {
            elfes.Remove(proposal.Key);
            elfes.Add(proposal.Value);
        }

        direction = direction == Direction.East ? Direction.North : direction + 1;
    }

    return result;
}

void CheckNextMove(Direction direction, HashSet<Point> elfes, Point elfe, Dictionary<Point, Point> possibleNextPositions)
{
    var neighbours = GetNeighbours(elfe, elfes).ToList();
    if (!neighbours.Any())
    {
        return;
    }

    Point nextPosition = null;
    for (int move = (int)direction; move < (int)direction + 4; move++)
    {
        if (move % 4 == 0 && !neighbours.Any(d => d == Direction.North))
        {
            nextPosition = elfe with { Y = elfe.Y - 1 };
        }
        else if (move % 4 == 1 && !neighbours.Any(d => d == Direction.South))
        {
            nextPosition = elfe with { Y = elfe.Y + 1 };
        }
        else if (move % 4 == 2 && !neighbours.Any(d => d == Direction.West))
        {
            nextPosition = elfe with { X = elfe.X - 1 };
        }
        else if (move % 4 == 3 && !neighbours.Any(d => d == Direction.East))
        {
            nextPosition = elfe with { X = elfe.X + 1 };
        }

        if (nextPosition != null)
        {
            break;
        }
    }

    if (nextPosition != null)
    {
        possibleNextPositions.Add(elfe, nextPosition);
    }
}

static IEnumerable<Direction> GetNeighbours(Point elve, HashSet<Point> elfes)
{
    if (elfes.Contains(new Point(elve.X, elve.Y - 1)) || elfes.Contains(new Point(elve.X + 1, elve.Y - 1)) || elfes.Contains(new Point(elve.X - 1, elve.Y - 1)))
    {
        yield return Direction.North;
    }

    if (elfes.Contains(new Point(elve.X, elve.Y + 1)) || elfes.Contains(new Point(elve.X + 1, elve.Y + 1)) || elfes.Contains(new Point(elve.X - 1, elve.Y + 1)))
    {
        yield return Direction.South;
    }

    if (elfes.Contains(new Point(elve.X - 1, elve.Y)) || elfes.Contains(new Point(elve.X - 1, elve.Y + 1)) || elfes.Contains(new Point(elve.X - 1, elve.Y - 1)))
    {
        yield return Direction.West;
    }

    if (elfes.Contains(new Point(elve.X + 1, elve.Y)) || elfes.Contains(new Point(elve.X + 1, elve.Y + 1)) || elfes.Contains(new Point(elve.X + 1, elve.Y - 1)))
    {
        yield return Direction.East;
    }
}

public record Point(int X, int Y);

public enum Direction
{
    North = 0,
    South = 1,
    West = 2,
    East = 3,
}