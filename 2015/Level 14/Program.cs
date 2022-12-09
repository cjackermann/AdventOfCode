string[] input = File.ReadAllLines("input.txt");

var reindeers = (from line in input
                 let parts = line.Split(new string[] { " can fly ", " km/s for ", " seconds, but then must rest for ", " seconds." }, StringSplitOptions.None)
                 let name = parts[0]
                 let speed = int.Parse(parts[1])
                 let duration = int.Parse(parts[2])
                 let rest = int.Parse(parts[3])
                 select new Reindeer(name, speed, duration, duration, rest, rest, 0, 0)).ToArray();

for (int second = 1; second <= 2503; second++)
{
    for (int i = 0; i < reindeers.Length; i++)
    {
        var reindeer = reindeers[i];
        if (reindeer.RemainingRest == 0)
        {
            reindeers[i] = reindeer with { Distance = reindeer.Distance + reindeer.Speed, RemainingDuration = reindeer.Duration - 1, RemainingRest = reindeer.Rest };
        }
        else if (reindeer.RemainingDuration > 0)
        {
            reindeers[i] = reindeer with { Distance = reindeer.Distance + reindeer.Speed, RemainingDuration = reindeer.RemainingDuration - 1 };
        }
        else if (reindeer.RemainingRest > 0)
        {
            reindeers[i] = reindeer with { RemainingRest = reindeer.RemainingRest - 1 };
        }
    }

    var currentMaxDistance = reindeers.Max(d => d.Distance);
    for (int i = 0; i < reindeers.Length; i++)
    {
        if (reindeers[i].Distance == currentMaxDistance)
        {
            reindeers[i] = reindeers[i] with { Points = reindeers[i].Points + 1 };
        }
    }
}

Console.WriteLine("Stage 1: " + reindeers.OrderByDescending(d => d.Distance).FirstOrDefault().Distance);
Console.WriteLine("Stage 2: " + reindeers.OrderByDescending(d => d.Points).FirstOrDefault().Points);

record Reindeer(string Name, int Speed, int Duration, int RemainingDuration, int Rest, int RemainingRest, int Distance, int Points);