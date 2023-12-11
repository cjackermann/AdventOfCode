List<string> input = File.ReadAllLines("input.txt").ToList();

var tmpInput = input.ToList();
for (int i = 0; i < tmpInput.Count; i++)
{
    if (!tmpInput[i].Any(x => x == '#'))
    {
        input.Insert(i + 1, tmpInput[i]);
    }
}

tmpInput = input.ToList();
int addOffset = 0;
for (int i = 0; i < tmpInput[0].Length; i++)
{
    if (!tmpInput.Select(x => x[i]).Any(x => x == '#'))
    {
        for (int j = 0; j < input.Count; j++)
        {
            var t = input[j].ToList();
            t.Insert(i + addOffset, '.');
            input[j] = string.Join("", t);
        }

        addOffset++;
    }
}

var board = new Dictionary<Point, char>(
                from y in Enumerable.Range(0, input.Count)
                from x in Enumerable.Range(0, input[0].Length)
                select new KeyValuePair<Point, char>(new Point(x, y), input[y][x]));

var galaxies = board.Where(x => x.Value == '#').Select(x => x.Key).ToList();

int counter = 0;
for (int g1 = 0; g1 < galaxies.Count - 1;  g1++)
{
    for (int g2 = g1 + 1; g2 < galaxies.Count; g2++)
    {
        var distanceX = Math.Abs(galaxies[g1].X - galaxies[g2].X);
        var distanceY = Math.Abs(galaxies[g1].Y - galaxies[g2].Y);

        counter += distanceX + distanceY;
    }
}

Console.WriteLine("Part 1: " + counter);
Console.ReadKey();

record Point(int X, int Y);