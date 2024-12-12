using System.Drawing;

string[] input = File.ReadAllLines("input.txt");

var board = new Dictionary<Point, string>(
                from y in Enumerable.Range(0, input.Length)
                from x in Enumerable.Range(0, input[0].Length)
                select new KeyValuePair<Point, string>(new Point(x, y), input[y][x].ToString()));

long result = 0;
HashSet<Point> checkedPoints = [];
foreach (var point in board)
{
    if (checkedPoints.Contains(point.Key))
    {
        continue;
    }

    var tmpResult = CalculateForPoint(point.Key, point.Value);

    result += tmpResult.Item1 * tmpResult.Item2;
}

Console.WriteLine("Part 1: " + result);
Console.ReadKey();

(int, int) CalculateForPoint(Point currentPoint, string currentValue)
{
    checkedPoints.Add(currentPoint);

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
        if (checkedPoints.Contains(neighbour))
        {
            continue;
        }

        var tmpRes = CalculateForPoint(neighbour, currentValue);
        count += tmpRes.Item1;
        bounds += tmpRes.Item2;
    }

    return (count, bounds);
}