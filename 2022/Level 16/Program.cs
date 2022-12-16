string[] input = File.ReadAllLines("input.txt");

var valves = from line in input
             let parts = line.Split(new string[] { "Valve ", " has flow rate=", "; tunnels lead to valves ", "; tunnel leads to valve " }, StringSplitOptions.RemoveEmptyEntries)
             let name = parts[0]
             let flowRate = int.Parse(parts[1])
             let neighbours = parts[2].Split(", ").ToList()
             select new Valve(name, flowRate, neighbours);

Dictionary<(string Valve, string Neighbour), int> dict = new();

var closedNeedableValves = valves.Where(v => v.FlowRate > 0).ToHashSet();
var result = GetPressure(closedNeedableValves, valves.First(d => d.Name == "AA"), 0, 0);

Console.WriteLine(result);
Console.ReadKey();

int? GetPathCost(Valve currentValve, string currentNeighbour, HashSet<string> visitedValves)
{
    if (dict.TryGetValue((currentValve.Name, currentNeighbour), out var value))
    {
        return value;
    }

    visitedValves.Add(currentValve.Name);

    int? score = null;
    if (currentValve.Neighbours.Contains(currentNeighbour))
    {
        score = 1;
    }
    else
    {
        foreach (var neighbour in currentValve.Neighbours)
        {
            if (!visitedValves.Contains(neighbour))
            {
                var possibility = GetPathCost(valves.FirstOrDefault(d => d.Name == neighbour), currentNeighbour, visitedValves);
                if (possibility != null)
                {
                    if (score == null || possibility.Value + 1 < score.Value)
                    {
                        score = possibility.Value + 1;
                    }
                }
            }
        }
    }

    visitedValves.Remove(currentValve.Name);

    if (score != null)
    {
        dict[(currentValve.Name, currentNeighbour)] = score.Value;
        return score.Value;
    }

    return null;
}

int GetPressure(HashSet<Valve> closedNeedableValves, Valve currentValve, int pressure, int minute)
{
    if (minute == 30 || closedNeedableValves.Count == 0)
    {
        return pressure;
    }

    var highestPressure = pressure;
    var minutesLeft = 30 - minute;

    foreach (var closedNeedableValve in closedNeedableValves.ToArray())
    {
        if (currentValve == closedNeedableValve)
        {
            continue;
        }

        var pathCost = GetPathCost(currentValve, closedNeedableValve.Name, new HashSet<string>());

        if (pathCost != null && minutesLeft > pathCost.Value)
        {
            closedNeedableValves.Remove(closedNeedableValve);

            var newMinute = minute + pathCost.Value + 1;
            var possibleMaxPressure = GetPressure(closedNeedableValves, closedNeedableValve, pressure + (30 - newMinute) * closedNeedableValve.FlowRate, newMinute);

            closedNeedableValves.Add(closedNeedableValve);

            if (possibleMaxPressure > highestPressure)
            {
                highestPressure = possibleMaxPressure;
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