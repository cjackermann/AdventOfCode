string[] input = File.ReadAllLines("input.txt");

IEnumerable<(string Time, string Action)> entries = from line in input
                                                    let parts = line.Split(new string[] { "[", "] " }, StringSplitOptions.RemoveEmptyEntries)
                                                    let time = parts[0]
                                                    let action = parts[1]
                                                    select (time, action);

Dictionary<int, List<int>> guardsSleepingTimes = new();
int currentGuardId = 0;
int startTime = 0;
foreach (var entry in entries.OrderBy(d => d.Time))
{
    if (entry.Action.StartsWith("Guard"))
    {
        currentGuardId = entry.Action.Split(new string[] { "Guard #", " begins shift" }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).First();

        if (!guardsSleepingTimes.ContainsKey(currentGuardId))
        {
            guardsSleepingTimes.Add(currentGuardId, new List<int>());
        }
    }
    else if (entry.Action.StartsWith("falls"))
    {
        startTime = int.Parse(entry.Time.Substring(entry.Time.Length - 2));

    }
    else if (entry.Action.StartsWith("wakes"))
    {
        var endTime = int.Parse(entry.Time.Substring(entry.Time.Length - 2));
        for (int i = startTime; i < endTime; i++)
        {
            guardsSleepingTimes[currentGuardId].Add(i);
        }
    }
}

var mostSleepingGuard = guardsSleepingTimes.OrderByDescending(d => d.Value.Count).FirstOrDefault();
var mostSleptMinute = mostSleepingGuard.Value.GroupBy(d => d).OrderByDescending(d => d.Count()).Select(d => d.Key).First();
Console.WriteLine("Part 1: " + mostSleepingGuard.Key * mostSleptMinute);

var mostSleepingMinuteByGuard = guardsSleepingTimes.Select(d => d.Value.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => new { GuardId = d.Key, Value = x.Key, Count = x.Count() }).Take(1)).SelectMany(d => d).ToList();
var mostSleepingGuardPerMinute = mostSleepingMinuteByGuard.OrderByDescending(d => d.Count).FirstOrDefault();
Console.WriteLine("Part 2: " + mostSleepingGuardPerMinute.GuardId * mostSleepingGuardPerMinute.Value);
