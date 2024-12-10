string[] input = File.ReadAllLines("input.txt");

var board = new Dictionary<Point, int>(
                from y in Enumerable.Range(0, input.Length)
                from x in Enumerable.Range(0, input[0].Length)
                select new KeyValuePair<Point, int>(new Point(x, y), int.Parse(input[y][x].ToString())));

var startPoints = board.Where(x => x.Value == 0).ToList();
long result1 = 0;
long result2 = 0;
foreach (var start in startPoints)
{
    PriorityQueue<Point, int> queue = new();
    queue.Enqueue(start.Key, 0);

    HashSet<Point> results = [];
    while (true)
    {
        Point currentPoint = null;
        try
        {
            currentPoint = queue.Dequeue();
        }
        catch
        {
            break;
        }

        int currentValue = board[currentPoint];
        var neighbours = GetNeighbours(currentPoint).ToList();
        foreach (var neighbour in neighbours)
        {
            if (board.TryGetValue(neighbour, out int value) && value == currentValue + 1)
            {
                if (value == 9)
                {
                    results.Add(neighbour);
                    result2++;
                }
                else
                {
                    queue.Enqueue(neighbour, value);
                }
            }
        }
    }

    result1 += results.Count;
}

Console.WriteLine("Part 1: " + result1);
Console.WriteLine("Part 2: " + result2);
Console.ReadKey();

IEnumerable<Point> GetNeighbours(Point currentPoint)
{
    yield return new Point(currentPoint.X + 1, currentPoint.Y);
    yield return new Point(currentPoint.X - 1, currentPoint.Y);
    yield return new Point(currentPoint.X, currentPoint.Y + 1);
    yield return new Point(currentPoint.X, currentPoint.Y - 1);
}

record Point(int X, int Y);