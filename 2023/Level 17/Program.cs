using Level_17;

string[] input = File.ReadAllLines("input.txt");

var board = new Dictionary<MapPoint, long>(
                from y in Enumerable.Range(0, input.Length)
                from x in Enumerable.Range(0, input[0].Length)
                select new KeyValuePair<MapPoint, long>(new MapPoint(x, y), long.Parse(input[y][x].ToString())));

var maxX = board.Max(s => s.Key.X);
var maxY = board.Max(s => s.Key.Y);
var end = new MapPoint(maxX, maxY);

var map = new Dictionary<MapPoint, long>() { { new MapPoint(0, 0), 0 } };

var queue = new PriorityQueue<(MapPoint Point, Direction Direction, int Steps), long>();
queue.Enqueue((new MapPoint(0, 0), Direction.East, 0), 0);

while (queue.Count != 0)
{
    var current = queue.Dequeue();

    var neighbours = current.Point.GetNeighbours(current.Direction, current.Steps).Where(s => s.Point.X >= 0 && s.Point.X <= maxX && s.Point.Y >= 0 && s.Point.Y <= maxY).ToList();
    foreach (var neighbour in neighbours)
    {
        long heatLoss = map[current.Point] + board[neighbour.Point];
        if (heatLoss < map.GetValueOrDefault(neighbour.Point, long.MaxValue))
        {
            map[neighbour.Point] = heatLoss;
            queue.Enqueue(neighbour, heatLoss);
        }
    }
}

Console.WriteLine("Part 1: " + map[end]);
Console.ReadKey();