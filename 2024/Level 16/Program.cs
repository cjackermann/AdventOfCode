using System.Drawing;

string[] input = File.ReadAllLines("input.txt");

var board = new Dictionary<Point, string>(
                from y in Enumerable.Range(0, input.Length)
                from x in Enumerable.Range(0, input[0].Length)
                select new KeyValuePair<Point, string>(new Point(x, y), input[y][x].ToString()));

var start = board.First(x => x.Value == "S").Key;
var end = board.First(x => x.Value == "E").Key;

HashSet<(Point Point, int Direction, long Cost)> visitedPoints = [];
PriorityQueue<(Point Point, int Direction, HashSet<Point> Points), long> queue = new();
queue.Enqueue((start, 0, [start]), 0);

long? lowestCost = null;
HashSet<Point> bestPathsPoints = [];
while (queue.TryDequeue(out var currentElement, out long cost))
{
    if (currentElement.Point == end)
    {
        lowestCost = cost;
        foreach (var point in currentElement.Points)
        {
            bestPathsPoints.Add(point);
        }

        continue;
    }
    else if (cost > lowestCost)
    {
        continue;
    }
    else if (visitedPoints.Any(x => x.Point == currentElement.Point && x.Direction == currentElement.Direction && x.Cost < cost))
    {
        continue;
    }

    visitedPoints.Add((currentElement.Point, currentElement.Direction, cost));

    (Point Point, int Direction)[] neighbours =
    [
        (new Point(currentElement.Point.X + 1, currentElement.Point.Y), 0),
        (new Point(currentElement.Point.X - 1, currentElement.Point.Y), 2),
        (new Point(currentElement.Point.X, currentElement.Point.Y + 1), 1),
        (new Point(currentElement.Point.X, currentElement.Point.Y - 1), 3),
    ];

    foreach (var neighbour in neighbours)
    {
        if (board.TryGetValue(neighbour.Point, out var value) && value != "#")
        {
            var turn = neighbour.Direction != currentElement.Direction;
            var points = new HashSet<Point>(currentElement.Points)
            {
                neighbour.Point
            };

            queue.Enqueue((neighbour.Point, neighbour.Direction, points), cost + 1 + (turn ? 1000 : 0));
        }
    }
}

Console.WriteLine("Part 1: " + lowestCost);
Console.WriteLine("Part 2: " + bestPathsPoints.Count);
Console.ReadKey();