string[] input = File.ReadAllLines("input.txt");
string tmpActions = File.ReadAllText("actions.txt");

List<string> actions = GetActions(tmpActions);
HashSet<Point> points = GetPoints(input);

var (CurrentPoint, CurrentDirection) = PartOne(actions, points);
int rowResult = 1000 * (CurrentPoint.Y + 1);
int columnResult = 4 * (CurrentPoint.X + 1);
int facingResult = (int)CurrentDirection;
Console.WriteLine("Stage 1: " + (rowResult + columnResult + facingResult));

(CurrentPoint, CurrentDirection) = PartTwo(actions, points);
rowResult = 1000 * (CurrentPoint.Y + 1);
columnResult = 4 * (CurrentPoint.X + 1);
facingResult = (int)CurrentDirection;
Console.WriteLine("Stage 2: " + (rowResult + columnResult + facingResult));

static (Point CurrentPoint, Direction CurrentDirection) PartOne(List<string> actions, HashSet<Point> points)
{
    Point currentPoint = points.First();
    Direction direction = Direction.Right;

    foreach (var action in actions)
    {
        if (int.TryParse(action, out int steps))
        {
            for (int i = 0; i < steps; i++)
            {
                Point nextPoint = null;
                if (direction == Direction.Right)
                {
                    nextPoint = points.FirstOrDefault(d => d.X == currentPoint.X + 1 && d.Y == currentPoint.Y);
                }
                else if (direction == Direction.Down)
                {
                    nextPoint = points.FirstOrDefault(d => d.X == currentPoint.X && d.Y == currentPoint.Y + 1);
                }
                else if (direction == Direction.Left)
                {
                    nextPoint = points.FirstOrDefault(d => d.X == currentPoint.X - 1 && d.Y == currentPoint.Y);
                }
                else if (direction == Direction.Up)
                {
                    nextPoint = points.FirstOrDefault(d => d.X == currentPoint.X && d.Y == currentPoint.Y - 1);
                }

                if (nextPoint != null)
                {
                    if (nextPoint.Wall)
                    {
                        break;
                    }

                    currentPoint = nextPoint;
                }
                else
                {
                    if (direction == Direction.Right)
                    {
                        nextPoint = points.Where(d => d.Y == currentPoint.Y).OrderBy(d => d.X).FirstOrDefault();
                    }
                    else if (direction == Direction.Down)
                    {
                        nextPoint = points.Where(d => d.X == currentPoint.X).OrderBy(d => d.Y).FirstOrDefault();
                    }
                    else if (direction == Direction.Left)
                    {
                        nextPoint = points.Where(d => d.Y == currentPoint.Y).OrderByDescending(d => d.X).FirstOrDefault(); ;
                    }
                    else if (direction == Direction.Up)
                    {
                        nextPoint = points.Where(d => d.X == currentPoint.X).OrderByDescending(d => d.Y).FirstOrDefault();
                    }

                    if (nextPoint.Wall)
                    {
                        break;
                    }

                    currentPoint = nextPoint;
                }
            }
        }
        else if (action == "R")
        {
            direction = (int)direction == 3 ? 0 : direction + 1;
        }
        else if (action == "L")
        {
            direction = direction == 0 ? Direction.Up : direction - 1;
        }
    }

    return (currentPoint, direction);
}

