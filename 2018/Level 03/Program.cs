using System.ComponentModel.Design.Serialization;

string[] input = File.ReadAllLines("input.txt");

var data = from line in input
           let parts = line.Split(new string[] { "#", " @ ", ",", ": ", "x" }, StringSplitOptions.RemoveEmptyEntries)
           let id = int.Parse(parts[0])
           let startPoint = new Point(int.Parse(parts[2]), int.Parse(parts[1]))
           let wide = int.Parse(parts[3])
           let tall = int.Parse(parts[4])
           select new Present(id, startPoint, wide, tall);

Dictionary<Point, (int Count, HashSet<int> Ids)> dict = new();
foreach (var present in data)
{
    for (int y = present.StartPoint.Y; y < present.StartPoint.Y + present.Wide; y++)
    {
        for (int x = present.StartPoint.X; x < present.StartPoint.X + present.Tall; x++)
        {
            var point = new Point(x, y);
            if (dict.ContainsKey(point))
            {
                var oldEntry = dict[point];
                var newEntry = (oldEntry.Count + 1, oldEntry.Ids.Union(new List<int> { present.Id }).ToHashSet());
                dict[point] = newEntry;
            }
            else
            {
                dict.Add(point, (1, new HashSet<int> { present.Id }));
            }
        }
    }
}

Console.WriteLine("Part 1: " + dict.Count(d => d.Value.Count > 1));

List<int> ids = data.Select(d => d.Id).ToList();
List<int> allMultiUsedIds = dict.Where(d => d.Value.Count > 1).SelectMany(d => d.Value.Ids).Distinct().ToList();
Console.WriteLine("Part 2: " + ids.Except(allMultiUsedIds).First());

public record Present(int Id, Point StartPoint, int Wide, int Tall);

public record Point(int X, int Y);