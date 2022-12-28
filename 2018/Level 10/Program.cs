string[] input = File.ReadAllLines("input.txt");

var points = (from line in input
              let parts = line.Split(new string[] { "position=<", ",", "> velocity=<", ",", ">" }, StringSplitOptions.RemoveEmptyEntries)
              let positionX = int.Parse(parts[0].Trim())
              let positionY = int.Parse(parts[1].Trim())
              let veloX = int.Parse(parts[2].Trim())
              let veloY = int.Parse(parts[3].Trim())
              select new Position(new Point(positionX, positionY), veloX, veloY)).ToList();

int xMin = points.Select(d => d.CurrentPoint).Min(d => d.X);
int yMin = points.Select(d => d.CurrentPoint).Min(d => d.Y);
int xMax = points.Select(d => d.CurrentPoint).Max(d => d.X);
int yMax = points.Select(d => d.CurrentPoint).Max(d => d.Y);
var seconds = 0;

while (true)
{
    var temp = points.Select(x => x).ToList();
    for (int i = 0; i < points.Count; i++)
    {
        var p = points[i];
        points[i] = new Position(new Point(p.CurrentPoint.X + p.XVelo, p.CurrentPoint.Y + p.YVelo), p.XVelo, p.YVelo);
    }

    var newMinX = points.Select(d => d.CurrentPoint).Min(x => x.X);
    var newMinY = points.Select(d => d.CurrentPoint).Min(x => x.Y);
    var newMaxX = points.Select(d => d.CurrentPoint).Max(x => x.X);
    var newMaxY = points.Select(d => d.CurrentPoint).Max(x => x.Y);

    if ((newMaxX - newMinX) > (xMax - xMin) || (newMaxY - newMinY) > (yMax - yMin))
    {
        Console.WriteLine(seconds);
        for (int y = newMinY; y <= newMaxY; y++)
        {
            for (int x = newMinX; x <= newMaxX; x++)
            {
                if (temp.Select(d => d.CurrentPoint).Contains(new Point(x, y)))
                {
                    Console.Write("#");
                }
                else
                {
                    Console.Write(".");
                }
            }

            Console.Write("\r\n");
        }

        Console.ReadKey();
        Console.Clear();
    }

    xMin = newMinX;
    yMin = newMinY;
    xMax = newMaxX;
    yMax = newMaxY;
    seconds++;
}

public record Position(Point CurrentPoint, int XVelo, int YVelo);

public record Point(int X, int Y);