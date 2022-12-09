string[] input = File.ReadAllLines("input.txt");

HashSet<Point> visitedTailPoints = new() { new Point(0, 0) };

var head = new Point(0, 0);
//List<Point> tails = new(1);
List<Point> tails = new(9);

for (int i = 0; i < tails.Capacity; i++)
{
    tails.Add(head);
}

foreach (string line in input)
{
    var blocks = line.Split(' ');
    int amount = int.Parse(blocks[1]);

    for (int i = 0; i < amount; i++)
    {
        if (blocks[0] == "R")
        {
            head = head with { X = head.X + 1 };
        }
        else if (blocks[0] == "L")
        {
            head = head with { X = head.X - 1 };
        }
        else if (blocks[0] == "D")
        {
            head = head with { Y = head.Y - 1 };
        }
        else if (blocks[0] == "U")
        {
            head = head with { Y = head.Y + 1 };
        }

        for (int x = 0; x < tails.Count; x++)
        {
            var currentTail = tails[x];
            var previousTail = x == 0 ? head : tails[x - 1];

            if (Math.Abs(previousTail.X - currentTail.X) > 1 || Math.Abs(previousTail.Y - currentTail.Y) > 1)
            {
                if (currentTail.X > previousTail.X)
                {
                    currentTail = currentTail with { X = currentTail.X - 1 };
                }
                else if (currentTail.X < previousTail.X)
                {
                    currentTail = currentTail with { X = currentTail.X + 1 };
                }

                if (currentTail.Y > previousTail.Y)
                {
                    currentTail = currentTail with { Y = currentTail.Y - 1 };
                }
                else if (currentTail.Y < previousTail.Y)
                {
                    currentTail = currentTail with { Y = currentTail.Y + 1 };
                }

                tails[x] = currentTail;
            }
        }

        visitedTailPoints.Add(tails.Last());
    }
}

Console.WriteLine(visitedTailPoints.Count);

record Point(int X, int Y);
