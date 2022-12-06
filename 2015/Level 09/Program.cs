string[] input = File.ReadAllLines("input.txt");

var journeys = from line in input
               let blocks = line.Split(new string[] { " to ", " = " }, StringSplitOptions.None)
               let start = blocks[0]
               let end = blocks[1]
               let distance = int.Parse(blocks[2])
               select new Journey(start, end, distance);

Console.WriteLine(GetRoutes(journeys).Min()); // = PartOne
Console.WriteLine(GetRoutes(journeys).Max()); // = PartTwo

static IEnumerable<int> GetRoutes(IEnumerable<Journey> journeys)
{
    var citys = journeys.Select(d => d.Start).Union(journeys.Select(d => d.End)).ToList();
    var possibleRoutes = GetLocations(citys);
    return GetRouteLenghts(possibleRoutes, journeys);
}

static IEnumerable<List<string>> GetLocations(IEnumerable<string> citys)
{
    foreach (var city in citys)
    {
        if (!citys.Where(c => c != city).Any())
        {
            yield return new List<string> { city };
        }
        else
        {
            var tmp = GetLocations(citys.Where(c => c != city));

            foreach (var t in tmp)
            {
                t.Insert(0, city);
                yield return t;
            }
        }
    }
}

static IEnumerable<int> GetRouteLenghts(IEnumerable<List<string>> routes, IEnumerable<Journey> journeys)
{
    foreach (var route in routes)
    {
        int result = 0;
        for (int i = 0; i < route.Count - 1; i++)
        {
            result += journeys.FirstOrDefault(d => (d.Start == route[i] || d.Start == route[i + 1]) && (d.End == route[i] || d.End == route[i + 1])).Distance;
        }

        yield return result;
    }
}

record Journey(string Start, string End, int Distance);