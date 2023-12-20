namespace Level_20
{
    public class ConjunctionModule : ModuleBase
    {
        public ConjunctionModule(string name, List<string> nextModules)
        {
            Name = name;
            NextModules = nextModules;
        }

        public Dictionary<string, Pulse> InputModuleStates { get; } = new Dictionary<string, Pulse>();

        public override (long High, long Low, List<PulseRequest> NextPulses) SendPulse(string sender, Pulse pulse, Dictionary<string, ModuleBase> modules)
        {
            (long High, long Low, List<PulseRequest> NextPulses) result = (0, 0, new List<PulseRequest>());
            InputModuleStates[sender] = pulse;

            var tmpPulse = Pulse.High;
            if (InputModuleStates.Values.All(x => x == Pulse.High))
            {
                tmpPulse = Pulse.Low;
            }

            if (pulse == Pulse.High)
            {
                result.High++;
            }
            else
            {
                result.Low++;
            }

            foreach (var nextModule in NextModules)
            {
                result.NextPulses.Add(new PulseRequest(Name, tmpPulse, nextModule));
            }

            return result;
        }

        public override void Reset() => InputModuleStates.Values.ToList().ForEach(x => x = Pulse.Low);
    }
}