static (Point CurrentPoint, Direction CurrentDirection) PartTwo(List<string> actions, HashSet<Point> points)
{
    var map = new Dictionary<Point, (Point Point, Direction NewDirection)>();

    for (int i = 0; i < 50; i++)
    {
        // 1 top to 6 left (up to right)
        map[new Point(50 + i, -1)] = (new Point(0, 150 + i), Direction.Right);
        map[new Point(-1, 150 + i)] = (new Point(50 + i, 0), Direction.Down);

        // 1 left to 5 left (left to right)
        map[new Point(49, i)] = (new Point(0, 149 - i), Direction.Right);
        map[new Point(-1, 100 + i)] = (new Point(50, 49 - i), Direction.Right);

        // 2 top to 6 bottom (up to up)
        map[new Point(100 + i, -1)] = (new Point(i, 199), Direction.Up);
        map[new Point(i, 200)] = (new Point(100 + i, 0), Direction.Down);

        // 2 right to 4 right (right to left)
        map[new Point(150, i)] = (new Point(99, 149 - i), Direction.Left);
        map[new Point(100, 100 + i)] = (new Point(149, 49 - i), Direction.Left);

        // 2 bottom to 3 right (down to left)
        map[new Point(100 + i, 50)] = (new Point(99, 50 + i), Direction.Left);
        map[new Point(100, 50 + i)] = (new Point(100 + i, 49), Direction.Up);

        // 3 left to 5 top (left to down)
        map[new Point(49, 50 + i)] = (new Point(i, 100), Direction.Down);
        map[new Point(i, 99)] = (new Point(50, 50 + i), Direction.Right);

        // 4 bottom to 6 right (down to left)
        map[new Point(50 + i, 150)] = (new Point(49, 150 + i), Direction.Left);
        map[new Point(50, 150 + i)] = (new Point(50 + i, 149), Direction.Up);
    }

    var currentPoint = points.First();
    var direction = Direction.Right;

    foreach (var action in actions)
    {
        if (int.TryParse(action, out int steps))
        {
            for (int i = 0; i < steps; i++)
            {
                Point nextPoint = null;

                if (direction == Direction.Right)
                {
                    nextPoint = currentPoint with { X = currentPoint.X + 1 };
                }
                else if (direction == Direction.Down)
                {
                    nextPoint = currentPoint with { Y = currentPoint.Y + 1 };
                }
                else if (direction == Direction.Left)
                {
                    nextPoint = currentPoint with { X = currentPoint.X - 1 };
                }
                else if (direction == Direction.Up)
                {
                    nextPoint = currentPoint with { Y = currentPoint.Y - 1 };
                }

                Direction? tmpNewDirection = null;
                if (map.TryGetValue(nextPoint, out (Point Point, Direction NewDirection) value))
                {
                    nextPoint = value.Point;
                    tmpNewDirection = value.NewDirection;
                }

                nextPoint = points.FirstOrDefault(d => d.X == nextPoint.X && d.Y == nextPoint.Y);
                if (nextPoint.Wall)
                {
                    break;
                }
                else
                {
                    currentPoint = nextPoint;
                    direction = tmpNewDirection ?? direction;
                }
            }
        }
        else if (action == "R")
        {
            direction = (int)direction == 3 ? 0 : direction + 1;
        }
        else if (action == "L")
        {
            direction = direction == 0 ? Direction.Up : direction - 1;
        }
    }

    return (currentPoint, direction);
}

static List<string> GetActions(string tmpActions)
{
    List<string> actions = new();

    string tmpStr = string.Empty;
    foreach (var charakter in tmpActions)
    {
        if (char.IsNumber(charakter))
        {
            tmpStr += charakter;
        }
        else
        {
            actions.Add(tmpStr);
            tmpStr = string.Empty;
            actions.Add(charakter.ToString());
        }
    }

    actions.Add(tmpStr);
    return actions;
}

static HashSet<Point> GetPoints(string[] input)
{
    HashSet<Point> points = new();
    for (int y = 0; y < input.Length; y++)
    {
        for (int x = 0; x < input[y].Length; x++)
        {
            if (input[y][x] == '#')
            {
                points.Add(new Point(x, y, true));
            }
            else if (input[y][x] == '.')
            {
                points.Add(new Point(x, y, false));
            }
        }
    }

    return points;
}

public record Point(int X, int Y, bool Wall = false);

public enum Direction
{
    Right = 0,
    Down = 1,
    Left = 2,
    Up = 3,
}