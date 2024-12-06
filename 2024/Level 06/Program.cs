using System.Drawing;

string[] input = File.ReadAllLines("input.txt");

var board = new Dictionary<Point, char>(
                from y in Enumerable.Range(0, input.Length)
                from x in Enumerable.Range(0, input[0].Length)
                select new KeyValuePair<Point, char>(new Point(x, y), input[y][x]));

var start = board.First(x => x.Value == '^');
HashSet<VisitedPosition> resultPositions = [];
resultPositions.Add(new VisitedPosition(start.Key, Direction.Up));

var currentPoint = resultPositions.First();
while (true)
{
    currentPoint = GetNextPosition(currentPoint, board);

    if (currentPoint == null || resultPositions.Contains(currentPoint))
    {
        break;
    }

    resultPositions.Add(currentPoint);
}

Console.WriteLine("Part1: " + resultPositions.Select(x => x.Point).Distinct().Count());

int maxX = board.Max(x => x.Key.X);
int maxY = board.Max(x => x.Key.Y);
long result = 0;
for (int y = 0; y < maxY; y++)
{
    for (int x = 0; x < maxX; x++)
    {
        var additionalPoint = new Point(x, y);
        resultPositions.Clear();
        resultPositions.Add(new VisitedPosition(start.Key, Direction.Up));

        var currentPoint2 = resultPositions.First();
        while (true)
        {
            currentPoint2 = GetNextPosition(currentPoint2, board, additionalPoint);

            if (currentPoint2 == null)
            {
                break;
            }
            else if (resultPositions.Contains(currentPoint2))
            {
                result++;
                break;
            }

            resultPositions.Add(currentPoint2);
        }
    }
}

Console.WriteLine("Part2: " + result);
Console.ReadKey();

static VisitedPosition? GetNextPosition(VisitedPosition currentPoint, Dictionary<Point, char> board, Point? additionalPoint = null)
{
    var nextPosition = new VisitedPosition(NextPoint(currentPoint.Point, currentPoint.Direction), currentPoint.Direction);

    if (board.TryGetValue(nextPosition.Point, out char value))
    {
        if (value == '#' || nextPosition.Point == additionalPoint)
        {
            nextPosition = GetNextPosition(currentPoint with { Direction = Turn(currentPoint.Direction) }, board, additionalPoint);
        }
    }
    else
    {
        return null;
    }

    return nextPosition;
}

static Point NextPoint(Point currentPoint, Direction direction)
{
    return direction switch
    {
        Direction.Up => currentPoint with { Y = currentPoint.Y - 1 },
        Direction.Down => currentPoint with { Y = currentPoint.Y + 1 },
        Direction.Right => currentPoint with { X = currentPoint.X + 1 },
        Direction.Left => currentPoint with { X = currentPoint.X - 1 },
        _ => throw new Exception(),
    };
}

static Direction Turn(Direction currDirection)
{
    return currDirection switch
    {
        Direction.Up => Direction.Right,
        Direction.Down => Direction.Left,
        Direction.Right => Direction.Down,
        Direction.Left => Direction.Up,
        _ => throw new Exception(),
    };
}

record VisitedPosition(Point Point, Direction Direction);

enum Direction
{
    Up,
    Down,
    Right,
    Left,
}