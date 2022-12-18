string[] input = File.ReadAllLines("input.txt");

var cubes = from line in input
            let parts = line.Split(',')
            select new Point(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));

PartOne(cubes.ToHashSet());
PartTwo(cubes.ToHashSet());

static void PartOne(HashSet<Point> cubes)
{
    int result = 0;
    foreach (var cube in cubes)
    {
        if (!cubes.Contains(cube with { X = cube.X - 1 }))
        {
            result++;
        }
        if (!cubes.Contains(cube with { X = cube.X + 1 }))
        {
            result++;
        }
        if (!cubes.Contains(cube with { Y = cube.Y - 1 }))
        {
            result++;
        }
        if (!cubes.Contains(cube with { Y = cube.Y + 1 }))
        {
            result++;
        }
        if (!cubes.Contains(cube with { Z = cube.Z - 1 }))
        {
            result++;
        }
        if (!cubes.Contains(cube with { Z = cube.Z + 1 }))
        {
            result++;
        }
    }

    Console.WriteLine("Stage 1: " + result);
}

static void PartTwo(HashSet<Point> cubes)
{
    int result = 0;
    var dict = new Dictionary<Point, bool>();

    for (var x = cubes.Min(d => d.X); x <= cubes.Max(d => d.X); x++)
    {
        for (var y = cubes.Min(d => d.Y); y <= cubes.Max(d => d.Y); y++)
        {
            for (var z = cubes.Min(d => d.Z); z <= cubes.Max(d => d.Z); z++)
            {
                var cube = cubes.FirstOrDefault(d => d.X == x && d.Y == y && d.Z == z);
                if (cube != null)
                {
                    if (HasWayOut(cube with { X = cube.X - 1 }, new HashSet<Point>()))
                    {
                        result++;
                    }
                    if (HasWayOut(cube with { X = cube.X + 1 }, new HashSet<Point>()))
                    {
                        result++;
                    }
                    if (HasWayOut(cube with { Y = cube.Y - 1 }, new HashSet<Point>()))
                    {
                        result++;
                    }
                    if (HasWayOut(cube with { Y = cube.Y + 1 }, new HashSet<Point>()))
                    {
                        result++;
                    }
                    if (HasWayOut(cube with { Z = cube.Z - 1 }, new HashSet<Point>()))
                    {
                        result++;
                    }
                    if (HasWayOut(cube with { Z = cube.Z + 1 }, new HashSet<Point>()))
                    {
                        result++;
                    }
                }
            }
        }
    }

    Console.WriteLine("Part 2: " + result);

    bool HasWayOut(Point cube, HashSet<Point> visitedPoints)
    {
        if (dict.TryGetValue(cube, out var result))
        {
            return result;
        }
        if (visitedPoints.Contains(cube))
        {
            return false;
        }

        visitedPoints.Add(cube);

        if (cubes.Contains(cube))
        {
            return false;
        }
        else if (cube.X < cubes.Min(d => d.X) || cube.X > cubes.Max(d => d.X) ||
                cube.Y < cubes.Min(d => d.Y) || cube.Y > cubes.Max(d => d.Y) ||
                cube.Z < cubes.Min(d => d.Z) || cube.Z > cubes.Max(d => d.Z))
        {
            return true;
        }
        else
        {
            result =
                HasWayOut(cube with { X = cube.X - 1 }, visitedPoints) ||
                HasWayOut(cube with { X = cube.X + 1 }, visitedPoints) ||
                HasWayOut(cube with { Y = cube.Y - 1 }, visitedPoints) ||
                HasWayOut(cube with { Y = cube.Y + 1 }, visitedPoints) ||
                HasWayOut(cube with { Z = cube.Z - 1 }, visitedPoints) ||
                HasWayOut(cube with { Z = cube.Z + 1 }, visitedPoints);
        }

        visitedPoints.Remove(cube);

        dict[cube] = result;
        return result;
    }
}

public record Point(int X, int Y, int Z);