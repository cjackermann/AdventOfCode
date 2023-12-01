string[] input = File.ReadAllLines("input.txt");

PartOne(input);
PartTwo(input);

Console.ReadKey();

void PartOne(string[] input)
{
    int counter = 0;
    foreach (string line in input)
    {
        var numbers = line.Where(char.IsNumber).ToList();
        if (numbers.Count >= 2)
        {
            counter += Convert.ToInt32((numbers.First().ToString() + numbers.Last().ToString()));
        }
        else if (numbers.Count == 1)
        {
            counter += Convert.ToInt32((numbers.First().ToString() + numbers.First().ToString()));
        }
    }

    Console.WriteLine("Part 1: " + counter);
}

void PartTwo(string[] input)
{
    int counter = 0;
    foreach (string line in input)
    {
        var lineReplaced = line;
        lineReplaced = lineReplaced.Replace("one", "o1one1e");
        lineReplaced = lineReplaced.Replace("two", "t2two2o");
        lineReplaced = lineReplaced.Replace("three", "t3three3e");
        lineReplaced = lineReplaced.Replace("four", "f4four4r");
        lineReplaced = lineReplaced.Replace("five", "f5five5e");
        lineReplaced = lineReplaced.Replace("six", "s6six6x");
        lineReplaced = lineReplaced.Replace("seven", "s7seven7n");
        lineReplaced = lineReplaced.Replace("eight", "e8eight8t");
        lineReplaced = lineReplaced.Replace("nine", "n9nine9e");

        var numbers = lineReplaced.Where(char.IsNumber).ToList();
        if (numbers.Count >= 2)
        {
            counter += Convert.ToInt32(numbers.First().ToString() + numbers.Last().ToString());
        }
        else if (numbers.Count == 1)
        {
            counter += Convert.ToInt32(numbers.First().ToString() + numbers.First().ToString());
        }
    }

    Console.WriteLine("Part 2: " + counter);
}
