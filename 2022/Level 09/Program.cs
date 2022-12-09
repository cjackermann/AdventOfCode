string[] input = File.ReadAllLines("input.txt");

var start = new Point(0, 0);
HashSet<Point> list = new() { start };

var currentH = start;
List<Point> points = new();
for (int i = 0; i < 9; i++)
{
    points.Add(start);
}

foreach (string line in input)
{
    var blocks = line.Split(' ');
    int amount = int.Parse(blocks[1]);

    for (int i = 0; i < amount; i++)
    {
        var previousH = currentH;

        if (blocks[0] == "R")
        {
            currentH = currentH with { X = currentH.X + 1 };
        }
        else if (blocks[0] == "L")
        {
            currentH = currentH with { X = currentH.X - 1 };
        }
        else if (blocks[0] == "D")
        {
            currentH = currentH with { Y = currentH.Y - 1 };
        }
        else if (blocks[0] == "U")
        {
            currentH = currentH with { Y = currentH.Y + 1 };
        }

        for (int x = 0; x < points.Count; x++)
        {
            var currentPoint = points[x];
            var previousPoint = x == 0 ? currentH : points[x - 1];

            if (Math.Abs(previousPoint.X - currentPoint.X) > 1 || Math.Abs(previousPoint.Y - currentPoint.Y) > 1)
            {
                if (currentPoint.X > previousPoint.X)
                {
                    currentPoint = currentPoint with { X = currentPoint.X -1 };
                }
                else if (currentPoint.X < previousPoint.X)
                {
                    currentPoint = currentPoint with { X = currentPoint.X + 1 };
                }

                if (currentPoint.Y > previousPoint.Y)
                {
                    currentPoint = currentPoint with { Y = currentPoint.Y - 1 };
                }
                else if (currentPoint.Y < previousPoint.Y)
                {
                    currentPoint = currentPoint with { Y = currentPoint.Y + 1 };
                }

                points[x] = currentPoint;
            }
        }

        list.Add(points.Last());
    }
}

Console.WriteLine(list.Count);
Console.ReadKey();

record Point(int X, int Y);
