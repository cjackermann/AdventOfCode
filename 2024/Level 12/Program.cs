using System.Drawing;

string[] input = File.ReadAllLines("input.txt");

var board = new Dictionary<Point, string>(
                from y in Enumerable.Range(0, input.Length)
                from x in Enumerable.Range(0, input[0].Length)
                select new KeyValuePair<Point, string>(new Point(x, y), input[y][x].ToString()));

long result1 = 0;
long result2 = 0;
HashSet<Point> checkedPoints1 = [];
HashSet<Point> checkedPoints2 = [];
foreach (var point in board)
{
    if (checkedPoints1.Contains(point.Key))
    {
        continue;
    }

    // Part1
    var tmpResult = CalculateForPoint(point.Key, point.Value);
    result1 += tmpResult.Item1 * tmpResult.Item2;

    // Part2
    var points = GetPoints(point.Key, point.Value).ToList();
    int sides = 0;

    sides += GetYPoints(points.Select(s => s with { Y = s.Y - 1}).Except(points).GroupBy(s => s.Y));
    sides += GetYPoints(points.Select(s => s with { Y = s.Y + 1}).Except(points).GroupBy(s => s.Y));
    sides += GetXPoints(points.Select(s => s with { X = s.X - 1}).Except(points).GroupBy(s => s.X));
    sides += GetXPoints(points.Select(s => s with { X = s.X + 1}).Except(points).GroupBy(s => s.X));

    result2 += points.Count * sides;
}

Console.WriteLine("Part 1: " + result1);
Console.WriteLine("Part 2: " + result2);
Console.ReadKey();

(int, int) CalculateForPoint(Point currentPoint, string currentValue)
{
    checkedPoints1.Add(currentPoint);

    Point[] neighbours =
    [
        new Point(currentPoint.X + 1, currentPoint.Y),
        new Point(currentPoint.X - 1, currentPoint.Y),
        new Point(currentPoint.X, currentPoint.Y + 1),
        new Point(currentPoint.X, currentPoint.Y - 1),
    ];

    var bounds = neighbours.Count(x => !board.ContainsKey(x) || board[x] != currentValue);
    var valueNeighbours = neighbours.Where(x => board.TryGetValue(x, out var value) && value == currentValue).ToList();

    int count = 1;
    foreach (var neighbour in valueNeighbours)
    {
        if (checkedPoints1.Contains(neighbour))
        {
            continue;
        }

        var tmpRes = CalculateForPoint(neighbour, currentValue);
        count += tmpRes.Item1;
        bounds += tmpRes.Item2;
    }

    return (count, bounds);
}

IEnumerable<Point> GetPoints(Point currentPoint, string currentValue)
{
    checkedPoints2.Add(currentPoint);

    Point[] neighbours =
    [
        new Point(currentPoint.X + 1, currentPoint.Y),
        new Point(currentPoint.X - 1, currentPoint.Y),
        new Point(currentPoint.X, currentPoint.Y + 1),
        new Point(currentPoint.X, currentPoint.Y - 1),
    ];

    var valueNeighbours = neighbours.Where(x => board.TryGetValue(x, out var value) && value == currentValue).ToList();

    foreach (var neighbour in valueNeighbours)
    {
        if (checkedPoints2.Contains(neighbour))
        {
            continue;
        }

        foreach (var point in GetPoints(neighbour, currentValue))
        {
            yield return point;
        }
    }

    yield return currentPoint;
}

int GetYPoints(IEnumerable<IGrouping<int, Point>> groups)
{
    var result = 0;
    foreach (var group in groups)
    {
        result++;

        var orderedList = group.OrderBy(s => s.X).ToList();
        for (int i = 0; i < orderedList.Count - 1; i++)
        {
            if (orderedList[i].X + 1 != orderedList[i + 1].X)
            {
                result++;
            }
        }
    }

    return result;
}

int GetXPoints(IEnumerable<IGrouping<int, Point>> groups)
{
    var result = 0;
    foreach (var group in groups)
    {
        result++;

        var orderedList = group.OrderBy(s => s.Y).ToList();
        for (int i = 0; i < orderedList.Count - 1; i++)
        {
            if (orderedList[i].Y + 1 != orderedList[i + 1].Y)
            {
                result++;
            }
        }
    }

    return result;
}