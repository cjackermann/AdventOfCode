namespace Level_13
{
    public abstract class Element
    {
        public Element(Container parent)
        {
            Parent = parent;
        }

        public Container Parent { get; }
    }
}