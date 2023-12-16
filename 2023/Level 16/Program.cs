using System.Drawing;
using Level_16;

string[] input = File.ReadAllLines("input.txt");

var board = new Dictionary<Point, char>(
                from y in Enumerable.Range(0, input.Length)
                from x in Enumerable.Range(0, input[0].Length)
                select new KeyValuePair<Point, char>(new Point(x, y), input[y][x]));

var maxX = board.Max(s => s.Key.X);
var maxY = board.Max(s => s.Key.Y);

long counter1 = Calculate(new Beam(0, 0, Direction.Right));
Console.WriteLine("Part 1: " + counter1);

long counter2 = 0;

for (int x = 0; x < input[0].Length; x++)
{
    var start = new Beam(x, 0, Direction.Down);
    var result = Calculate(start);

    if (result > counter2)
    {
        counter2 = result;
    }
}

for (int y = 0; y < input.Length; y++)
{
    var start = new Beam(0, y, Direction.Right);
    var result = Calculate(start);

    if (result > counter2)
    {
        counter2 = result;
    }
}

for (int x = 0; x < input[0].Length; x++)
{
    var start = new Beam(x, input.Length - 1, Direction.Up);
    var result = Calculate(start);

    if (result > counter2)
    {
        counter2 = result;
    }
}

for (int y = 0; y < input.Length; y++)
{
    var start = new Beam(input[0].Length - 1, y, Direction.Left);
    var result = Calculate(start);

    if (result > counter2)
    {
        counter2 = result;
    }
}

Console.WriteLine("Part 2: " + counter2);
Console.ReadKey();

long Calculate(Beam start)
{
    var currentBeams = new List<Beam>() { start };
    var visited = new HashSet<Point>();
    var cache = new HashSet<(Point, Direction)>();

    while (currentBeams.Any())
    {
        var tmpNewBeams = new List<Beam>();
        currentBeams.ForEach(x => visited.Add(x.CurrentPosition));

        foreach (var beam in currentBeams)
        {
            var boardValue = board[beam.CurrentPosition];
            switch (boardValue)
            {
                case '.':
                case '-' when beam.CurrentDirection == Direction.Left || beam.CurrentDirection == Direction.Right:
                case '|' when beam.CurrentDirection == Direction.Up || beam.CurrentDirection == Direction.Down:
                    beam.ChangePosition(beam.CurrentDirection);
                    break;

                case '\\':
                    if (beam.CurrentDirection == Direction.Right)
                    {
                        beam.ChangePosition(Direction.Down);
                    }
                    else if (beam.CurrentDirection == Direction.Up)
                    {
                        beam.ChangePosition(Direction.Left);
                    }
                    else if (beam.CurrentDirection == Direction.Left)
                    {
                        beam.ChangePosition(Direction.Up);
                    }
                    else if (beam.CurrentDirection == Direction.Down)
                    {
                        beam.ChangePosition(Direction.Right);
                    }

                    break;

                case '/':
                    if (beam.CurrentDirection == Direction.Right)
                    {
                        beam.ChangePosition(Direction.Up);
                    }
                    else if (beam.CurrentDirection == Direction.Up)
                    {
                        beam.ChangePosition(Direction.Right);
                    }
                    else if (beam.CurrentDirection == Direction.Left)
                    {
                        beam.ChangePosition(Direction.Down);
                    }
                    else if (beam.CurrentDirection == Direction.Down)
                    {
                        beam.ChangePosition(Direction.Left);
                    }

                    break;

                case '-':
                    {
                        beam.ChangePosition(Direction.Right);

                        var newBeam = new Beam(beam.CurrentPosition.X, beam.CurrentPosition.Y, Direction.Left);
                        newBeam.ChangePosition(Direction.Left);
                        newBeam.ChangePosition(Direction.Left);

                        if (newBeam.CheckPosition(maxX, maxY))
                        {
                            tmpNewBeams.Add(newBeam);
                        }

                        break;
                    }


                case '|':
                    {
                        beam.ChangePosition(Direction.Up);

                        var newBeam = new Beam(beam.CurrentPosition.X, beam.CurrentPosition.Y, Direction.Down);
                        newBeam.ChangePosition(Direction.Down);
                        newBeam.ChangePosition(Direction.Down);

                        if (newBeam.CheckPosition(maxX, maxY))
                        {
                            tmpNewBeams.Add(newBeam);
                        }

                        break;
                    }
            }

            if (beam.CheckPosition(maxX, maxY))
            {
                tmpNewBeams.Add(beam);
            }
        }

        currentBeams = new List<Beam>();
        foreach (var beam in tmpNewBeams)
        {
            if (!cache.Contains((beam.CurrentPosition, beam.CurrentDirection)))
            {
                currentBeams.Add(beam);
                cache.Add((beam.CurrentPosition, beam.CurrentDirection));
            }
        }
    }

    return visited.Count;
}