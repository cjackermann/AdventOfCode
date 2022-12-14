namespace Level_13
{
    public class Container : Element
    {
        public Container(Container parent)
            : base(parent)
        {
        }

        public List<Element> SubElements { get; set; } = new List<Element>();

        public IEnumerable<Number> GetSubNumbers()
        {
            foreach (var element in SubElements)
            {
                if (element is Number num)
                {
                    yield return num;
                }
                else if (element is Container arr)
                {
                    if (arr.SubElements.OfType<Number>().Any())
                    {
                        foreach (var number in arr.GetSubNumbers())
                        {
                            yield return number;
                        }
                    }
                    else
                    {
                        yield return new Number(arr, -1);
                    }
                }
            }
        }
    }
}