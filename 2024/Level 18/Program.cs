using System.Drawing;

string[] input = File.ReadAllLines("input.txt");

HashSet<Point> unsavePoints = [];
foreach (var line in input)
{
    var parts = line.Split(",");
    unsavePoints.Add(new Point(int.Parse(parts[0]), int.Parse(parts[1])));
}

int gridSize = 70;
var start = new Point(0, 0);
var end = new Point(gridSize, gridSize);

Part1(unsavePoints, gridSize, start, end);
Part2(unsavePoints, gridSize, start, end);

Console.ReadKey();

static void Part1(HashSet<Point> unsavePoints, int gridSize, Point start, Point end)
{
    var queue = new PriorityQueue<(Point Point, List<Point>), int>();
    queue.Enqueue((start, []), 0);

    HashSet<Point> visitedPoints = [];
    while (queue.TryDequeue(out var currentPoint, out var currentPrio))
    {
        if (currentPoint.Point == end)
        {
            Console.WriteLine("Part 1: " + currentPrio);
            break;
        }

        (Point Point, int Direction)[] neighbours =
        [
            (new Point(currentPoint.Point.X + 1, currentPoint.Point.Y), 0),
        (new Point(currentPoint.Point.X - 1, currentPoint.Point.Y), 2),
        (new Point(currentPoint.Point.X, currentPoint.Point.Y + 1), 1),
        (new Point(currentPoint.Point.X, currentPoint.Point.Y - 1), 3),
    ];

        foreach (var neighbour in neighbours.Where(x => !unsavePoints.Take(1024).Contains(x.Point) && x.Point.X >= 0 && x.Point.Y >= 0 && x.Point.X <= gridSize && x.Point.Y <= gridSize))
        {
            if (!visitedPoints.Contains(neighbour.Point))
            {
                var tmpList = currentPoint.Item2.ToList();
                tmpList.Add(neighbour.Point);

                queue.Enqueue((neighbour.Point, tmpList), currentPrio + 1);
                visitedPoints.Add(neighbour.Point);
            }
        }
    }
}

static void Part2(HashSet<Point> unsavePoints, int gridSize, Point start, Point end)
{
    for (int i = 1; i <= unsavePoints.Count; i++)
    {
        var queue = new PriorityQueue<(Point Point, List<Point>), int>();
        queue.Enqueue((start, []), 0);

        HashSet<Point> visitedPoints = [];
        bool hasPath = false;
        while (queue.TryDequeue(out var currentPoint, out var currentPrio))
        {
            if (currentPoint.Point == end)
            {
                hasPath = true;
                break;
            }

            (Point Point, int Direction)[] neighbours =
            [
                (new Point(currentPoint.Point.X + 1, currentPoint.Point.Y), 0),
            (new Point(currentPoint.Point.X - 1, currentPoint.Point.Y), 2),
            (new Point(currentPoint.Point.X, currentPoint.Point.Y + 1), 1),
            (new Point(currentPoint.Point.X, currentPoint.Point.Y - 1), 3),
        ];

            foreach (var neighbour in neighbours.Where(x => !unsavePoints.Take(i).Contains(x.Point) && x.Point.X >= 0 && x.Point.Y >= 0 && x.Point.X <= gridSize && x.Point.Y <= gridSize))
            {
                if (!visitedPoints.Contains(neighbour.Point))
                {
                    var tmpList = currentPoint.Item2.ToList();
                    tmpList.Add(neighbour.Point);

                    queue.Enqueue((neighbour.Point, tmpList), currentPrio + 1);
                    visitedPoints.Add(neighbour.Point);
                }
            }
        }

        if (!hasPath)
        {
            var item = unsavePoints.ElementAt(i - 1);
            Console.WriteLine("Part 2: " + item.X + "," + item.Y);
            break;
        }
    }
}