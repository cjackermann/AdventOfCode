string[] input = File.ReadAllLines("input.txt");

var data = (from line in input
            where !string.IsNullOrWhiteSpace(line)
            select line.Split(" -> ").Select(x => new Point(x.Split(",").Select(int.Parse).First(), x.Split(",").Select(int.Parse).Last())).ToList()).ToList();

var yMax = data.SelectMany(d => d).Max(d => d.Y);
yMax += 2; // Part 2 instructions

HashSet<Point> rocksAndSands = new();
foreach (var points in data)
{
    var startPoint = points.First();
    rocksAndSands.Add(startPoint);

    foreach (var point in points.Skip(1))
    {
        int towardsX = 0;
        if (point.X < startPoint.X)
        {
            towardsX = -1;
        }
        else if (point.X > startPoint.X)
        {
            towardsX = 1;
        }

        int towardsY = 0;
        if (point.Y < startPoint.Y)
        {
            towardsY = -1;
        }
        else if (point.Y > startPoint.Y)
        {
            towardsY = 1;
        }

        while (startPoint != point)
        {
            startPoint = startPoint with { X = startPoint.X + towardsX, Y = startPoint.Y + towardsY };
            rocksAndSands.Add(startPoint);
        }
    }
}

int result = 0;
Point currentPoint = new Point(500, 0);

while (true)
{
    Point newPoint = null;

    if (!rocksAndSands.Contains(new Point(currentPoint.X, currentPoint.Y + 1)))
    {
        newPoint = currentPoint with { Y = currentPoint.Y + 1 };
    }
    else if (!rocksAndSands.Contains(new Point(currentPoint.X - 1, currentPoint.Y + 1)))
    {
        newPoint = currentPoint with { X = currentPoint.X - 1, Y = currentPoint.Y + 1 };
    }
    else if (!rocksAndSands.Contains(new Point(currentPoint.X + 1, currentPoint.Y + 1)))
    {
        newPoint = currentPoint with { X = currentPoint.X + 1, Y = currentPoint.Y + 1 };
    }

    if (newPoint == null && currentPoint == new Point(500, 0))
    {
        result++;
        Console.WriteLine(result);
        break;
    }

    if (newPoint == null || newPoint?.Y == yMax)
    {
        result++;
        rocksAndSands.Add(currentPoint);
        currentPoint = new Point(500, 0);
    }
    else
    {
        currentPoint = newPoint;
    }
}

public record Point(int X, int Y);