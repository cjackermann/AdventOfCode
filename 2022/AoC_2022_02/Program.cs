string[] input = File.ReadAllLines("input.txt");

PartOne(input);
PartTwo(input);

static void PartOne(string[] input)
{
    int count = 0;
    foreach (var line in input)
    {
        var split = line.Split(' ');
        if (split[0] == "A")
        {
            if (split[1] == "X")
            {
                count += 1 + 3;
            }
            else if (split[1] == "Y")
            {
                count += 2 + 6;
            }
            else
            {
                count += 3 + 0;
            }
        }
        else if (split[0] == "B")
        {
            if (split[1] == "X")
            {
                count += 1 + 0;
            }
            else if (split[1] == "Y")
            {
                count += 2 + 3;
            }
            else
            {
                count += 3 + 6;
            }
        }
        else
        {
            if (split[1] == "X")
            {
                count += 1 + 6;
            }
            else if (split[1] == "Y")
            {
                count += 2 + 0;
            }
            else
            {
                count += 3 + 3;
            }
        }
    }

    Console.WriteLine(count);
}

static void PartTwo(string[] input)
{
    int count = 0;
    foreach (var line in input)
    {
        var split = line.Split(' ');
        if (split[1] == "X")
        {
            if (split[0] == "A")
            {
                count += 3 + 0;
            }
            else if (split[0] == "B")
            {
                count += 1 + 0;
            }
            else
            {
                count += 2 + 0;
            }
        }
        else if (split[1] == "Y")
        {
            if (split[0] == "A")
            {
                count += 1 + 3;
            }
            else if (split[0] == "B")
            {
                count += 2 + 3;
            }
            else
            {
                count += 3 + 3;
            }
        }
        else
        {
            if (split[0] == "A")
            {
                count += 2 + 6;
            }
            else if (split[0] == "B")
            {
                count += 3 + 6;
            }
            else
            {
                count += 1 + 6;
            }
        }
    }

    Console.WriteLine(count);
}