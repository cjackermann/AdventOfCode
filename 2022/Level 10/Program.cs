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
        Printer.AddPoint(x);

        x += instruction.Count;
        instructions.Remove(instruction);
    }
    else
    {
        Printer.AddPoint(x);
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
Console.WriteLine(Printer.PrintRows());

static class Printer
{
    private static readonly List<int> _points = new();
    private static readonly List<string> _rows = new();
    private static string currentRow = string.Empty;

    public static string PrintRows() => string.Join(Environment.NewLine, _rows);

    public static void AddPoint(int newPoint)
    {
        _points.Add(newPoint);

        if (currentRow.Length == 39)
        {
            _rows.Add(currentRow);
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