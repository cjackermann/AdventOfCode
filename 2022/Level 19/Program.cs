using Level_19;

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
foreach (var blueprint in blueprints.Take(1))
{
    result += blueprint.Id * Calculate(blueprint);
}

Console.WriteLine(result);
Console.ReadKey();

static int Calculate(Blueprint blueprint)
{
    List<Robot> activeRobots = new() { new Robot(RobotType.Ore) };

    var previousRounds = new HashSet<Round>();
    var rounds = new HashSet<Round> { new Round(0, 1, 0, 0, 0, 0, 0, 0, 24) };
    int geodeCount = 0;

    for (int minute = 1; minute <= 24; minute++)
    {
        var newRobots = BuildNewRobots().ToList();

        oreCount += activeRobots.Count(d => d.Type == RobotType.Ore);
        clayCount += activeRobots.Count(d => d.Type == RobotType.Clay);
        obsidianCount += activeRobots.Count(d => d.Type == RobotType.Obsidian);
        geodeCount += activeRobots.Count(d => d.Type == RobotType.Geode);

        activeRobots.AddRange(newRobots);
    }

    return geodeCount;

    IEnumerable<Robot> BuildNewRobots()
    {
        if (oreCount >= blueprint.GeodeRobotCost.Ore && obsidianCount >= blueprint.GeodeRobotCost.Obsidian)
        {
            yield return new Robot(RobotType.Geode);
            oreCount -= blueprint.GeodeRobotCost.Ore;
            obsidianCount -= blueprint.GeodeRobotCost.Obsidian;
        }

        if (oreCount >= blueprint.ObsidianRobotCost.Ore && clayCount >= blueprint.ObsidianRobotCost.Clay)
        {
            yield return new Robot(RobotType.Obsidian);
            oreCount -= blueprint.ObsidianRobotCost.Ore;
            clayCount -= blueprint.ObsidianRobotCost.Clay;
        }

        if (oreCount >= blueprint.ClayRobotCost)
        {
            yield return new Robot(RobotType.Clay);
            oreCount -= blueprint.ClayRobotCost;
        }
    }
}

public record Blueprint(int Id, int OreRobotCost, int ClayRobotCost, (int Ore, int Clay) ObsidianRobotCost, (int Ore, int Obsidian) GeodeRobotCost);

public record Robot(RobotType Type);

public enum RobotType
{
    Ore,
    Clay,
    Obsidian,
    Geode,
}