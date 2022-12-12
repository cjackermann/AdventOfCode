string[] input = File.ReadAllLines("input.txt");

var board = new Dictionary<Point, char>(
                from y in Enumerable.Range(0, input.Length)
                from x in Enumerable.Range(0, input[0].Length)
                select new KeyValuePair<Point, char>(new Point(x, y), input[y][x]));

List<Point> startPoints = new();
startPoints = board.Where(d => d.Value == 'S' || d.Value == 'a').Select(d => d.Key).ToList();
//startPoints = board.Where(d => d.Value == 'S').Select(d => d.Key).ToList();
var endPoint = board.FirstOrDefault(d => d.Value == 'E').Key;

var results = new List<int>();
foreach (var start in startPoints)
{
    var queue = new PriorityQueue<Point, int>();
    var resultBoard = new Dictionary<Point, int>();

    resultBoard[start] = 0;
    queue.Enqueue((start), 0);

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

        if (currentPoint == endPoint)
        {
            results.Add(resultBoard[endPoint]);
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
}


Console.WriteLine(results.Min());
Console.ReadKey();

IEnumerable<Point> GetNeighbours(Point currentPoint)
{
    var allowedNeighbours = new[] {
                currentPoint with {Y = currentPoint.Y + 1},
                currentPoint with {Y = currentPoint.Y - 1},
                currentPoint with {X = currentPoint.X + 1},
                currentPoint with {X = currentPoint.X - 1},
            };

    return allowedNeighbours.Where(d =>
    {
        if (board.TryGetValue(currentPoint, out var point) && board.TryGetValue(d, out var x) && (point + 1 == x || point == x || (point - 2 == x) || (point == 'S' && x == 'a') || (point == 'z' && x == 'E')))
        {
            return true;
        }

        return false;
    });
}

record Point(int X, int Y);