string[] input = File.ReadAllLines("input.txt");

var start = new Point(0, 0);
HashSet<Point> list = new() { start };

var currentH = start;
List<Point> points = new List<Point>();
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
            var tailX = points[x].X;
            var tailY = points[x].Y;

            var previousX = x == 0 ? previousH.X : points[x - 1].X;
            var previousY = x == 0 ? previousH.Y : points[x - 1].Y;
            if (Math.Abs(previousX - tailX) > 1 || Math.Abs(previousY - tailY) > 1)
            {
                var newTailX = tailX;
                if (tailX > previousX)
                {
                    tailX--;
                }
                else if (tailX < previousX)
                {
                    tailX++;
                }

                var newTailY = tailY;
                if (tailY > previousY)
                {
                    tailY--;
                }
                else if (tailY < previousY)
                {
                    tailY++;
                }

                points[x] = new Point(tailX, tailY);
            }
        }

        list.Add(points.Last());
    }
}

Console.WriteLine(list.Count + 1);

record Point(int X, int Y);
