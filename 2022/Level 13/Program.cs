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

    var checkResult = CheckSuccesfull(left, right);
    if (checkResult == false)
    {
        isRightOrder = false;
    }

    if (isRightOrder)
    {
        result += counter;
    }

    Console.ForegroundColor = checkResult ?? false ? ConsoleColor.Green : ConsoleColor.Red;
    Console.WriteLine(counter);

    Console.ResetColor();
    Console.WriteLine(Print(left));
    Console.WriteLine(Print(right));

    static bool? CheckSuccesfull(Element left, Element right)
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
            if (!leftArray1.SubElements.Any() && rightArray1.SubElements.Any())
            {
                return true;
            }

            for (int i = 0; i < leftArray1.SubElements.Count; i++)
            {
                if (i >= rightArray1.SubElements.Count)
                {
                    return false;
                }

                var checkResult = CheckSuccesfull(leftArray1.SubElements[i], rightArray1.SubElements[i]);
                if (checkResult == false)
                {
                    return false;
                }
                else if (checkResult == true)
                {
                    return true;
                }
            }

            if (leftArray1.SubElements.Count < rightArray1.SubElements.Count)
            {
                return true;
            }

            return null;
        }
        else if (left is Array leftArray2 && right is Number rightNumber2)
        {
            var subNumbers = leftArray2.GetSubNumbers().ToList();
            if (subNumbers.Count == 0)
            {
                return true;
            }

            for (int i = 0; i < subNumbers.Count; i++)
            {
                var checkResult = CheckSuccesfull(subNumbers[i], rightNumber2);
                if (checkResult == false)
                {
                    return false;
                }
                else if (checkResult == true)
                {
                    return true;
                }
                else if (subNumbers.Count > 1)
                {
                    return false;
                }
            }

            return null;
        }
        else if (left is Number leftNumber2 && right is Array rightArray2)
        {
            var subNumbers = rightArray2.GetSubNumbers().ToList();
            if (subNumbers.Count == 0)
            {
                return false;
            }

            for (int i = 0; i < subNumbers.Count; i++)
            {
                var checkResult = CheckSuccesfull(leftNumber2, subNumbers[i]);
                if (checkResult == false)
                {
                    return false;
                }
                else if (checkResult == true)
                {
                    return true;
                }
                else if (subNumbers.Count > 1)
                {
                    return true;
                }
            }

            return null;
        }

        return true;
    }
}

Console.Write(result);

static string Print(Array array)
{
    List<string> result = new();

    foreach (var item in array.SubElements)
    {
        if (item is Number v) result.Add(v.Value.ToString());
        else if (item is Array l) result.Add(Print(l));
    }

    return "[" + string.Join(",", result) + "]";
}

static Array GetElements(string input)
{
    Array root = null;
    Array current = null;

    var tmpNumber = string.Empty;
    foreach (var charakter in input)
    {
        if (charakter == '[')
        {
            Array newElement = new(current);
            if (current != null)
            {
                current.SubElements.Add(newElement);
            }
            else
            {
                root = newElement;
            }

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
        foreach (var element in SubElements)
        {
            if (element is Number num)
            {
                yield return num;
            }
            else if (element is Array arr)
            {
                foreach (var number in arr.GetSubNumbers())
                {
                    yield return number;
                }
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