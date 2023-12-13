using System.Drawing;

var input = File.ReadAllText("input.txt");
var patterns = input.Split("\r\n\r\n").Select(x => x.Split("\r\n").ToList()).ToList();

long totalSum = 0;
foreach (var pattern in patterns)
{
    var board = new Dictionary<Point, char>(
                from y in Enumerable.Range(0, pattern.Count)
                from x in Enumerable.Range(0, pattern[0].Length)
                select new KeyValuePair<Point, char>(new Point(x, y), pattern[y][x]));

    var resVertical = CheckVertical(board);
    var resHorizontal = CheckHorizontal(board);

    totalSum += (resVertical ?? 0) + (resHorizontal ?? 0) * 100;
}

Console.WriteLine("Part 1: " + totalSum);
Console.ReadKey();

static long? CheckVertical(Dictionary<Point, char> board)
{
    var grp = board.GroupBy(x => x.Key.X).ToList();
    var possibleResults = new List<int>();

    for (int i = 0; i < grp.Count() - 1; i++)
    {
        var t1 = grp[i].OrderBy(x => x.Key.Y).Select(x => x.Value).ToList();
        var t2 = grp[i + 1].OrderBy(x => x.Key.Y).Select(x => x.Value).ToList();

        if (t1.SequenceEqual(t2))
        {
            possibleResults.Add(i);
        }
    }

    foreach (var res in possibleResults)
    {
        var leftValues = grp.Where(x => x.Key < res).Reverse().ToList();
        var rightValues = grp.Where(x => x.Key > res + 1).ToList();

        bool correct = true;
        for (int i = 0; i < Math.Min(leftValues.Count, rightValues.Count); i++)
        {
            var t1 = leftValues[i].OrderBy(x => x.Key.Y).Select(x => x.Value).ToList();
            var t2 = rightValues[i].OrderBy(x => x.Key.Y).Select(x => x.Value).ToList();

            if (!t1.SequenceEqual(t2))
            {
                correct = false;
            }
        }

        if (correct)
        {
            return res + 1;
        }
    }

    return null;
}

static long? CheckHorizontal(Dictionary<Point, char> board)
{
    var grp = board.GroupBy(x => x.Key.Y).ToList();
    var possibleResults = new List<int>();

    for (int i = 0; i < grp.Count() - 1; i++)
    {
        var t1 = grp[i].OrderBy(x => x.Key.X).Select(x => x.Value).ToList();
        var t2 = grp[i + 1].OrderBy(x => x.Key.X).Select(x => x.Value).ToList();

        if (t1.SequenceEqual(t2))
        {
            possibleResults.Add(i);
        }
    }

    foreach (var res in possibleResults)
    {
        var leftValues = grp.Where(x => x.Key < res).Reverse().ToList();
        var rightValues = grp.Where(x => x.Key > res + 1).ToList();

        bool correct = true;
        for (int i = 0; i < Math.Min(leftValues.Count, rightValues.Count); i++)
        {
            var t1 = leftValues[i].OrderBy(x => x.Key.X).Select(x => x.Value).ToList();
            var t2 = rightValues[i].OrderBy(x => x.Key.X).Select(x => x.Value).ToList();

            if (!t1.SequenceEqual(t2))
            {
                correct = false;
            }
        }

        if (correct)
        {
            return res + 1;
        }
    }

    return null;
}