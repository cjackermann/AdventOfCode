namespace Level_20
{
    public abstract class ModuleBase
    {
        public string Name { get; set; }

        public List<string> NextModules { get; set; }

        public abstract (long High, long Low, List<PulseRequest> NextPulses) SendPulse(string sender, Pulse pulse, Dictionary<string, ModuleBase> modules);

        public abstract void Reset();
    }
}
