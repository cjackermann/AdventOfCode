string[] input = File.ReadAllLines("input.txt");

var blueprints = from line in input
                 let parts = line.Split(new string[] { "Blueprint ", ": Each ore robot costs ", " ore. Each clay robot costs ", " ore. Each obsidian robot costs ", " ore and ", " clay. Each geode robot costs ", " ore and ", " obsidian." }, StringSplitOptions.RemoveEmptyEntries)
                 let id = int.Parse(parts[0])
                 let oreRobotCost = int.Parse(parts[1])
                 let clayRobotCost = int.Parse(parts[2])
                 let obsidianRobotCost = (int.Parse(parts[3]), int.Parse(parts[4]))
                 let geodeRobotCost = (int.Parse(parts[5]), int.Parse(parts[6]))
                 select new Blueprint(id, oreRobotCost, clayRobotCost, obsidianRobotCost, geodeRobotCost);

int result = 0;
foreach (var blueprint in blueprints)
{
    result += blueprint.Id * Calculate(blueprint, 24);
}

Console.WriteLine("Stage 1: " + result);

result = 1;
foreach (var blueprint in blueprints.Take(3))
{
    result *= Calculate(blueprint, 32);
}

Console.WriteLine("Stage 2: " + result);
Console.ReadKey();

static int Calculate(Blueprint blueprint, int minutes)
{
    var previousRounds = new HashSet<Round>();
    var rounds = new HashSet<Round> { new Round(0, 1, 0, 0, 0, 0, 0, 0, minutes) };
    int geodeCount = 0;

    while (true)
    {
        var round = rounds.LastOrDefault();
        if (round == null)
        {
            break;
        }

        rounds.Remove(round);

        if (round.Time == 0)
        {
            if (round.GeodeCount > geodeCount)
            {
                geodeCount = round.GeodeCount;
            }

            continue;
        }

        int maxOreCost = new List<int> { blueprint.OreRobotCost, blueprint.ClayRobotCost, blueprint.ObsidianRobotCost.Ore, blueprint.GeodeRobotCost.Ore }.Max();
        round = round with
        {
            OreRobotsCount = Math.Min(round.OreRobotsCount, maxOreCost),
            OreCount = Math.Min(round.OreCount, round.Time * maxOreCost - round.OreRobotsCount * (round.Time - 1)),
            ClayRobotsCount = Math.Min(round.ClayRobotsCount, blueprint.ObsidianRobotCost.Clay),
            ClayCount = Math.Min(round.ClayCount, round.Time * blueprint.ObsidianRobotCost.Clay - round.ClayRobotsCount * (round.Time - 1)),
            ObsidianRobotsCount = Math.Min(round.ObsidianRobotsCount, blueprint.GeodeRobotCost.Obsidian),
            ObsidianCount = Math.Min(round.ObsidianCount, round.Time * blueprint.GeodeRobotCost.Obsidian - round.ObsidianRobotsCount * (round.Time - 1)),
        };

        if (previousRounds.Contains(round))
        {
            continue;
        }

        previousRounds.Add(round);

        if (round.OreCount >= blueprint.GeodeRobotCost.Ore && round.ObsidianCount >= blueprint.GeodeRobotCost.Obsidian)
        {
            rounds.Add(round with
            {
                OreCount = round.OreCount + round.OreRobotsCount - blueprint.GeodeRobotCost.Ore,
                ClayCount = round.ClayCount + round.ClayRobotsCount,
                ObsidianCount = round.ObsidianCount + round.ObsidianRobotsCount - blueprint.GeodeRobotCost.Obsidian,
                GeodeCount = round.GeodeCount + round.GeodeRobotsCount,
                GeodeRobotsCount = round.GeodeRobotsCount + 1,
                Time = round.Time - 1,
            });
        }

        if (round.OreCount >= blueprint.ObsidianRobotCost.Ore && round.ClayCount >= blueprint.ObsidianRobotCost.Clay)
        {
            rounds.Add(round with
            {
                OreCount = round.OreCount + round.OreRobotsCount - blueprint.ObsidianRobotCost.Ore,
                ClayCount = round.ClayCount + round.ClayRobotsCount - blueprint.ObsidianRobotCost.Clay,
                ObsidianCount = round.ObsidianCount + round.ObsidianRobotsCount,
                ObsidianRobotsCount = round.ObsidianRobotsCount + 1,
                GeodeCount = round.GeodeCount + round.GeodeRobotsCount,
                Time = round.Time - 1,
            });
        }

        if (round.OreCount >= blueprint.ClayRobotCost)
        {
            rounds.Add(round with
            {
                OreCount = round.OreCount + round.OreRobotsCount - blueprint.ClayRobotCost,
                ClayCount = round.ClayCount + round.ClayRobotsCount,
                ClayRobotsCount = round.ClayRobotsCount + 1,
                ObsidianCount = round.ObsidianCount + round.ObsidianRobotsCount,
                GeodeCount = round.GeodeCount + round.GeodeRobotsCount,
                Time = round.Time - 1,
            });
        }

        if (round.OreCount >= blueprint.OreRobotCost)
        {
            rounds.Add(round with
            {
                OreCount = round.OreCount + round.OreRobotsCount - blueprint.OreRobotCost,
                OreRobotsCount = round.OreRobotsCount + 1,
                ClayCount = round.ClayCount + round.ClayRobotsCount,
                ObsidianCount = round.ObsidianCount + round.ObsidianRobotsCount,
                GeodeCount = round.GeodeCount + round.GeodeRobotsCount,
                Time = round.Time - 1,
            });
        }

        rounds.Add(round with
        {
            OreCount = round.OreCount + round.OreRobotsCount,
            ClayCount = round.ClayCount + round.ClayRobotsCount,
            ObsidianCount = round.ObsidianCount + round.ObsidianRobotsCount,
            GeodeCount = round.GeodeCount + round.GeodeRobotsCount,
            Time = round.Time - 1,
        });
    }

    return geodeCount;
}

public record Blueprint(int Id, int OreRobotCost, int ClayRobotCost, (int Ore, int Clay) ObsidianRobotCost, (int Ore, int Obsidian) GeodeRobotCost);

public record Round(int OreCount, int OreRobotsCount, int ClayCount, int ClayRobotsCount, int ObsidianCount, int ObsidianRobotsCount, int GeodeCount, int GeodeRobotsCount, int Time);