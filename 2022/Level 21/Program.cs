string[] input = File.ReadAllLines("input.txt");

var statements = (from line in input
                  let sides = line.Split(": ")
                  let key = sides[0]
                  let action = sides[1]
                  select new Statement(key, action)).ToList();

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

Console.WriteLine(resultDict["root"]);
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