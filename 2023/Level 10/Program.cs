string[] input = File.ReadAllLines("input.txt");

var board = new Dictionary<Point, char>(
                from y in Enumerable.Range(0, input.Length)
                from x in Enumerable.Range(0, input[0].Length)
                select new KeyValuePair<Point, char>(new Point(x, y), input[y][x]));

int maxX = board.Max(x => x.Key.X);
int maxY = board.Max(x => x.Key.Y);

var start = board.First(x => x.Value == 'S').Key;
PriorityQueue<Point, int> queue = new();
Dictionary<Point, int> resultBoard = new() { [start] = 0 };
queue.Enqueue(start, 0);

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

    var neighbours = GetNeighbours(currentPoint).ToList();
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

foreach (var point in board.Keys.Where(x => !resultBoard.ContainsKey(x)))
{
    board[point] = '.';
}

long counter = 0;
for (int y = 0; y < maxY; y++)
{
    bool inside = false;
    char value = board[new Point(0, y)];

    for (int x = 0; x < maxX; x++)
    {
        char currentValue = board[new Point(x, y)];

        switch (currentValue)
        {
            case '|': inside = !inside; break;
            case 'F': value = 'F'; break;
            case 'L': value = 'L'; break;
            case '7': if (value == 'L') inside = !inside; break;
            case 'J': if (value == 'F') inside = !inside; break;
            case '.': if (inside) counter++; break;
        }
    }
}

Console.WriteLine("Part 2: " + counter);
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