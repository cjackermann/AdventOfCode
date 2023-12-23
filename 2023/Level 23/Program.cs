using System.Drawing;

string[] input = File.ReadAllLines("input.txt");

var board = new Dictionary<Point, char>(
                from y in Enumerable.Range(0, input.Length)
                from x in Enumerable.Range(0, input[0].Length)
                select new KeyValuePair<Point, char>(new Point(x, y), input[y][x]));


var start = board.First(s => s.Key.Y == 0 && s.Value == '.').Key;
PriorityQueue<(Point Current, HashSet<Point> Previous), long> queue = new();
Dictionary<(Point Current, Point? Previous), long> resultBoard = new() { [(start, null)] = 0 };
queue.Enqueue((start, new HashSet<Point>()), 0);

while (queue.Count > 0)
{
    var (Current, Previous) = queue.Dequeue();

    var neighbours = GetNeighbours(Current).Except(Previous).ToHashSet();

    foreach (var neighbour in neighbours)
    {
        Point? last = Previous.Any() ? Previous.Last() : null;
        var steps = resultBoard[(Current, last)] + 1;
        var tuple = (neighbour, Current);

        resultBoard[tuple] = steps;

        var list = new HashSet<Point>(Previous) { Current };
        queue.Enqueue((neighbour, list), steps);
    }
}


int maxY = board.Max(s => s.Key.Y);
var end = board.First(s => s.Key.Y == maxY && s.Value == '.').Key;
var endPoint = resultBoard.First(s => s.Key.Current.X == end.X && s.Key.Current.Y == end.Y);

Console.WriteLine("Part 1: " + endPoint.Value);
Console.ReadKey();

IEnumerable<Point> GetNeighbours(Point currentPoint)
{
    var currentValue = board[currentPoint];
    if (currentValue == '<')
    {
        yield return new Point(currentPoint.X - 1, currentPoint.Y);
        yield break;
    }
    else if (currentValue == '>')
    {
        yield return new Point(currentPoint.X + 1, currentPoint.Y);
        yield break;
    }
    else if (currentValue == '^')
    {
        yield return new Point(currentPoint.X, currentPoint.Y - 1);
        yield break;
    }
    else if (currentValue == 'v')
    {
        yield return new Point(currentPoint.X, currentPoint.Y + 1);
        yield break;
    }

    var directions = new[]
    {
            new Point(0, -1), // North
            new Point(1, 0),  // East
            new Point(0, 1),  // South
            new Point(-1, 0)  // West
        };

    foreach (var direction in directions)
    {
        var neighbour = new Point(currentPoint.X + direction.X, currentPoint.Y + direction.Y);
        if (board.TryGetValue(neighbour, out char neighbourValue) && neighbourValue != '#')
        {
            yield return neighbour;
        }
    }
}