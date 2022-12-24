string[] input = File.ReadAllLines("input.txt");

(Dictionary<Point, bool> map, List<Blizzard> blizzards) = GetStartData(input);
Point start = map.First(d => d.Value).Key;
Point end = map.Last(d => d.Value).Key;
int yMax = map.Max(d => d.Key.Y);
int xMax = map.Max(d => d.Key.X);

var maxBoards = (xMax - 1) * (yMax - 1);
var occupiedSpotsByRound = new List<HashSet<Point>>();
var currentBlizzards = blizzards;

for (int i = 0; i < maxBoards; i++)
{
    var newBlizzards = new List<Blizzard>();
    var occupiedSpots = new HashSet<Point>();

    UpdateBlizzards(yMax, xMax, currentBlizzards, newBlizzards, occupiedSpots);

    occupiedSpotsByRound.Add(occupiedSpots);
    currentBlizzards = newBlizzards;
}

var visited = new HashSet<(Point location, int step, int stage)>();
var queue = new Queue<(Point location, int step, int stage)>();
queue.Enqueue((start, 0, 0));
var stageOneDone = false;

while (queue.TryDequeue(out var current))
{
    var (location, step, stage) = current;

    if (stage == 1 && location.Y == 0)
    {
        stage = 2;
    }

    if (location.Y == yMax)
    {
        if (stage == 0)
        {
            if (!stageOneDone)
            {
                Console.WriteLine("Stage 1: " + (step - 1));
                stageOneDone = true;
            }

            stage = 1;
        }
        else if (stage == 2)
        {
            Console.WriteLine("Stage 2: " + (step - 1));
            break;
        }
    }

    if (visited.Contains(current))
    {
        continue;
    }

    visited.Add(current);

    var obstacles = occupiedSpotsByRound[step % occupiedSpotsByRound.Count];
    var possibleNextPoints = new[] { location, location with { Y = location.Y + 1 }, location with { Y = location.Y - 1 }, location with { X = location.X + 1 }, location with { X = location.X - 1 } };

    foreach (var possiblePoint in possibleNextPoints)
    {
        if (!map.ContainsKey(possiblePoint) || map[possiblePoint] == false || obstacles.Contains(possiblePoint))
        {
            continue;
        }

        queue.Enqueue((possiblePoint, step + 1, stage));
    }
}

static void UpdateBlizzards(int yMax, int xMax, List<Blizzard> currentBlizzards, List<Blizzard> newBlizzards, HashSet<Point> occupiedSpots)
{
    foreach (var blizzard in currentBlizzards)
    {
        occupiedSpots.Add(blizzard.Location);

        if (blizzard.Direction == Direction.Right)
        {
            var newLocation = blizzard.Location with { X = blizzard.Location.X + 1 };
            if (newLocation.X == xMax)
            {
                newLocation = blizzard.Location with { X = 1 };
            }

            newBlizzards.Add(new Blizzard(newLocation, blizzard.Direction));
        }
        else if (blizzard.Direction == Direction.Down)
        {
            var newLocation = blizzard.Location with { Y = blizzard.Location.Y + 1 };
            if (newLocation.Y == yMax)
            {
                newLocation = blizzard.Location with { Y = 1 };
            }

            newBlizzards.Add(new Blizzard(newLocation, blizzard.Direction));
        }
        else if (blizzard.Direction == Direction.Left)
        {
            var newLocation = blizzard.Location with { X = blizzard.Location.X - 1 };
            if (newLocation.X == 0)
            {
                newLocation = blizzard.Location with { X = xMax - 1 };
            }

            newBlizzards.Add(new Blizzard(newLocation, blizzard.Direction));
        }
        else if (blizzard.Direction == Direction.Up)
        {
            var newLocation = blizzard.Location with { Y = blizzard.Location.Y - 1 };
            if (newLocation.Y == 0)
            {
                newLocation = blizzard.Location with { Y = yMax - 1 };
            }

            newBlizzards.Add(new Blizzard(newLocation, blizzard.Direction));
        }
    }
}

static (Dictionary<Point, bool> Map, List<Blizzard> Blizzards) GetStartData(string[] input)
{
    Dictionary<Point, bool> map = new();
    List<Blizzard> blizzards = new();

    for (int y = 0; y < input.Length; y++)
    {
        for (int x = 0; x < input[0].Length; x++)
        {
            if (input[y][x] == '#')
            {
                map.Add(new Point(x, y), false);
            }
            else if (input[y][x] == '.')
            {
                map.Add(new Point(x, y), true);
            }
            else
            {
                var point = new Point(x, y);
                map.Add(point, true);

                if (input[y][x] == '>')
                {
                    blizzards.Add(new Blizzard(point, Direction.Right));
                }
                else if (input[y][x] == 'v')
                {
                    blizzards.Add(new Blizzard(point, Direction.Down));
                }
                else if (input[y][x] == '<')
                {
                    blizzards.Add(new Blizzard(point, Direction.Left));
                }
                else if (input[y][x] == '^')
                {
                    blizzards.Add(new Blizzard(point, Direction.Up));
                }
            }
        }
    }

    return (map, blizzards);
}

public record Point(int X, int Y);

public record Blizzard(Point Location, Direction Direction);

public enum Direction
{
    Right = 0,
    Down = 1,
    Left = 2,
    Up = 3,
}