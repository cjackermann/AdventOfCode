string input = File.ReadAllText("input.txt");
var groups = input.Split("\r\n\r\n");

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

PartOne(maps);
PartTwo(maps);

Console.ReadKey();

void PartOne(List<Map> maps)
{
    var seeds = groups[0].Split(new string[] { "seeds:", " " }, StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();

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

void PartTwo(List<Map> maps)
{
    var seeds = new List<(long Start, long End)>();
    var splittedSeeds = groups[0].Split(new string[] { "seeds:", " " }, StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
    for (int i = 0; i < splittedSeeds.Count; i += 2)
    {
        long start = splittedSeeds[i];
        long count = splittedSeeds[i + 1];

        seeds.Add((start, start + count));
    }

    maps.Reverse();

    for (long i = 1; i < long.MaxValue; i++)
    {
        long internalCounter = i;
        foreach (var map in maps)
        {
            internalCounter = map.GetNearestReverse(internalCounter);
        }

        if (seeds.Any(x => x.Start <= internalCounter && x.End >= internalCounter))
        {
            Console.WriteLine("Part 2: " + i);
            break;
        }
    }
}

class Map
{
    public List<Helper> Ranges = new List<Helper>();

    public long GetNearest(long number)
    {
        return Ranges.Min(x => x.Check(number)) ?? number;
    }

    public long GetNearestReverse(long number)
    {
        return Ranges.Min(x => x.CheckReverse(number)) ?? number;
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
        DestinationEnd = long.Parse(items[0].ToString()) + long.Parse(items[2].ToString());
    }

    public long SourceStart { get; }

    public long SourceEnd { get; }

    public long DestinationStart { get; }

    public long DestinationEnd { get; }

    public long? Check(long number)
    {
        if (number >= SourceStart && number <= SourceEnd)
        {
            var tmp = number - SourceStart;
            return DestinationStart + tmp;
        }

        return null;
    }

    public long? CheckReverse(long number)
    {
        if (number >= DestinationStart && number <= DestinationEnd)
        {
            var tmp = number - DestinationStart;
            return SourceStart + tmp;
        }

        return null;
    }
}