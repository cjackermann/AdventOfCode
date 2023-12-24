using Level_24;

string[] lines = File.ReadAllLines("input.txt");

var hailstones = new List<Hailstone>();
foreach (string line in lines)
{
    var parts = line.Split(" @ ");

    var coordinates = parts[0].Split(", ");
    var px = long.Parse(coordinates[0]);
    var py = long.Parse(coordinates[1]);
    var pz = long.Parse(coordinates[2]);

    var velocities = parts[1].Split(", ");
    var vx = long.Parse(velocities[0]);
    var vy = long.Parse(velocities[1]);
    var vz = long.Parse(velocities[2]);

    var currentPosition = new Hailstone(new PositionXYZ(px, py, pz), new PositionXYZ(vx, vy, vz));
    var nextPosition = currentPosition.Move();

    var slope = (nextPosition.Position.Y - currentPosition.Position.Y) / (nextPosition.Position.X - currentPosition.Position.X);
    var intersect = nextPosition.Position.Y - slope * nextPosition.Position.X;

    hailstones.Add(currentPosition with { Slope = slope, Intersect = intersect }); ;
}

const long minTest = 200000000000000;
const long maxTest = 400000000000000;
const int b = -1;

var intersections = 0;
var visited = new HashSet<Hailstone>();
foreach (var hailstoneA in hailstones)
{
    visited.Add(hailstoneA);

    foreach (var hailstoneB in hailstones.Where(h => !visited.Contains(h)))
    {
        var x2 = hailstoneB.Position.X;
        var y2 = hailstoneB.Position.Y;
        var vx2 = hailstoneB.Velocity.X;
        var vy2 = hailstoneB.Velocity.Y;

        if (hailstoneA.Slope == hailstoneB.Slope)
        {
            continue; // Parallel
        }

        var x = (b * hailstoneB.Intersect - b * hailstoneA.Intersect) / (hailstoneA.Slope * b - hailstoneB.Slope * b);
        var y = (hailstoneB.Slope * hailstoneA.Intersect - hailstoneA.Slope * hailstoneB.Intersect) / (hailstoneA.Slope * b - hailstoneB.Slope * b);

        if (x - hailstoneA.Position.X < 0 != hailstoneA.Velocity.X < 0 || y - hailstoneA.Position.Y < 0 != hailstoneA.Velocity.Y < 0)
        {
            continue; // Opposite to first's direction
        }

        if (x - x2 < 0 != vx2 < 0 || y - y2 < 0 != vy2 < 0)
        {
            continue; // Opposite to second's direction
        }

        var intersectionPoint = new PositionXYZ(x, y);
        if (intersectionPoint.CheckBounds(minTest, maxTest))
        {
            intersections++;
        }
    }
}

Console.WriteLine("Part 1: " + intersections);
Console.ReadKey();