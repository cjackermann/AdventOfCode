namespace Level_19
{
    public class Condition
    {
        public Condition(string input)
        {
            if (input.Contains(":"))
            {
                var parts = input.Split(":");

                var left = parts[0];
                Category = left.Substring(0, 1);
                Operator = left.Substring(1, 1);
                CheckValue = long.Parse(left.Substring(2));

                NextPipe = parts[1];
            }
            else
            {
                NextPipe = input;
            }
        }

        public string Category { get; }

        public string Operator { get; }

        public long CheckValue { get; }

        public string NextPipe { get; }
    }
}
