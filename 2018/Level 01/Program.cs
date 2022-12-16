string[] input = File.ReadAllLines("input.txt");

// PartOne
int result = 0;
foreach (string line in input)
{
    result += int.Parse(line);
}

Console.WriteLine(result);

// PartTwo
List<int> values = new() { 0 };
int current = 0;
while (true)
{
    bool finished = false;
    foreach (string line in input)
    {
        current += int.Parse(line);
        if (values.Contains(current))
        {
            finished = true;
            break;
        }
        else
        {
            values.Add(current);
        }
    }

    if (finished)
    {
        Console.WriteLine(current);
        break;
    }
}