string[] input = File.ReadAllLines("input.txt");

var board = new Dictionary<Point, char>(
                from y in Enumerable.Range(0, input.Length)
                from x in Enumerable.Range(0, input[0].Length)
                select new KeyValuePair<Point, char>(new Point(x, y), input[y][x]));

var start = board.First(x => x.Value == 'S').Key;
PriorityQueue<Point, int> queue = new();
Dictionary<Point, int> resultBoard = new() { [start] = 0 };
queue.Enqueue(start, 0);

Point previousPoint = null;
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

    var neighbours = GetNeighbours(currentPoint).Where(x => x != previousPoint).ToList();
    foreach (var neighbour in neighbours)
    {
        var steps = resultBoard[currentPoint] + 1;
        if (steps < resultBoard.GetValueOrDefault(neighbour, int.MaxValue))
        {
            resultBoard[neighbour] = steps;
            queue.Enqueue(neighbour, steps);
        }
    }
}

Console.WriteLine("Part 1: " + resultBoard.Max(x => x.Value));
Console.ReadKey();

IEnumerable<Point> GetNeighbours(Point currentPoint)
{
    if (board[currentPoint] == '|')
    {
        yield return currentPoint with { Y = currentPoint.Y + 1 };
        yield return currentPoint with { Y = currentPoint.Y - 1 };
    }
    else if (board[currentPoint] == '-')
    {
        yield return currentPoint with { X = currentPoint.X + 1 };
        yield return currentPoint with { X = currentPoint.X - 1 };
    }
    else if (board[currentPoint] == 'L' || board[currentPoint] == 'S')
    {
        yield return currentPoint with { Y = currentPoint.Y - 1 };
        yield return currentPoint with { X = currentPoint.X + 1 };
    }
    else if (board[currentPoint] == 'J')
    {
        yield return currentPoint with { Y = currentPoint.Y - 1 };
        yield return currentPoint with { X = currentPoint.X - 1 };
    }
    else if (board[currentPoint] == '7')
    {
        yield return currentPoint with { Y = currentPoint.Y + 1 };
        yield return currentPoint with { X = currentPoint.X - 1 };
    }
    else if (board[currentPoint] == 'F')
    {
        yield return currentPoint with { Y = currentPoint.Y + 1 };
        yield return currentPoint with { X = currentPoint.X + 1 };
    }
}

record Point(int X, int Y);