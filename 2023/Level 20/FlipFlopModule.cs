namespace Level_20
{
    public class FlipFlopModule : ModuleBase
    {
        public FlipFlopModule(string name, bool state, List<string> nextModules)
        {
            Name = name;
            State = state;
            NextModules = nextModules;
        }

        public bool State { get; private set; }

        public override (long High, long Low, List<PulseRequest> NextPulses) SendPulse(string sender, Pulse pulse, Dictionary<string, ModuleBase> modules)
        {
            (long High, long Low, List<PulseRequest> NextPulses) result = (0, 0, new List<PulseRequest>());

            if (pulse == Pulse.High)
            {
                result.High++;
            }
            else
            {
                result.Low++;
            }

            if (pulse == Pulse.Low)
            {
                State = !State;
                var tmpPulse = State ? Pulse.High : Pulse.Low;

                

                foreach (var nextModule in NextModules)
                {
                    result.NextPulses.Add(new PulseRequest(Name, tmpPulse, nextModule));
                }
            }

            return result;
        }

        public override void Reset() => State = false;
    }
}
