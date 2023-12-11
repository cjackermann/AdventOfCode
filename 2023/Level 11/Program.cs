List<string> input = File.ReadAllLines("input.txt").ToList();

List<long> yOffsets = new List<long>();
for (int y = 0; y < input.Count; y++)
{
    if (!input[y].Any(x => x == '#'))
    {
        yOffsets.Add(y);
    }
}

List<long> xOffsets = new List<long>();
for (int i = 0; i < input[0].Length; i++)
{
    if (!input.Select(x => x[i]).Any(x => x == '#'))
    {
        xOffsets.Add(i);
    }
}

var board = new Dictionary<Point, char>(
                from y in Enumerable.Range(0, input.Count)
                from x in Enumerable.Range(0, input[0].Length)
                select new KeyValuePair<Point, char>(new Point(x, y), input[y][x]));

var galaxies = board.Where(x => x.Value == '#').Select(x => x.Key).ToList();

long counter1 = 0;
long counter2 = 0;
for (int g1 = 0; g1 < galaxies.Count - 1; g1++)
{
    for (int g2 = g1 + 1; g2 < galaxies.Count; g2++)
    {
        var distanceX1 = Math.Abs(galaxies[g1].X - galaxies[g2].X);
        var distanceY1 = Math.Abs(galaxies[g1].Y - galaxies[g2].Y);
        distanceX1 += xOffsets.Count(x => x >= Math.Min(galaxies[g1].X, galaxies[g2].X) && x <= Math.Max(galaxies[g1].X, galaxies[g2].X));
        distanceY1 += yOffsets.Count(x => x >= Math.Min(galaxies[g1].Y, galaxies[g2].Y) && x <= Math.Max(galaxies[g1].Y, galaxies[g2].Y));
        counter1 += distanceX1 + distanceY1;

        var distanceX2 = Math.Abs(galaxies[g1].X - galaxies[g2].X);
        var distanceY2 = Math.Abs(galaxies[g1].Y - galaxies[g2].Y);
        distanceX2 += xOffsets.Count(x => x >= Math.Min(galaxies[g1].X, galaxies[g2].X) && x <= Math.Max(galaxies[g1].X, galaxies[g2].X)) * 999999;
        distanceY2 += yOffsets.Count(x => x >= Math.Min(galaxies[g1].Y, galaxies[g2].Y) && x <= Math.Max(galaxies[g1].Y, galaxies[g2].Y)) * 999999;
        counter2 += distanceX2 + distanceY2;
    }
}

Console.WriteLine("Part 1: " + counter1);
Console.WriteLine("Part 2: " + counter2);
Console.ReadKey();

record Point(long X, long Y);