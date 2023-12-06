string[] input = File.ReadAllLines("input.txt");

var racesPart1 = GetRaces(input, true);
var racesPart2 = GetRaces(input, false);

Console.WriteLine("Part 1: " + Calculate(racesPart1));
Console.WriteLine("Part 2: " + Calculate(racesPart2));

Console.ReadKey();

List<Race> GetRaces(string[] input, bool part1)
{
    List<long> times = null;
    List<long> distances = null;

    if (part1)
    {
        times = input[0].Split(new string[] { "Time:", " " }, StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
        distances = input[1].Split(new string[] { "Distance:", " " }, StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
    }
    else
    {
        var time = input[0].Split(new string[] { "Time:" }, StringSplitOptions.RemoveEmptyEntries)[0].Replace(" ", string.Empty);
        times = new List<long>() { long.Parse(time) };

        var distance = input[1].Split(new string[] { "Distance:" }, StringSplitOptions.RemoveEmptyEntries)[0].Replace(" ", string.Empty);
        distances = new List<long>() { long.Parse(distance) };
    }

    var races = new List<Race>();
    for (int i = 0; i < times.Count; i++)
    {
        races.Add(new Race(times[i], distances[i]));
    }

    return races;
}

long Calculate(List<Race> races)
{
    int res = 1;
    foreach (var race in races)
    {
        int counter = 0;
        for (int j = 0; j < race.Time; j++)
        {
            if (j * (race.Time - j) > race.Distance)
            {
                counter++;
            }
        }

        if (counter != 0)
        {
            res *= counter;
        }
    }

    return res;
}

record Race(long Time, long Distance);
