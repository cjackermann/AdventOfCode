string[] input = File.ReadAllLines("input.txt");

var instructions = (from line in input
                    let parts = line.Split(' ')
                    let operation = parts[0]
                    let count = operation != "noop" ? int.Parse(parts[1]) : 0
                    select new Instruction(operation, count, operation == "noop" ? 1 : 2)).ToList();

int counter = 1;
int x = 1;
int[] checkPoints = new int[] { 20, 60, 100, 140, 180, 220 };
int result = 0;

while (true)
{
    if (checkPoints.Contains(counter))
    {
        result += counter * x;
    }

    var instruction = instructions.FirstOrDefault();
    if (instruction.Duration == 1)
    {
        Printer.Print(x);

        x += instruction.Count;
        instructions.Remove(instruction);
    }
    else
    {
        Printer.Print(x);
        instructions[0] = instruction with { Duration = instruction.Duration - 1 };
    }

    if (!instructions.Any())
    {
        break;
    }

    counter++;
}

Console.WriteLine(result);
Console.WriteLine();
Console.WriteLine(string.Join(Environment.NewLine, Printer.Rows));

static class Printer
{
    private static readonly List<int> _points = new();
    private static string currentRow = string.Empty;

    public static List<string> Rows = new();

    public static void Print(int newPoint)
    {
        _points.Add(newPoint);

        if (currentRow.Length == 39)
        {
            Rows.Add(currentRow);
            currentRow = string.Empty;
        }
        else
        {
            var currentPosition = (_points.Count - 1) % 40;
            var possiblePositions = new List<int> { currentPosition, currentPosition - 1, currentPosition + 1 };

            if (possiblePositions.Contains(newPoint))
            {
                currentRow += "#";
            }
            else
            {
                currentRow += " ";
            }
        }
    }
}

record Instruction(string Operation, int Count, int Duration);