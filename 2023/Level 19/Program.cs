using System.Data;
using Level_19;

string input = File.ReadAllText("input.txt");

var splits = input.Split("\r\n\r\n");

var tmpPipes = splits[0].Split(["}\r\n", "}"], StringSplitOptions.RemoveEmptyEntries);
var pipes = new Dictionary<string, List<Condition>>();
foreach (var pipe in tmpPipes)
{
    var tmpPipe = pipe.Split("{");
    var key = tmpPipe[0];
    var values = tmpPipe[1].Split(",");

    pipes.Add(key, values.Select(x => new Condition(x)).ToList());
}

var tmpParts = splits[1].Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
var parts = new List<Part>();
foreach (var part in tmpParts)
{
    var tmpPart = part.Replace("{", string.Empty).Replace("}", string.Empty);
    var partsSplitted = tmpPart.Split(",");
    parts.Add(new Part(partsSplitted[0].Substring(2), partsSplitted[1].Substring(2), partsSplitted[2].Substring(2), partsSplitted[3].Substring(2)));
}

Part1();
Part2();

Console.ReadKey();

void Part1()
{
    var acceptedParts = new List<Part>();
    foreach (var part in parts)
    {
        var currentPipe = pipes["in"];

        while (true)
        {
            string nextPipe = CalculateAccepted(part, pipes, currentPipe);
            if (nextPipe == "A")
            {
                acceptedParts.Add(part);
                break;
            }
            else if (nextPipe == "R")
            {
                break;
            }
            else
            {
                currentPipe = pipes[nextPipe];
            }
        }
    }

    Console.WriteLine("Part 1: " + acceptedParts.Sum(x => x.Result));
}

void Part2()
{
    var dict = new Dictionary<string, CheckHelper>
    {
        { "x", new CheckHelper(0, 4001) },
        { "m", new CheckHelper(0, 4001) },
        { "a", new CheckHelper(0, 4001) },
        { "s", new CheckHelper(0, 4001) },
    };

    var result = CountAccepted(pipes, dict, "in");

    Console.WriteLine("Part 2: " + result);
}

long CountAccepted(Dictionary<string, List<Condition>> pipes, Dictionary<string, CheckHelper> dict, string currentKey)
{
    var items = pipes[currentKey];
    long result = 0;

    foreach (var item in items)
    {
        if (item.Operator == null)
        {
            result += CheckRecursive(pipes, dict, item);
        }
        else
        {
            if (item.Operator == "<")
            {
                var newHelper = dict.ToDictionary();
                var helper = dict[item.Category];
                newHelper[item.Category] = helper.CheckLessThan(item.CheckValue);
                dict[item.Category] = helper.CheckMoreThan(item.CheckValue - 1);

                result += CheckRecursive(pipes, newHelper, item);
            }
            else if (item.Operator == ">")
            {
                var newHelpers = dict.ToDictionary();
                var helper = dict[item.Category];
                newHelpers[item.Category] = helper.CheckMoreThan(item.CheckValue);
                dict[item.Category] = helper.CheckLessThan(item.CheckValue + 1);

                result += CheckRecursive(pipes, newHelpers, item);
            }
        }
    }

    return result;
}

long CheckRecursive(Dictionary<string, List<Condition>> conditions, Dictionary<string, CheckHelper> dict, Condition item)
{
    if (item.NextPipe == "A")
    {
        return dict.Values.Select(x => x.ValidNumbers()).Aggregate((long)1, (a, b) => a * b);
    }
    else if (item.NextPipe == "R")
    {
        return 0;
    }
    else
    {
        return CountAccepted(conditions, dict, item.NextPipe);
    }
}

static string CalculateAccepted(Part part, Dictionary<string, List<Condition>> pipes, List<Condition> currentPipe)
{
    string result = null;

    foreach (var condition in currentPipe)
    {
        if (condition.Operator != null)
        {
            if (part.CheckCondition(condition.Category, condition.Operator, condition.CheckValue))
            {
                result = condition.NextPipe;
                break;
            }
        }
        else
        {
            result = condition.NextPipe;
            break;
        }
    }

    return result;
}