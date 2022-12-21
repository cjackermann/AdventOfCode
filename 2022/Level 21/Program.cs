string[] input = File.ReadAllLines("input.txt");

var statements = from line in input
                 let sides = line.Split(": ")
                 let key = sides[0]
                 let action = sides[1]
                 select new Statement(key, action);

PartOne(statements.ToList());
PartTwo(statements.ToList());

static void PartOne(List<Statement> statements)
{
    var resultDict = new Dictionary<string, long>();
    int count = 0;

    while (true)
    {
        if (resultDict.ContainsKey("root"))
        {
            break;
        }

        if (DoCalculation(resultDict, statements[count]))
        {
            statements.Remove(statements[count]);
            count = 0;
        }
        else
        {
            count++;
        }
    }

    Console.WriteLine("Stage 1: " + resultDict["root"]);
}

static void PartTwo(List<Statement> statements)
{
    var resultDict = new Dictionary<string, long>();
    int count = 0;
    HashSet<string> needsMe = new() { "humn" };

    while (true)
    {
        if (resultDict.ContainsKey("root") || count == statements.Count)
        {
            break;
        }

        if (statements[count].Key == "humn")
        {
            count++;
            continue;
        }
        else if (!char.IsNumber(statements[count].Action[0]))
        {
            var sides = statements[count].Action.Split(" ");
            string leftSide = sides[0];
            string rightSide = sides[2];

            if (needsMe.Contains(leftSide) || needsMe.Contains(rightSide))
            {
                needsMe.Add(statements[count].Key);
                count++;
                continue;
            }
        }

        if (DoCalculation(resultDict, statements[count]))
        {
            statements.Remove(statements[count]);
            count = 0;
        }
        else
        {
            count++;
        }
    }

    var rootStatement = statements.FirstOrDefault(d => d.Key == "root");
    var rootActions = rootStatement.Action.Split(" ");
    string leftRootAction = rootActions[0];
    string rightRootAction = rootActions[2];
    long rootValue = resultDict.FirstOrDefault(d => leftRootAction == d.Key || rightRootAction == d.Key).Value;
    resultDict.Add(leftRootAction, rootValue);
    needsMe.RemoveWhere(d => d == "root" || d == "humn");

    long currentCalculationValue = rootValue;
    foreach (var statementKey in needsMe.Reverse())
    {
        var currentStatement = statements.FirstOrDefault(d => d.Key == statementKey);
        var parts = currentStatement.Action.Split(" ");
        string left = parts[0];
        string action = parts[1];
        string right = parts[2];

        if (resultDict.ContainsKey(left))
        {
            long res = 0;
            if (action == "+")
            {
                res = currentCalculationValue - resultDict[left];
            }
            else if (action == "-")
            {
                res = resultDict[left] - currentCalculationValue;
            }
            else if (action == "*")
            {
                res = currentCalculationValue / resultDict[left];
            }
            else if (action == "/")
            {
                res = resultDict[left] / currentCalculationValue;
            }

            currentCalculationValue = res;
            resultDict.Add(right, res);
        }
        else if (resultDict.ContainsKey(right))
        {
            long res = 0;
            if (action == "+")
            {
                res = currentCalculationValue - resultDict[right];
            }
            else if (action == "-")
            {
                res = currentCalculationValue + resultDict[right];
            }
            else if (action == "*")
            {
                res = currentCalculationValue / resultDict[right];
            }
            else if (action == "/")
            {
                res = currentCalculationValue * resultDict[right];
            }

            currentCalculationValue = res;
            resultDict.Add(left, res);
        }
    }

    Console.WriteLine("Stage 2: " + currentCalculationValue);
}

Console.ReadKey();

static bool DoCalculation(Dictionary<string, long> resultDict, Statement statement)
{
    long? result = null;
    if (!char.IsNumber(statement.Action[0]))
    {
        long? leftNumber = null;
        long? rightNumber = null;

        var parts = statement.Action.Split(" ");
        string calculationValue1 = parts[0];
        string action = parts[1];
        string calculationValue2 = parts[2];

        if (resultDict.TryGetValue(calculationValue1, out var leftValue))
        {
            leftNumber = leftValue;
        }

        if (resultDict.TryGetValue(calculationValue2, out var rightValue))
        {
            rightNumber = rightValue;
        }

        if (leftNumber.HasValue && rightNumber.HasValue)
        {
            if (action == "+")
            {
                result = leftNumber + rightNumber;
            }
            else if (action == "-")
            {
                result = leftNumber - rightNumber;
            }
            else if (action == "*")
            {
                result = leftNumber * rightNumber;
            }
            else if (action == "/")
            {
                result = leftNumber / rightNumber;
            }

            resultDict.Add(statement.Key, result.Value);
            return true;
        }
    }
    else
    {
        resultDict.Add(statement.Key, int.Parse(statement.Action));
        return true;
    }

    return false;
}

public record Statement(string Key, string Action);