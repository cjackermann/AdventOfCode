string[] input = File.ReadAllText("input.txt").Split("\r\n\r\n");

var monkeys = (from monkey in input
               let parts = monkey.Split('\n')
               let monkeyId = int.Parse(parts[0].Substring(7, 1))
               let startingItems = parts[1].Split(new string[] { "  Starting items: ", ", " }, StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList()
               let operation = parts[2].Split("  Operation: new = old ", StringSplitOptions.RemoveEmptyEntries).First().Trim()
               let test = long.Parse(parts[3].Split("  Test: divisible by ", StringSplitOptions.RemoveEmptyEntries).First())
               let trueMonkeyId = int.Parse(parts[4].Split("    If true: throw to monkey ", StringSplitOptions.RemoveEmptyEntries).First())
               let falseMonkeyId = int.Parse(parts[5].Split("    If false: throw to monkey ", StringSplitOptions.RemoveEmptyEntries).First())
               select new Monkey(monkeyId, startingItems, operation, test, trueMonkeyId, falseMonkeyId)).ToList();

long part2Helper = 1;
foreach (var monkey in monkeys)
{
    part2Helper *= monkey.Test;
}

for (int i = 0; i < 10000; i++)
{
    foreach (var monkey in monkeys)
    {
        monkey.InspectedItemsCount += monkey.Items.Count;
        var operation = monkey.Operation.Split(' ');
        List<(int NewMonkeyId, long Item)> newAssignments = new();

        for (int itemCount = 0; itemCount < monkey.Items.Count; itemCount++)
        {
            var item = monkey.Items[itemCount];

            long worryLevel = item;
            long multiplierValue = operation[1] == "old" ? worryLevel : long.Parse(operation[1]);

            if (operation[0] == "+")
            {
                worryLevel = item + multiplierValue;
            }
            else
            {
                worryLevel = item * multiplierValue;
            }

            //worryLevel = worryLevel / 3; -> only needed in part 1
            if (worryLevel % monkey.Test != 0)
            {
                newAssignments.Add((monkey.FalseMonkeyId, worryLevel % part2Helper));
            }
            else
            {
                newAssignments.Add((monkey.TrueMonkeyId, worryLevel % part2Helper));
            }
        }

        monkey.Items.Clear();
        newAssignments.ForEach(d => monkeys.FirstOrDefault(x => x.Id == d.NewMonkeyId).Items.Add(d.Item));
    }
}

foreach (var monkey in monkeys)
{
    Console.WriteLine($"Monkey {monkey.Id} inspected items {monkey.InspectedItemsCount} times.");
}

Console.WriteLine();
var neededMonkeys = monkeys.OrderByDescending(d => d.InspectedItemsCount).Take(2).Select(d => d.InspectedItemsCount);
Console.WriteLine("Result: " + neededMonkeys.First() * neededMonkeys.Last());

public class Monkey
{
    public Monkey(int id, List<long> startingItems, string operation, long test, int trueMonkeyId, int falseMonkeyId)
    {
        Id = id;
        Items = startingItems;
        Operation = operation;
        Test = test;
        TrueMonkeyId = trueMonkeyId;
        FalseMonkeyId = falseMonkeyId;
    }

    public int Id { get; }

    public List<long> Items { get; set; }

    public string Operation { get; }

    public long Test { get; }

    public int TrueMonkeyId { get; }

    public int FalseMonkeyId { get; }

    public long InspectedItemsCount { get; set; }
}