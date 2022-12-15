string[] input = File.ReadAllLines("input.txt");

IEnumerable<(Point Sensor, Point Beacon)> data = from line in input
                                                 let parts = line.Split(new string[] { "Sensor at x=", ", y=", ": closest beacon is at x=", ", y=" }, StringSplitOptions.RemoveEmptyEntries)
                                                 let sensor = new Point(int.Parse(parts[0]), int.Parse(parts[1]))
                                                 let beacon = new Point(int.Parse(parts[2]), int.Parse(parts[3]))
                                                 select (sensor, beacon);
long maxCoordinates = 4000000;
long? result = null;

for (int currentY = 0; currentY <= maxCoordinates; currentY++)
{
    long currentX = 0;

    var intersections = data.Select(d => GetIntersection(currentY, d)).Where(d => d != null).Select(d => d.Value).OrderBy(d => d.Start);
    foreach (var (Start, End) in intersections)
    {
        if (Start > currentX)
        {
            result = currentX * 4000000 + currentY;
            break;
        }
        else if (currentX <= End)
        {
            currentX = End + 1;
        }
    }

    if (result != null)
    {
        break;
    }
}

Console.WriteLine(result);
Console.ReadKey();

static (long Start, long End)? GetIntersection(long currentY, (Point Sensor, Point Beacon) pair)
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