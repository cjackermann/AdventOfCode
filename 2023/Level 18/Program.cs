string[] input = File.ReadAllLines("input.txt");

(List<Point> outerPoints1, long totalEdgeLength1) = GetPoints(input, true);
Console.WriteLine("Part 1: " + Calculate(outerPoints1, totalEdgeLength1));

(List<Point> outerPoints2, long totalEdgeLength2) = GetPoints(input, false);
Console.WriteLine("Part 2: " + Calculate(outerPoints2, totalEdgeLength2));

Console.ReadKey();

static (List<Point>, long) GetPoints(string[] input, bool part1)
{
    long totalEdgeLength = 0;
    long currentX = 0;
    long currentY = 0;
    var outerPoints = new List<Point>() { };

    foreach (var line in input)
    {
        string direction;
        int distance;

        if (part1)
        {
            var split = line.Split();
            direction = split[0];
            distance = int.Parse(split[1]);
        }
        else
        {
            var rightSide = line.Split()[2];
            var tmp = string.Join("", rightSide.Remove(rightSide.Length - 1).Remove(0, 2));
            direction = tmp.Substring(tmp.Length - 1);
            distance = Convert.ToInt32(tmp.Substring(0, tmp.Length - 1), 16);
        }

        totalEdgeLength += distance;

        switch (direction)
        {
            case "R" or "0": currentX += distance; break;
            case "D" or "1": currentY += distance; break;
            case "L" or "2": currentX -= distance; break;
            case "U" or "3": currentY -= distance; break;
        }

        outerPoints.Add(new Point(currentX, currentY));
    }

    return (outerPoints, totalEdgeLength);
}

static long Calculate(List<Point> points, long totalEdgeLength) // shoelace formula from internet
{
    long area = 0;
    for (var i = 0; i < points.Count - 1; ++i)
    {
        area += (points[i].Y + points[i + 1].Y) * (points[i].X - points[i + 1].X);
    }
    area += (points[^1].Y + points[0].Y) * (points[^1].X - points[0].X);
    area += totalEdgeLength;
    area >>= 1;

    return area + 1;
}

record Point(long X, long Y);