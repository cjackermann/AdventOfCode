namespace Level_20
{
    public class BroadcasterModule : ModuleBase
    {
        public BroadcasterModule(string name, List<string> nextModules)
        {
            Name = name;
            NextModules = nextModules;
        }

        public override (long High, long Low, List<PulseRequest> NextPulses) SendPulse(string sender, Pulse pulse, Dictionary<string, ModuleBase> modules)
        {
            (long High, long Low, List<PulseRequest> NextPulses) result = (0, 1, new List<PulseRequest>());

            foreach (var nextModule in NextModules)
            {
                result.NextPulses.Add(new PulseRequest(Name, pulse, nextModule));
            }

            return result;
        }

        public override void Reset()
        {
        }
    }
}
