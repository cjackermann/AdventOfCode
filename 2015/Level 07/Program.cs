string[] input = File.ReadAllLines("input.txt");

var statements = from line in input
                 let sides = line.Split(" -> ")
                 let calculation = sides[0].Split(' ')
                 let result = sides[1]
                 select new Statement(calculation, result);

var resultDict = new Dictionary<string, int>();
CalculateWires(statements.ToList(), resultDict); // = PartOne

// PartTwo = PartOne + a -> b + reset all wires except b + PartOne again
var valueA = resultDict["a"];
resultDict.Clear();
resultDict.Add("b", valueA);
CalculateWires(statements.ToList(), resultDict);

Console.WriteLine(resultDict["a"]);

static void CalculateWires(List<Statement> statements, Dictionary<string, int> resultDict)
{
    int count = 0;

    while (true)
    {
        if (!statements.Any())
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
}

static bool DoCalculation(Dictionary<string, int> resultDict, Statement statement)
{
    int? result = null;
    if (statement.Calculation.Length == 3)
    {
        ushort? leftNumber = null;
        ushort? rightNumber = null;

        string calculationValue1 = statement.Calculation[0];
        string action = statement.Calculation[1];
        string calculationValue2 = statement.Calculation[2];

        if (char.IsNumber(calculationValue1.First()))
        {
            leftNumber = ushort.Parse(calculationValue1);
        }
        else if (resultDict.TryGetValue(calculationValue1, out var value))
        {
            leftNumber = (ushort)value;
        }

        if (char.IsNumber(calculationValue2.First()))
        {
            rightNumber = ushort.Parse(calculationValue2);
        }
        else if (resultDict.TryGetValue(calculationValue2, out var value))
        {
            rightNumber = (ushort)value;
        }

        if (leftNumber.HasValue && rightNumber.HasValue)
        {
            if (action == "AND")
            {
                result = leftNumber & rightNumber;
            }
            else if (action == "OR")
            {
                result = leftNumber | rightNumber;
            }
            else if (action == "LSHIFT")
            {
                result = leftNumber << rightNumber;
            }
            else if (action == "RSHIFT")
            {
                result = leftNumber >> rightNumber;
            }
        }
    }
    else if (statement.Calculation.Length == 2)
    {
        var calculationValue = statement.Calculation[1];
        if (resultDict.TryGetValue(calculationValue, out var value))
        {
            result = (ushort)~value;
        }
    }
    else if (statement.Calculation.Length == 1)
    {
        var calculationValue = statement.Calculation[0];
        if (char.IsNumber(calculationValue[0]))
        {
            result = int.Parse(calculationValue);
        }
        else if (resultDict.TryGetValue(calculationValue, out int value))
        {
            result = value;
        }
    }

    if (result.HasValue)
    {
        if (resultDict.TryGetValue(statement.Result, out var value))
        {
            resultDict[statement.Result] = value + result.Value;
        }
        else
        {
            resultDict.Add(statement.Result, result.Value);
        }
        return true;
    }
    else
    {
        return false;
    }
}

record Statement(string[] Calculation, string Result);
