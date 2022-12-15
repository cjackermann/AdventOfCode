string[] input = File.ReadAllLines("input.txt");

IEnumerable<(Point Sensor, Point Beacon)> data = from line in input
                                                 let parts = line.Split(new string[] { "Sensor at x=", ", y=", ": closest beacon is at x=", ", y=" }, StringSplitOptions.RemoveEmptyEntries)
                                                 let sensor = new Point(int.Parse(parts[0]), int.Parse(parts[1]))
                                                 let beacon = new Point(int.Parse(parts[2]), int.Parse(parts[3]))
                                                 select (sensor, beacon);

HashSet<Point> empty = new();
HashSet<Point> sensors = new();
HashSet<Point> beacons = new();
foreach (var pair in data)
{
    sensors.Add(pair.Sensor);
    beacons.Add(pair.Beacon);

    var xDiff = Math.Abs(pair.Sensor.X - pair.Beacon.X);
    var yDiff = Math.Abs(pair.Sensor.Y - pair.Beacon.Y);
    var together = xDiff + yDiff;

    var leftPoint = new Point(pair.Sensor.X - together, pair.Sensor.Y);
    var rightPoint = new Point(pair.Sensor.X + together, pair.Sensor.Y);
    var downPoint = new Point(pair.Sensor.X, pair.Sensor.Y + together);
    var upPoint = new Point(pair.Sensor.X, pair.Sensor.Y - together);

    int step = 0;
    for (int y = pair.Sensor.Y; y <= pair.Sensor.Y + together; y++)
    {
        if (y == 2000000)
        {
            for (int x = pair.Sensor.X - together + step; x <= pair.Sensor.X + together - step; x++)
            {
                empty.Add(new Point(x, y));
            }
        }

        step++;
    }

    step = 0;
    for (int y = pair.Sensor.Y; y >= pair.Sensor.Y - together; y--)
    {
        if (y == 2000000)
        {
            for (int x = pair.Sensor.X - together + step; x <= pair.Sensor.X + together - step; x++)
            {
                empty.Add(new Point(x, y));
            }
        }

        step++;
    }
}

var result = empty.Union(sensors.Where(d => d.Y == 2000000)).ToList();
result.RemoveAll(beacons.Contains);

Console.WriteLine(result.Count);
Console.ReadKey();

public record Point(int X, int Y);