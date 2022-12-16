string[] input = File.ReadAllLines("input.txt");

var valves = from line in input
             let parts = line.Split(new string[] { "Valve ", " has flow rate=", "; tunnels lead to valves ", "; tunnel leads to valve " }, StringSplitOptions.RemoveEmptyEntries)
             let name = parts[0]
             let flowRate = int.Parse(parts[1])
             let neighbours = parts[2].Split(", ").ToList()
             select new Valve(name, flowRate, neighbours);

Dictionary<(string Valve, string Neighbour), int> dict = new();

//var closedNeedableValves = valves.Where(v => v.FlowRate > 0).ToHashSet();
//var stage1Result = GetPressure(closedNeedableValves, valves.First(d => d.Name == "AA"), 0, 0);
//Console.WriteLine("Stage 1: " + stage1Result);

var closedNeedableValveNames = valves.Where(v => v.FlowRate > 0).ToHashSet();
var stage2Result = GetPressureWithElefant(closedNeedableValveNames, valves.First(d => d.Name == "AA"), valves.First(d => d.Name == "AA"), 0, 4, 4);
Console.WriteLine("Stage 2: " + stage2Result);

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
                var pathCost = GetPathCost(valves.FirstOrDefault(d => d.Name == neighbour), currentNeighbour, visitedValves);
                if (pathCost != null && (score == null || pathCost.Value + 1 < score.Value))
                {
                    score = pathCost.Value + 1;
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

    var maxPressure = pressure;
    var minutesLeft = 30 - minute;

    foreach (var closedNeedableValve in closedNeedableValves.ToArray())
    {
        var pathCost = GetPathCost(currentValve, closedNeedableValve.Name, new HashSet<string>());
        if (pathCost != null && minutesLeft > pathCost.Value)
        {
            closedNeedableValves.Remove(closedNeedableValve);

            var newMinute = minute + pathCost.Value + 1;
            var possibleMaxPressure = GetPressure(closedNeedableValves, closedNeedableValve, pressure + (30 - newMinute) * closedNeedableValve.FlowRate, newMinute);

            closedNeedableValves.Add(closedNeedableValve);

            if (possibleMaxPressure > maxPressure)
            {
                maxPressure = possibleMaxPressure;
            }
        }
    }

    return maxPressure;
}

int GetPressureWithElefant(HashSet<Valve> closedNeedableValves, Valve valveMe, Valve valveElefant, int pressure, int minutesMe, int minutesElefant)
{
    if (minutesMe == 30 || closedNeedableValves.Count == 0)
    {
        return pressure;
    }

    var maxPressure = pressure;
    var minutesLeftForMe = 30 - minutesMe;
    var minutesLeftForElefant = 30 - minutesElefant;

    foreach (var closedNeedableValveMe in closedNeedableValves.ToArray())
    {
        var pahtCostMe = GetPathCost(valveMe, closedNeedableValveMe.Name, new HashSet<string>());
        if (pahtCostMe != null && minutesLeftForMe > pahtCostMe.Value)
        {
            var newMinuteMe = minutesMe + pahtCostMe.Value + 1;
            var newPressureMe = pressure + (30 - newMinuteMe) * closedNeedableValveMe.FlowRate;

            closedNeedableValves.Remove(closedNeedableValveMe);

            foreach (var closedNeedableValveElefant in closedNeedableValves.ToArray())
            {
                var pathCostElefant = GetPathCost(valveElefant, closedNeedableValveElefant.Name, new HashSet<string>());
                if (pathCostElefant != null && minutesLeftForElefant > pathCostElefant.Value)
                {
                    closedNeedableValves.Remove(closedNeedableValveElefant);

                    var newMinuteElefant = minutesElefant + pathCostElefant.Value + 1;
                    var newPressureElefant = newPressureMe + (30 - newMinuteElefant) * closedNeedableValveElefant.FlowRate;

                    var possibleMaxPressureWithElefant = GetPressureWithElefant(closedNeedableValves, closedNeedableValveMe, closedNeedableValveElefant, newPressureElefant, newMinuteMe, newMinuteElefant);
                    
                    closedNeedableValves.Add(closedNeedableValveElefant);

                    if (possibleMaxPressureWithElefant > maxPressure)
                    {
                        maxPressure = possibleMaxPressureWithElefant;
                    }
                }
            }

            var possibleMaxPressureOnlyMe = GetPressureWithElefant(closedNeedableValves, closedNeedableValveMe, valveElefant, newPressureMe, newMinuteMe, minutesElefant);
            if (possibleMaxPressureOnlyMe > maxPressure)
            {
                maxPressure = possibleMaxPressureOnlyMe;
            }

            closedNeedableValves.Add(closedNeedableValveMe);
        }
    }

    return maxPressure;
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
}