namespace Level_13
{
    public class Number : Element
    {
        public Number(Container parent, int value)
            : base(parent)
        {
            Value = value;
        }

        public int Value { get; set; }
    }
}