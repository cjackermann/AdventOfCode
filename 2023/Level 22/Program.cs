using Level_22;

string[] input = File.ReadAllLines("input.txt");

var bricks = new List<(VectorXYZ Start, VectorXYZ End)>();
foreach (var line in input)
{
    var parts = line.Split('~');

    string[] startCoordinates = parts[0].Split(',');
    VectorXYZ start = new VectorXYZ(long.Parse(startCoordinates[0]), long.Parse(startCoordinates[1]), long.Parse(startCoordinates[2]));

    string[] endCoordinates = parts[1].Split(',');
    VectorXYZ end = new VectorXYZ(long.Parse(endCoordinates[0]), long.Parse(endCoordinates[1]), long.Parse(endCoordinates[2]));

    bricks.Add((start, end));
}

var resters = new Dictionary<long, HashSet<long>>();
var supporters = new Dictionary<long, HashSet<long>>();
var heightMap = new Dictionary<VectorXY, (long height, long blockNumber)>();

var orderedBricks = bricks.OrderBy(b => b.Start.Z).ToArray();
for (long i = 0; i < orderedBricks.Length; i++)
{
    var (Start, End) = orderedBricks[i];

    long brickHeight = End.Z - Start.Z + 1;
    long landing = 0;

    var newRester = new HashSet<long>();
    for (long x = Start.X; x <= End.X; x++)
    {
        for (long y = Start.Y; y <= End.Y; y++)
        {
            VectorXY coord = new VectorXY(x, y);
            if (heightMap.TryGetValue(coord, out var heightData))
            {
                if (heightData.height > landing)
                {
                    landing = heightData.height;
                    newRester.Clear();
                    newRester.Add(heightData.blockNumber);
                }
                else if (heightData.height == landing)
                {
                    newRester.Add(heightData.blockNumber);
                }
            }
        }
    }

    resters.Add(i, newRester);

    for (long x = Start.X; x <= End.X; x++)
    {
        for (long y = Start.Y; y <= End.Y; y++)
        {
            VectorXY coord = new VectorXY(x, y);
            heightMap[coord] = (landing + brickHeight, i);
        }
    }

    supporters[i] = new();
    foreach (long supportBrick in newRester)
    {
        supporters[supportBrick].Add(i);
    }
}

foreach (var supports in resters.Values)
{
    if (supports.Count == 0)
    {
        supports.Add(-1);
    }
}

Console.WriteLine("Part 1: " + Part1());
Console.WriteLine("Part 2: " + Part2());

Console.ReadKey();

long Part1()
{
    long result = 0;
    for (long i = 0; i < supporters.Count; i++)
    {
        if (supporters[i].Count == 0 || supporters[i].All(x => resters[x].Count > 1))
        {
            result++;
        }
    }

    return result;
}

long Part2()
{
    long result = 0;

    for (long i = 0; i < supporters.Count; i++)
    {
        var fallen = new HashSet<long> { i };

        for (long j = i + 1; j < supporters.Count; j++)
        {
            if (!resters[j].Except(fallen).Any())
            {
                fallen.Add(j);
                result++;
            }
        }
    }
    return result;
}