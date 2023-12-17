using Level_17;

string[] input = File.ReadAllLines("input.txt");

var board = new Dictionary<MapPoint, long>(
                from y in Enumerable.Range(0, input.Length)
                from x in Enumerable.Range(0, input[0].Length)
                select new KeyValuePair<MapPoint, long>(new MapPoint(x, y), long.Parse(input[y][x].ToString())));

var maxX = board.Max(s => s.Key.X);
var maxY = board.Max(s => s.Key.Y);
var end = new MapPoint(maxX, maxY);

Part1();
Part2();

Console.ReadKey();

void Part1()
{
    var visited = new List<(MapPoint Point, Direction Direction, int Steps)>();
    var map = new Dictionary<(MapPoint Point, Direction Direction, int Steps), long>() { { (new MapPoint(0, 0), Direction.South, 1), 0 }, { (new MapPoint(0, 0), Direction.East, 1), 0 } };

    var queue = new PriorityQueue<(MapPoint Point, Direction Direction, int Steps), long>();
    queue.Enqueue((new MapPoint(0, 0), Direction.East, 1), 0);
    queue.Enqueue((new MapPoint(0, 0), Direction.South, 1), 0);

    long? result = null;
    while (result == null)
    {
        var current = queue.Dequeue();
        visited.Add(current);

        if (current.Point == end)
        {
            result = map[current];
        }

        var neighbours = current.Point.GetPart1Neighbours(current.Direction, current.Steps).Where(s => s.Point.X >= 0 && s.Point.X <= maxX && s.Point.Y >= 0 && s.Point.Y <= maxY).ToList();
        foreach (var neighbour in neighbours)
        {
            long heatLoss = map[current] + board[neighbour.Point];
            if (heatLoss < map.GetValueOrDefault(neighbour, long.MaxValue))
            {
                map[neighbour] = heatLoss;
                queue.Enqueue(neighbour, heatLoss);
            }
        }
    }

    Console.WriteLine("Part 2: " + result);
}

void Part2()
{
    var visited = new List<(MapPoint Point, Direction Direction, int Steps)>();
    var map = new Dictionary<(MapPoint Point, Direction Direction, int Steps), long>() { { (new MapPoint(0, 0), Direction.South, 1), 0 }, { (new MapPoint(0, 0), Direction.East, 1), 0 } };

    var queue = new PriorityQueue<(MapPoint Point, Direction Direction, int Steps), long>();
    queue.Enqueue((new MapPoint(0, 0), Direction.East, 1), 0);
    queue.Enqueue((new MapPoint(0, 0), Direction.South, 1), 0);

    long? result = null;
    while (result == null)
    {
        var current = queue.Dequeue();
        visited.Add(current);

        if (current.Point == end && current.Steps >= 4)
        {
            result = map[current];
        }

        var neighbours = current.Point.GetPart2Neighbours(current.Direction, current.Steps).Where(s => s.Point.X >= 0 && s.Point.X <= maxX && s.Point.Y >= 0 && s.Point.Y <= maxY).ToList();
        foreach (var neighbour in neighbours)
        {
            long heatLoss = map[current] + board[neighbour.Point];
            if (heatLoss < map.GetValueOrDefault(neighbour, long.MaxValue))
            {
                map[neighbour] = heatLoss;
                queue.Enqueue(neighbour, heatLoss);
            }
        }
    }

    Console.WriteLine("Part 2: " + result);
}