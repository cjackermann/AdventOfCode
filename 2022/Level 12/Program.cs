string[] input = File.ReadAllLines("input.txt");

var board = new Dictionary<Point, char>(
                from y in Enumerable.Range(0, input.Length)
                from x in Enumerable.Range(0, input[0].Length)
                select new KeyValuePair<Point, char>(new Point(x, y), input[y][x]));

var start = board.FirstOrDefault(d => d.Value == 'S').Key;
var endPoint = board.FirstOrDefault(d => d.Value == 'E').Key;

var queue = new PriorityQueue<Point, int>();
var resultBoard = new Dictionary<Point, int>();

resultBoard[start] = 0;
queue.Enqueue(start, 0);

while (true)
{
    var currentPoint = queue.Dequeue();
    if (currentPoint == endPoint)
    {
        break;
    }

    var neighbours = GetNeighbours(currentPoint).ToList();
    foreach (var neighbour in neighbours)
    {
        var steps = resultBoard[currentPoint] + 1;
        if (steps < resultBoard.GetValueOrDefault(neighbour))
        {
            resultBoard[neighbour] = steps;
            queue.Enqueue(neighbour, steps);
        }
    }
}

Console.WriteLine(resultBoard[endPoint]);

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
        if (board.TryGetValue(currentPoint, out var point) && board.TryGetValue(d, out var x) && (point + 1 == x || point == x || (point == 'S' && x == 'a') || (point == 'z' && x == 'E')))
        {
            return true;
        }

        return false;
    });
}

record Point(int X, int Y);