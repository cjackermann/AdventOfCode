using System.Text.RegularExpressions;

string[] input = File.ReadAllLines("input.txt");

PartOne(input);
PartTwo(input);

static void PartOne(string[] input)
{
    int codeCount = 0;
    int memoryCount = 0;

    foreach (var line in input)
    {
        memoryCount += Regex.Unescape(line[1..^1]).Length;
        codeCount += line.Length;
    }

    Console.WriteLine(codeCount - memoryCount);
}

static void PartTwo(string[] input)
{
    int originalCount = 0;
    int encodedCount = 0;

    foreach (var line in input)
    {
        encodedCount += ("\"" + line.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"").Length;
        originalCount += line.Length;
    }

    Console.WriteLine(encodedCount - originalCount);
}