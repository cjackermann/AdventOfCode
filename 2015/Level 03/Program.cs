string input = File.ReadAllText("input.txt");

//PartOne(input);
PartTwo(input);

static void PartOne(string input)
{
    var santaPosition = new Point(0, 0);
    List<Point> points = new() { santaPosition };

    foreach (var charakter in input)
    {
        if (charakter == '>')
        {
            santaPosition = santaPosition with { X = santaPosition.X + 1 };
        }
        else if (charakter == '<')
        {
            santaPosition = santaPosition with { X = santaPosition.X - 1 };
        }
        else if (charakter == '^')
        {
            santaPosition = santaPosition with { Y = santaPosition.Y - 1 };
        }
        else if (charakter == 'v')
        {
            santaPosition = santaPosition with { Y = santaPosition.Y + 1 };
        }

        if (!points.Contains(santaPosition))
        {
            points.Add(santaPosition);
        }
    }

    Console.WriteLine(points.Count);
}

static void PartTwo(string input)
{
    var santaPosition = new Point(0, 0);
    var roboSantaPosition = new Point(0, 0);
    List<Point> points = new() { santaPosition };

    bool moveRoboSanta = false;
    foreach (var charakter in input)
    {
        var position = moveRoboSanta ? roboSantaPosition : santaPosition;
        if (charakter == '>')
        {
            position = position with { X = position.X + 1 };
        }
        else if (charakter == '<')
        {
            position = position with { X = position.X - 1 };
        }
        else if (charakter == '^')
        {
            position = position with { Y = position.Y - 1 };
        }
        else if (charakter == 'v')
        {
            position = position with { Y = position.Y + 1 };
        }

        if (!points.Contains(position))
        {
            points.Add(position);
        }

        if (moveRoboSanta)
        {
            roboSantaPosition = position;
            moveRoboSanta = false;
        }
        else
        {
            santaPosition = position;
            moveRoboSanta = true;
        }
    }

    Console.WriteLine(points.Count);
}

record Point(int X, int Y);