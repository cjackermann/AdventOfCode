string[] input = File.ReadAllLines("input.txt");

PartOne(input);
PartTwo(input);

static void PartOne(string[] input)
{
    var dict = new Dictionary<Point, bool>(from x in Enumerable.Range(0, 1000)
                                           from y in Enumerable.Range(0, 1000)
                                           select new KeyValuePair<Point, bool>(new Point(x, y), false));

    foreach (string line in input)
    {
        var blocks = line.Split(' ');
        int xStart = 0;
        int xEnd = 0;
        int yStart = 0;
        int yEnd = 0;

        if (blocks[0] == "toggle")
        {
            xStart = blocks[1].Split(',').Select(int.Parse).First();
            yStart = blocks[1].Split(',').Select(int.Parse).Last();

            xEnd = blocks[3].Split(',').Select(int.Parse).First();
            yEnd = blocks[3].Split(',').Select(int.Parse).Last();
        }
        else
        {
            xStart = blocks[2].Split(',').Select(int.Parse).First();
            yStart = blocks[2].Split(',').Select(int.Parse).Last();

            xEnd = blocks[4].Split(',').Select(int.Parse).First();
            yEnd = blocks[4].Split(',').Select(int.Parse).Last();
        }

        var pointsToChange = from x in Enumerable.Range(xStart, xEnd + 1 - xStart)
                             from y in Enumerable.Range(yStart, yEnd + 1 - yStart)
                             select new Point(x, y);

        foreach (var point in pointsToChange)
        {
            if (blocks[0] == "turn")
            {
                if (blocks[1] == "on")
                {
                    dict[point] = true;
                }
                else
                {
                    dict[point] = false;
                }
            }
            else
            {
                dict[point] = !dict[point];
            }
        }

    }

    Console.WriteLine(dict.Count(d => d.Value));
}

static void PartTwo(string[] input)
{
    var dict = new Dictionary<Point, int>(from x in Enumerable.Range(0, 1000)
                                          from y in Enumerable.Range(0, 1000)
                                          select new KeyValuePair<Point, int>(new Point(x, y), 0));

    foreach (string line in input)
    {
        var blocks = line.Split(' ');
        int xStart = 0;
        int xEnd = 0;
        int yStart = 0;
        int yEnd = 0;

        if (blocks[0] == "toggle")
        {
            xStart = blocks[1].Split(',').Select(int.Parse).First();
            yStart = blocks[1].Split(',').Select(int.Parse).Last();

            xEnd = blocks[3].Split(',').Select(int.Parse).First();
            yEnd = blocks[3].Split(',').Select(int.Parse).Last();
        }
        else
        {
            xStart = blocks[2].Split(',').Select(int.Parse).First();
            yStart = blocks[2].Split(',').Select(int.Parse).Last();

            xEnd = blocks[4].Split(',').Select(int.Parse).First();
            yEnd = blocks[4].Split(',').Select(int.Parse).Last();
        }

        var pointsToChange = from x in Enumerable.Range(xStart, xEnd + 1 - xStart)
                             from y in Enumerable.Range(yStart, yEnd + 1 - yStart)
                             select new Point(x, y);

        foreach (var point in pointsToChange)
        {
            if (blocks[0] == "turn")
            {
                if (blocks[1] == "on")
                {
                    dict[point] = dict[point] + 1;
                }
                else if (dict[point] > 0)
                {
                    dict[point] = dict[point] - 1;
                }
            }
            else
            {
                dict[point] = dict[point] + 2;
            }
        }
    }

    Console.WriteLine(dict.Sum(d => d.Value));
}

record Point(int X, int Y);