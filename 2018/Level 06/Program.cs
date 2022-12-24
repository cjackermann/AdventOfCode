string[] input = File.ReadAllLines("input.txt");

var points = new Dictionary<Point, List<Point>>(from line in input
                                        let parts = line.Split(", ")
                                        select new KeyValuePair<Point, List<Point>>(new Point(int.Parse(parts[0]), int.Parse(parts[1])), new List<Point>()));

int xMin = points.Min(d => d.Key.X);
int xMax = points.Max(d => d.Key.X);
int yMin = points.Min(d => d.Key.Y);
int yMax = points.Max(d => d.Key.Y);

for (int y = yMin; y <= yMax; y++)
{
    for (int x = xMin; x <= xMax; x++)
    {
        Point currentPoint = new(x, y);

        (List<Point> Point, int Distance) bestPoint = (new List<Point>(), int.MaxValue);
        foreach (var point in points)
        {
            var xDiff = Math.Abs(point.Key.X - currentPoint.X);
            var yDiff = Math.Abs(point.Key.Y - currentPoint.Y);
            var distance = xDiff + yDiff;

            if (distance < bestPoint.Distance)
            {
                bestPoint = (new List<Point> { point.Key }, distance);
            }
            else if (bestPoint.Distance == distance)
            {
                bestPoint.Point.Add(point.Key);
            }
        }

        if (bestPoint.Point.Count == 1)
        {
            points[bestPoint.Point.First()].Add(currentPoint);
        }
    }
}

var result = points.Where(d => d.Value.All(x => x.X != xMin && x.X != xMax && x.Y != yMin && x.Y != yMax)).Select(d => d.Value.Count).OrderByDescending(d => d).First();
Console.WriteLine("Stage 1: " + result);

int counter = 0;
for (int y = yMin; y <= yMax; y++)
{
    for (int x = xMin; x <= xMax; x++)
    {
        Point currentPoint = new(x, y);

        int totalDistance = 0;
        foreach (var point in points)
        {
            var xDiff = Math.Abs(point.Key.X - currentPoint.X);
            var yDiff = Math.Abs(point.Key.Y - currentPoint.Y);
            var distance = xDiff + yDiff;

            totalDistance += distance;
        }

        if (totalDistance < 10000)
        {
            counter++;
        }
    }
}

Console.WriteLine("Stage 2: " + counter);

public record Point(int X, int Y);