using Level_13;

//PartOne();
PartTwo();

static void PartOne()
{
    string[] input = File.ReadAllText("input.txt").Split("\r\n\r\n");

    int result = 0;
    int counter = 0;
    foreach (var line in input)
    {
        counter++;

        var parts = line.Split("\r\n");
        var left = GetElements(parts[0]);
        var right = GetElements(parts[1]);

        var checkResult = CheckSuccesfull(left, right);
        if (checkResult == true)
        {
            result += counter;
        }

        Console.ForegroundColor = checkResult ?? false ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine(counter);
        Console.ResetColor();
        Console.WriteLine(Print(left));
        Console.WriteLine(Print(right));
        Console.WriteLine();
    }

    Console.Write(result);
}

static void PartTwo()
{
    string[] input = File.ReadAllText("input.txt").Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

    List<Packet> lines = new();
    for (int i = 0; i < input.Length; i++)
    {
        lines.Add(new Packet(GetElements(input[i]), i, false));
    }

    lines.Add(new Packet(GetElements("[[2]]"), lines.Count, true));
    lines.Add(new Packet(GetElements("[[6]]"), lines.Count, true));

    Packet[] sortedLines = lines.ToArray();
    while (true)
    {
        bool isCorrectOrder = true;

        sortedLines = sortedLines.OrderBy(d => d.Index).ToArray();
        for (int i = 0; i < sortedLines.Length - 1; i++)
        {
            var leftContainer = sortedLines[i].Container;
            var rightContainer = sortedLines[i + 1].Container;

            var checkResult = CheckSuccesfull(leftContainer, rightContainer);
            if (checkResult != true)
            {
                isCorrectOrder = false;
                sortedLines[i] = sortedLines[i] with { Index = i + 1 };
                sortedLines[i + 1] = sortedLines[i + 1] with { Index = i };
                break;
            }
        }

        if (isCorrectOrder)
        {
            sortedLines = sortedLines.OrderBy(d => d.Index).ToArray();
            break;
        }
    }

    foreach (var line in sortedLines)
    {
        Console.WriteLine(Print(line.Container));
    }

    Console.WriteLine();
    Console.WriteLine(sortedLines.Where(d => d.Divider).First());
    Console.WriteLine(sortedLines.Where(d => d.Divider).Last());
    int result = sortedLines.Where(d => d.Divider).Select(d => d.Index + 1).Aggregate((x, y) => x * y);
    Console.WriteLine(result);
}

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
    else if (left is Container leftContainer1 && right is Container rightContainer1)
    {
        if (!leftContainer1.SubElements.Any() && rightContainer1.SubElements.Any())
        {
            return true;
        }

        for (int i = 0; i < leftContainer1.SubElements.Count; i++)
        {
            if (i >= rightContainer1.SubElements.Count)
            {
                return false;
            }

            var checkResult = CheckSuccesfull(leftContainer1.SubElements[i], rightContainer1.SubElements[i]);
            if (checkResult == false)
            {
                return false;
            }
            else if (checkResult == true)
            {
                return true;
            }
        }

        if (leftContainer1.SubElements.Count < rightContainer1.SubElements.Count)
        {
            return true;
        }

        return null;
    }
    else if (left is Container leftContainer2 && right is Number rightNumber2)
    {
        var subNumbers = leftContainer2.GetSubNumbers().ToList();
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
    else if (left is Number leftNumber2 && right is Container rightContainer2)
    {
        var subNumbers = rightContainer2.GetSubNumbers().ToList();
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

static string Print(Container container)
{
    List<string> result = new();

    foreach (var item in container.SubElements)
    {
        if (item is Number v) result.Add(v.Value.ToString());
        else if (item is Container l) result.Add(Print(l));
    }

    return "[" + string.Join(",", result) + "]";
}

static Container GetElements(string input)
{
    Container root = null;
    Container current = null;

    var tmpNumber = string.Empty;
    foreach (var charakter in input)
    {
        if (charakter == '[')
        {
            Container newElement = new(current);
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

public record Packet(Container Container, int Index, bool Divider);
