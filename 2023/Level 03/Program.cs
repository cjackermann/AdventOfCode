using System.Drawing;

string[] input = File.ReadAllLines("input.txt");

var board = new Dictionary<Point, char>(
                from y in Enumerable.Range(0, input.Length)
                from x in Enumerable.Range(0, input[0].Length)
                select new KeyValuePair<Point, char>(new Point(x, y), input[y][x]));

int maxY = input.Length - 1;
int maxX = input[0].Length - 1;

PartOne(input);
PartTwo(input);

Console.ReadKey();

void PartOne(string[] input)
{
    var numbers = new List<Number>();
    var specialCharacters = new HashSet<Point>();

    for (int y = 0; y < input.Length; y++)
    {
        Number number = null;
        for (int x = 0; x < input[0].Length; x++)
        {
            var value = board[new Point(x, y)];
            if (char.IsNumber(value))
            {
                if (number == null)
                {
                    number = new Number();
                    numbers.Add(number);
                }

                number.Points.Add(new Point(x, y));
                number.Value += board[new Point(x, y)];
            }
            else if (value == '.')
            {
                number = null;
            }
            else
            {
                number = null;
                specialCharacters.Add(new Point(x, y));
            }
        }
    }

    var result = numbers.Where(x => x.GetAllNeighbours(maxX, maxY).Any(specialCharacters.Contains)).Sum(x => Convert.ToInt32(x.Value));
    Console.WriteLine("Part 1: " + result);
}

void PartTwo(string[] input)
{
    var numbers = new List<Number>();
    var gears = new HashSet<Point>();

    for (int y = 0; y < input.Length; y++)
    {
        Number number = null;
        for (int x = 0; x < input[0].Length; x++)
        {
            var value = board[new Point(x, y)];
            if (char.IsNumber(value))
            {
                if (number == null)
                {
                    number = new Number();
                    numbers.Add(number);
                }

                number.Points.Add(new Point(x, y));
                number.Value += board[new Point(x, y)];
            }
            else if (value == '.')
            {
                number = null;
            }
            else if (value == '*')
            {
                number = null;
                gears.Add(new Point(x, y));
            }
            else
            {
                number = null;
            }
        }
    }

    int result = 0;
    foreach (var gear in gears)
    {
        var gearNumbers = numbers.Where(x => x.GetAllNeighbours(maxX, maxY).Any(x => gear == x)).ToList();
        if (gearNumbers.Count > 1)
        {
            result += gearNumbers.Select(x => Convert.ToInt32(x.Value)).Aggregate((a, x) => a * x);
        }
    }

    Console.WriteLine("Part 2: " + result);
}

class Number
{
    public HashSet<Point> Points = new HashSet<Point>();

    public string Value;

    public HashSet<Point> GetAllNeighbours(int maxX, int maxY)
    {
        var hashi = new HashSet<Point>();
        foreach (var point in Points)
        {
            foreach (var neighbour in GetNeighbours(point))
            {
                if (neighbour.X > 0 && neighbour.Y > 0 && neighbour.X <= maxX && neighbour.Y <= maxY)
                {
                    hashi.Add(neighbour);
                }
            }
        }

        return hashi;
    }

    IEnumerable<Point> GetNeighbours(Point currentPoint)
    {
        yield return currentPoint with { Y = currentPoint.Y + 1 };
        yield return currentPoint with { Y = currentPoint.Y - 1 };

        yield return currentPoint with { X = currentPoint.X - 1 };
        yield return currentPoint with { X = currentPoint.X + 1 };

        yield return currentPoint with { Y = currentPoint.Y + 1, X = currentPoint.X + 1 };
        yield return currentPoint with { Y = currentPoint.Y + 1, X = currentPoint.X - 1 };
        yield return currentPoint with { Y = currentPoint.Y - 1, X = currentPoint.X + 1 };
        yield return currentPoint with { Y = currentPoint.Y - 1, X = currentPoint.X - 1 };
    }
}