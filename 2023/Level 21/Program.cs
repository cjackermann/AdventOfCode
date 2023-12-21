using System.Drawing;

string[] input = File.ReadAllLines("input.txt");

var board = new Dictionary<Point, char>(
                from y in Enumerable.Range(0, input.Length)
                from x in Enumerable.Range(0, input[0].Length)
                select new KeyValuePair<Point, char>(new Point(x, y), input[y][x]));

var start = board.First(x => x.Value == 'S').Key;
var dict = new Dictionary<Point, int>();
var map = new HashSet<Point>() { start };

for (int i = 1; i <= 64; i++)
{
    var tmpMap = new HashSet<Point>();
    foreach (var point in map)
    {
        var neighbours = GetNeighbours(point, board).ToList();

        neighbours.ForEach(x => tmpMap.Add(x));
        if (!dict.ContainsKey(point))
        {
            dict.Add(point, neighbours.Count);
        }
    }

    map = tmpMap;
}

Console.WriteLine("Part 1: " + map.Count);
Console.ReadKey();

static IEnumerable<Point> GetNeighbours(Point point, Dictionary<Point, char> board)
{
    var north = new Point(point.X, point.Y - 1);
    if (board.TryGetValue(north, out char northValue) && northValue != '#')
    {
        yield return north;
    }

    var east = new Point(point.X + 1, point.Y);
    if (board.TryGetValue(east, out char eastValue) && eastValue != '#')
    {
        yield return east;
    }

    var south = new Point(point.X, point.Y + 1);
    if (board.TryGetValue(south, out char southValue) && southValue != '#')
    {
        yield return south;
    }

    var west = new Point(point.X - 1, point.Y);
    if (board.TryGetValue(west, out char westValue) && westValue != '#')
    {
        yield return west;
    }
}