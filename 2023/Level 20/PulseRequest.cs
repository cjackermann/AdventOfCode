namespace Level_20
{
    public class PulseRequest
    {
        public PulseRequest(string sender, Pulse pulse, string nextModule)
        {
            Sender = sender;
            Pulse = pulse;
            NextModule = nextModule;
        }

        public string Sender { get; }

        public Pulse Pulse { get; }

        public string NextModule { get; }
    }
}
