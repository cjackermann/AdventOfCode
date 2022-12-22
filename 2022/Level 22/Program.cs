string[] input = File.ReadAllLines("input.txt");
string tmpActions = File.ReadAllText("actions.txt");

List<string> actions = GetActions(tmpActions);
HashSet<Point> points = GetPoints(input);

var partOneResult = PartOne(actions, points);

int rowResult = 1000 * partOneResult.CurrentPoint.Y;
int columnResult = 4 * partOneResult.CurrentPoint.X;
int facingResult = (int)partOneResult.CurrentDirection;

Console.WriteLine("Stage 1: " + (rowResult + columnResult + facingResult));

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
                points.Add(new Point(x + 1, y + 1, true));
            }
            else if (input[y][x] == '.')
            {
                points.Add(new Point(x + 1, y + 1, false));
            }
        }
    }

    return points;
}

public record Point(int X, int Y, bool Wall);

public enum Direction
{
    Right = 0,
    Down = 1,
    Left = 2,
    Up = 3,
}