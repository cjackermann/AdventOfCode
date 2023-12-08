string[] input = File.ReadAllLines("input.txt");

char[] walks = input[0].ToArray();
var map = new Dictionary<string, (string Left, string Right)>();

foreach (var line in input.Skip(2))
{
    var tmp = line.Split(" = ");
    var values = tmp[1].Replace("(", string.Empty).Replace(")", string.Empty).Split(", ");

    map.Add(tmp[0], (values[0], values[1]));
}

PartOne(map, walks);
PartTwo(map, walks);

Console.ReadKey();

void PartOne(Dictionary<string, (string Left, string Right)> map, char[] walks)
{
    long counter = 0;
    var currentStep = map.First(x => x.Key == "AAA");
    bool found = false;

    while (!found)
    {
        for (int i = 0; i < walks.Length; i++)
        {
            var walk = walks[i];
            if (walk == 'L')
            {
                currentStep = map.First(x => x.Key == currentStep.Value.Left);
                counter++;
            }
            else
            {
                currentStep = map.First(x => x.Key == currentStep.Value.Right);
                counter++;
            }

            if (currentStep.Key == "ZZZ")
            {
                found = true;
                break;
            }
        }
    }

    Console.WriteLine("Part 1: " + counter);
}
void PartTwo(Dictionary<string, (string Left, string Right)> map, char[] walks)
{
    var steps = map.Where(x => x.Key.EndsWith("A")).ToList();

    List<long> tmpCounts = new List<long>();
    foreach (var step in steps)
    {
        var currentStep = step;
        long tmpCounter = 0;
        bool found = false;

        while (!found)
        {
            for (int i = 0; i < walks.Length; i++)
            {
                var walk = walks[i];
                if (walk == 'L')
                {
                    currentStep = map.First(x => x.Key == currentStep.Value.Left);
                }
                else
                {
                    currentStep = map.First(x => x.Key == currentStep.Value.Right);
                }

                tmpCounter++;

                if (currentStep.Key.EndsWith("Z"))
                {
                    found = true;
                    tmpCounts.Add(tmpCounter);
                    break;
                }
            }
        }
    }

    Console.WriteLine("Part 2: " + tmpCounts.Aggregate(GetLCM));
}

static long GetLCM(long a, long b)
{
    long num1, num2;
    if (a > b)
    {
        num1 = a; num2 = b;
    }
    else
    {
        num1 = b; num2 = a;
    }

    for (int i = 1; i < num2; i++)
    {
        long mult = num1 * i;
        if (mult % num2 == 0)
        {
            return mult;
        }
    }
    return num1 * num2;
}