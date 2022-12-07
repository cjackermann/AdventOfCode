using System.Text.Json;
using System.Text.RegularExpressions;

string input = File.ReadAllText("input.txt");

PartOne(input);
PartTwo(input);

static void PartOne(string input)
{
    int result = 0;
    input = Regex.Replace(input, @"[^\-?\d+]", " ");
    var numbers = input.Split(' ').Where(d => !string.IsNullOrWhiteSpace(d)).Select(int.Parse);
    foreach (int number in numbers)
    {
        result += number;
    }

    Console.WriteLine(result);
}

static void PartTwo(string input)
{
    var data = JsonDocument.Parse(input);
    var result = GetElementCount(data.RootElement);

    Console.WriteLine(result);
}

static int? GetElementCount(JsonElement element)
{
    int count = 0;

    if (element.ValueKind == JsonValueKind.Array)
    {
        foreach (var tmp in element.EnumerateArray())
        {
            count += GetElementCount(tmp) ?? 0;
        }
    }
    else if (element.ValueKind == JsonValueKind.Object)
    {
        foreach (var tmp in element.EnumerateObject())
        {
            var elementCount = GetElementCount(tmp.Value);

            if (elementCount == null && tmp.Value.ValueKind == JsonValueKind.String)
            {
                return null;
            }
            else
            {
                count += elementCount ?? 0;
            }

        }
    }
    else if (element.ValueKind == JsonValueKind.String)
    {
        if (element.GetString() == "red")
        {
            return null;
        }
    }
    else if (element.ValueKind == JsonValueKind.Number)
    {
        count = element.GetInt32();
    }

    return count;
}

Console.ReadKey();