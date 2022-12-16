string[] input = File.ReadAllLines("input.txt");

var valves = from line in input
             let parts = line.Split(new string[] { "Valve ", " has flow rate=", "; tunnels lead to valves ", "; tunnel leads to valve " }, StringSplitOptions.RemoveEmptyEntries)
             let name = parts[0]
             let flowRate = int.Parse(parts[1])
             let neighbours = parts[2].Split(", ").ToList()
             select new Valve(name, flowRate, neighbours);

Dictionary<(string Valve, string Neighbour), int> dict = new();

var unopenedValves = valves.Where(v => v.FlowRate > 0).ToHashSet();
var result = GetPressure(unopenedValves, valves.First());

Console.WriteLine(result);
Console.ReadKey();

int? GetPathCost(string valve, string neighbour, HashSet<string> visitedValves)
{
    if (dict.TryGetValue((valve, neighbour), out var cached))
    {
        return cached;
    }

    if (valve == neighbour)
    {
        return null;
    }

    int? best = null;

    visitedValves.Add(valve);

    var fromValve = valves.FirstOrDefault(d => d.Name == valve);

    if (fromValve.Neighbours.Contains(neighbour))
    {
        best = 1;
    }
    else
    {
        foreach (var neighbor in fromValve.Neighbours)
        {
            if (!visitedValves.Contains(neighbor))
            {
                var possibility = GetPathCost(neighbor, neighbour, visitedValves);
                if (possibility != null)
                {
                    if (best == null || possibility.Value + 1 < best.Value)
                    {
                        best = possibility.Value + 1;
                    }
                }
            }
        }
    }

    visitedValves.Remove(valve);

    if (best == null)
    {
        return null;
    }

    dict[(valve, neighbour)] = best.Value;
    return best.Value;
}

int GetPressure(HashSet<Valve> unopenedValves, Valve currentValve, int pressure = 0, int minute = 0)
{
    if (minute == 30 || unopenedValves.Count == 0)
    {
        return pressure;
    }

    var highestPressure = pressure;
    var minutesLeft = 30 - minute;

    foreach (var valve in unopenedValves.ToArray())
    {
        var pathCost = GetPathCost(currentValve.Name, valve.Name, new HashSet<string>());

        if (pathCost != null && minutesLeft > pathCost.Value)
        {
            unopenedValves.Remove(valve);

            var newMinute = minute + pathCost.Value + 1;
            var possibility = GetPressure(unopenedValves, valve, pressure + (30 - newMinute) * valve.FlowRate, newMinute);

            unopenedValves.Add(valve);

            if (possibility > highestPressure)
            {
                highestPressure = possibility;
            }
        }
    }

    return highestPressure;
}

public class Valve
{
    public Valve(string name, int flowRate, List<string> neighbours)
    {
        Name = name;
        FlowRate = flowRate;
        Neighbours = neighbours;
    }

    public string Name { get; }

    public int FlowRate { get; }

    public List<string> Neighbours { get; }

    public bool IsOpen { get; set; }
}