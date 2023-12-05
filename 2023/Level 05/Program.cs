
string input = File.ReadAllText("input.txt");
var groups = input.Split("\r\n\r\n");

var seeds = groups[0].Split(new string[] { "seeds:", " " }, StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();

var maps = new List<Map>();
foreach (var group in groups.Skip(1))
{
    var rows = group.Split("\r\n");
    var newMap = new Map();

    foreach (var row in rows.Skip(1))
    {
        newMap.Ranges.Add(new Helper(row));
    }

    maps.Add(newMap);
}

PartOne(seeds, maps);

Console.ReadKey();

void PartOne(List<long> seeds, List<Map> maps)
{
    long counter = long.MaxValue;
    foreach (var seed in seeds)
    {
        long internalCounter = seed;
        foreach (var map in maps)
        {
            internalCounter = map.GetNearest(internalCounter);
        }

        if (internalCounter < counter)
        {
            counter = internalCounter;
        }
    }

    Console.WriteLine("Part 1: " + counter);
}

class Map
{
    public List<Helper> Ranges = new List<Helper>();

    public long GetNearest(long number)
    {
        return Ranges.Min(x => x.CheckSeed(number)) ?? number;
    }
}

class Helper
{
    public Helper(string row)
    {
        var items = row.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        SourceStart = long.Parse(items[1].ToString());
        SourceEnd = long.Parse(items[1].ToString()) + long.Parse(items[2].ToString());

        DestinationStart = long.Parse(items[0].ToString());
        DestinationEnd = long.Parse(items[1].ToString()) + long.Parse(items[2].ToString());
    }

    public long SourceStart { get; }

    public long SourceEnd { get; }

    public long DestinationStart { get; }

    public long DestinationEnd { get; }

    public long? CheckSeed(long seed)
    {
        if (seed >= SourceStart && seed <= SourceEnd)
        {
            var tmp = seed - SourceStart;
            return DestinationStart + tmp;
        }

        return null;
    }
}