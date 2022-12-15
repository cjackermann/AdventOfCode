string[] input = File.ReadAllLines("input.txt");

IEnumerable<(Point Sensor, Point Beacon)> data = from line in input
                                                 let parts = line.Split(new string[] { "Sensor at x=", ", y=", ": closest beacon is at x=", ", y=" }, StringSplitOptions.RemoveEmptyEntries)
                                                 let sensor = new Point(int.Parse(parts[0]), int.Parse(parts[1]))
                                                 let beacon = new Point(int.Parse(parts[2]), int.Parse(parts[3]))
                                                 select (sensor, beacon);
long maxXY = 4000000;
for (int currentY = 0; currentY <= maxXY; currentY++)
{
    var intersections = data.Select(d => GetIntersection(currentY, d)).Where(d => d != null).Select(d => d.Value).OrderBy(d => d.xStart);

    long currentX = 0;
    long? result = null;
    foreach (var (xStart, xEnd) in intersections)
    {
        if (xStart > currentX)
        {
            result = currentX * 4000000 + currentY;
            break;
        }
        else if (currentX <= xEnd)
        {
            currentX = xEnd + 1;
        }
    }

    if (result != null)
    {
        Console.WriteLine(result);
        break;
    }
}

Console.ReadKey();

static (int xStart, int xEnd)? GetIntersection(int currentY, (Point Sensor, Point Beacon) pair)
{
    var xDiff = Math.Abs(pair.Sensor.X - pair.Beacon.X);
    var yDiff = Math.Abs(pair.Sensor.Y - pair.Beacon.Y);
    var distanceSensorToBeacon = xDiff + yDiff;

    var distanceToY = Math.Abs(currentY - pair.Sensor.Y);
    if (distanceToY > distanceSensorToBeacon)
    {
        return null;
    }
    else
    {
        var overlapLength = distanceSensorToBeacon - distanceToY;
        return (pair.Sensor.X - overlapLength, pair.Sensor.X + overlapLength);
    }
}

public record Point(int X, int Y);