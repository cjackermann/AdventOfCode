using Level_24;
using Microsoft.Z3;

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
Console.WriteLine("Part 2: " + Solve(hailstones));

Console.ReadKey();

// using microsoft z3 nuget package - see: https://github.com/Z3Prover/z3
static long Solve(List<Hailstone> hailstones) 
{
    var ctx = new Context();
    var solver = ctx.MkSolver();

    // Coordinates of the stone
    var x = ctx.MkIntConst("x");
    var y = ctx.MkIntConst("y");
    var z = ctx.MkIntConst("z");

    // Velocity of the stone
    var vx = ctx.MkIntConst("vx");
    var vy = ctx.MkIntConst("vy");
    var vz = ctx.MkIntConst("vz");

    // For each iteration, we will add 3 new equations to the solver.
    // We want to find 9 variables (x, y, z, vx, vy, vz, t0, t1, t2) that satisfy all the equations, so a system of 9 equations is enough.
    for (var i = 0; i < 3; i++)
    {
        var t = ctx.MkIntConst($"t{i}");
        var hailstone = hailstones[i];

        var px = ctx.MkInt(Convert.ToInt64(hailstone.Position.X));
        var py = ctx.MkInt(Convert.ToInt64(hailstone.Position.Y));
        var pz = ctx.MkInt(Convert.ToInt64(hailstone.Position.Z));

        var pvx = ctx.MkInt(Convert.ToInt64(hailstone.Velocity.X));
        var pvy = ctx.MkInt(Convert.ToInt64(hailstone.Velocity.Y));
        var pvz = ctx.MkInt(Convert.ToInt64(hailstone.Velocity.Z));

        var xLeft = ctx.MkAdd(x, ctx.MkMul(t, vx)); // x + t * vx
        var yLeft = ctx.MkAdd(y, ctx.MkMul(t, vy)); // y + t * vy
        var zLeft = ctx.MkAdd(z, ctx.MkMul(t, vz)); // z + t * vz

        var xRight = ctx.MkAdd(px, ctx.MkMul(t, pvx)); // px + t * pvx
        var yRight = ctx.MkAdd(py, ctx.MkMul(t, pvy)); // py + t * pvy
        var zRight = ctx.MkAdd(pz, ctx.MkMul(t, pvz)); // pz + t * pvz

        solver.Add(t >= 0); // time should always be positive - we don't want solutions for negative time
        solver.Add(ctx.MkEq(xLeft, xRight)); // x + t * vx = px + t * pvx
        solver.Add(ctx.MkEq(yLeft, yRight)); // y + t * vy = py + t * pvy
        solver.Add(ctx.MkEq(zLeft, zRight)); // z + t * vz = pz + t * pvz
    }

    solver.Check();
    var model = solver.Model;

    var rx = model.Eval(x);
    var ry = model.Eval(y);
    var rz = model.Eval(z);

    return long.Parse(rx.ToString()) + long.Parse(ry.ToString()) + long.Parse(rz.ToString());
}