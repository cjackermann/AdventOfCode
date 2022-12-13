string[] input = File.ReadAllText("input.txt").Split("\r\n\r\n");

int result = 0;
int counter = 0;
foreach (var line in input)
{
    counter++;
    bool isRightOrder = true;

    var parts = line.Split("\r\n");
    var left = GetElements(parts[0]);
    var right = GetElements(parts[1]);

    for (int i = 0; i < left.SubElements.Count; i++)
    {
        if (CheckSuccesfull(i, left, right) == false)
        {
            isRightOrder = false;
            break;
        }
    }

    if (isRightOrder)
    {
        result += counter;
    }

    bool? CheckSuccesfull(int index, Element left, Element right)
    {
        if (left is Number leftNumber1 && right is Number rightNumber1)
        {
            if (leftNumber1.Value > rightNumber1.Value)
            {
                return false;
            }
            else if (leftNumber1.Value < rightNumber1.Value)
            {
                return true;
            }

            return null;
        }
        else if (left is Array leftArray1 && right is Array rightArray1)
        {
            if (leftArray1.SubElements.Count > rightArray1.SubElements.Count)
            {
                return false;
            }

            for (int i = 0; i < leftArray1.SubElements.Count; i++)
            {
                var checkResult = CheckSuccesfull(i, leftArray1.SubElements[i], rightArray1.SubElements[i]);
                if (checkResult == false)
                {
                    return false;
                }
                else if (checkResult == true)
                {
                    return true;
                }
            }
        }
        else if (left is Array leftArray2 && right is Number rightNumber2)
        {
            var subNumbers = leftArray2.GetSubNumbers();
            if (!subNumbers.Any())
            {
                return false;
            }

            if (subNumbers.First().Value > rightNumber2.Value)
            {
                return false;
            }
        }
        else if (left is Number leftNumber2 && right is Array rightArray2)
        {
            var subNumbers = rightArray2.GetSubNumbers();
            if (!subNumbers.Any())
            {
                return true;
            }

            if (rightArray2.GetSubNumbers().First().Value < leftNumber2.Value)
            {
                return false;
            }
        }

        return true;
    }
}

Console.Write(result);
Console.ReadKey();

static Array GetElements(string input)
{
    Array root = new(null);
    Array current = root;

    var tmpNumber = string.Empty;
    foreach (var charakter in input)
    {
        if (charakter == '[')
        {
            Array newElement = new(current);
            current.SubElements.Add(newElement);
            current = newElement;
        }
        else if (charakter == ']')
        {
            if (tmpNumber.Length > 0)
            {
                current.SubElements.Add(new Number(current, int.Parse(tmpNumber)));
                tmpNumber = string.Empty;
            }

            if (current.Parent != null)
            {
                current = current.Parent;
            }
        }
        else if (charakter == ',')
        {
            if (tmpNumber.Length > 0)
            {
                current.SubElements.Add(new Number(current, int.Parse(tmpNumber)));
                tmpNumber = string.Empty;
            }
        }
        else
        {
            tmpNumber += charakter;
        }
    }

    return root;
}

public class Number : Element
{
    public Number(Array parent, int value)
        : base(parent)
    {
        Value = value;
    }

    public int Value { get; set; }
}

public class Array : Element
{
    public Array(Array parent)
        : base(parent)
    {
    }

    public List<Element> SubElements { get; set; } = new List<Element>();

    public IEnumerable<Number> GetSubNumbers()
    {
        foreach (var number in SubElements.OfType<Number>())
        {
            yield return number;
        }

        foreach (var array in SubElements.OfType<Array>())
        {
            foreach (var number in array.GetSubNumbers())
            {
                yield return number;
            }
        }
    }
}

public abstract class Element
{
    public Element(Array parent)
    {
        Parent = parent;
    }

    public Array Parent { get; }
}