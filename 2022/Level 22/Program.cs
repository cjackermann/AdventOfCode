string[] input = File.ReadAllLines("input.txt");
string tmpActions = File.ReadAllText("actions.txt");

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

string tmpStr = string.Empty;
List<string> actions = new();
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

Point currentPoint = points.First();
Direction direction = Direction.Right;

foreach (var action in actions)
{
    if (int.TryParse(action, out int steps))
    {
        for (int i = 0; i < steps; i++)
        {
            if (direction == Direction.Right)
            {
                var nextPoint = points.FirstOrDefault(d => d.X == currentPoint.X + 1 && d.Y == currentPoint.Y);
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
                    var firstPointOfRow = points.Where(d => d.Y == currentPoint.Y).OrderBy(d => d.X).FirstOrDefault();
                    if (firstPointOfRow.Wall)
                    {
                        break;
                    }

                    currentPoint = firstPointOfRow;
                }
            }
            else if (direction == Direction.Down)
            {
                var nextPoint = points.FirstOrDefault(d => d.X == currentPoint.X && d.Y == currentPoint.Y + 1);
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
                    var firstPointOfColumn = points.Where(d => d.X == currentPoint.X).OrderBy(d => d.Y).FirstOrDefault();
                    if (firstPointOfColumn.Wall)
                    {
                        break;
                    }

                    currentPoint = firstPointOfColumn;
                }
            }
            else if (direction == Direction.Left)
            {
                var nextPoint = points.FirstOrDefault(d => d.X == currentPoint.X - 1 && d.Y == currentPoint.Y);
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
                    var lastPointOfRow = points.Where(d => d.Y == currentPoint.Y).OrderByDescending(d => d.X).FirstOrDefault();
                    if (lastPointOfRow.Wall)
                    {
                        break;
                    }

                    currentPoint = lastPointOfRow;
                }
            }
            else if (direction == Direction.Up)
            {
                var nextPoint = points.FirstOrDefault(d => d.X == currentPoint.X && d.Y == currentPoint.Y - 1);
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
                    var lastPointOfColumn = points.Where(d => d.X == currentPoint.X).OrderByDescending(d => d.Y).FirstOrDefault();
                    if (lastPointOfColumn.Wall)
                    {
                        break;
                    }

                    currentPoint = lastPointOfColumn;
                }
            }
        }
    }
    else if (action == "R")
    {
        if (direction == Direction.Right)
        {
            direction = Direction.Down;
        }
        else if (direction == Direction.Down)
        {
            direction = Direction.Left;
        }
        else if (direction == Direction.Left)
        {
            direction = Direction.Up;
        }
        else if (direction == Direction.Up)
        {
            direction = Direction.Right;
        }
    }
    else if (action == "L")
    {
        if (direction == Direction.Right)
        {
            direction = Direction.Up;
        }
        else if (direction == Direction.Down)
        {
            direction = Direction.Right;
        }
        else if (direction == Direction.Left)
        {
            direction = Direction.Down;
        }
        else if (direction == Direction.Up)
        {
            direction = Direction.Left;
        }
    }
}

int rowResult = 1000 * currentPoint.Y;
int columnResult = 4 * currentPoint.X;
int facingResult = (int)direction;

Console.WriteLine("Stage 1: " + (rowResult + columnResult + facingResult));
Console.ReadKey();

public record Point(int X, int Y, bool Wall);

public enum Direction
{
    Right = 0,
    Down = 1,
    Left = 2,
    Up = 3,
}