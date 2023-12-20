using Level_20;

string[] input = File.ReadAllLines("input.txt");

var modules = new Dictionary<string, ModuleBase>();
foreach (var line in input)
{
    var parts = line.Split(" -> ");
    var nextModules = parts[1].Split(", ");

    if (parts[0] == "broadcaster")
    {
        modules.Add(parts[0], new BroadcasterModule(parts[0], nextModules.ToList()));
        continue;
    }

    var type = parts[0].Substring(0, 1);
    var name = parts[0].Substring(1);
    if (type == "%")
    {
        modules.Add(name, new FlipFlopModule(name, false, nextModules.ToList()));
    }
    else if (type == "&")
    {
        modules.Add(name, new ConjunctionModule(name, nextModules.ToList()));
    }
}

foreach (var conModule in modules.Where(x => x.Value is ConjunctionModule))
{
    var inputModules = modules.Where(x => x.Value.NextModules.Contains(conModule.Key)).Select(x => x.Key);
    foreach (var module in inputModules)
    {
        (conModule.Value as ConjunctionModule).InputModuleStates.Add(module, Pulse.Low);
    }
}

(long High, long Low) pulseCounter = (0, 0);
for (int i = 0; i < 1000; i++)
{
    List<PulseRequest> queue = [new PulseRequest(null, Pulse.Low, "broadcaster")];

    while (queue.Count != 0)
    {
        var currentRequest = queue.First();
        queue.Remove(queue.First());

        if (modules.TryGetValue(currentRequest.NextModule, out var currentModule))
        {
            var (high, low, nextPulses) = currentModule.SendPulse(currentRequest.Sender, currentRequest.Pulse, modules);

            pulseCounter = (pulseCounter.High + high, pulseCounter.Low + low);
            queue.AddRange(nextPulses);
        }
        else if (currentRequest.Pulse == Pulse.High)
        {
            pulseCounter.High++;
        }
        else
        {
            pulseCounter.Low++;
        }
    }
}

var result = pulseCounter.High * pulseCounter.Low;
Console.WriteLine("Part 1: " + result);

modules.Values.ToList().ForEach(x => x.Reset());

var rxDeliverants = modules.Values.Where(x => x.NextModules.Contains("hj")).ToDictionary(x => x.Name, x => (long)0); // -> hj works with my input @TBeer ;)
for (long i = 1; i < long.MaxValue; i++)
{
    List<PulseRequest> queue = [new PulseRequest(string.Empty, Pulse.Low, "broadcaster")];

    while (queue.Count != 0)
    {
        var currentRequest = queue.First();
        queue.Remove(queue.First());

        if (modules.TryGetValue(currentRequest.NextModule, out var currentModule))
        {
            var (high, low, nextPulses) = currentModule.SendPulse(currentRequest.Sender, currentRequest.Pulse, modules);
            queue.AddRange(nextPulses);
        }

        if (rxDeliverants.TryGetValue(currentRequest.Sender, out var value) && value == 0 && currentRequest.Pulse == Pulse.High)
        {
            rxDeliverants[currentRequest.Sender] = i;
        }
    }

    if (rxDeliverants.Values.All(x => x != 0))
    {
        break;
    }
}

long result2 = rxDeliverants.Values.Aggregate(GetLCM);
Console.WriteLine("Part 2: " + result2);
Console.ReadKey();

static long GetLCM(long a, long b)
{
    long num1, num2;
    if (a > b)
    {
        num1 = a; num2 = b;
    }
    else
    {
        num1 = b; num2 = a;
    }

    for (int i = 1; i < num2; i++)
    {
        long mult = num1 * i;
        if (mult % num2 == 0)
        {
            return mult;
        }
    }
    return num1 * num2;
}