string[] input = File.ReadAllLines("input.txt");

PartOne(input);
PartTwo(input);


void PartOne(string[] input)
{
    int counter = 0;
    foreach (string line in input)
    {
        var parts = line.Split(':');
        var leftPart = parts[0];

        var rightParts = parts[1].Split(';');
        bool add = true;
        foreach (var part in rightParts)
        {
            foreach (var t in part.Split(", "))
            {
                if (t.Contains("red") && Convert.ToInt32(t.Replace(" red", string.Empty).ToString()) > 12)
                {
                    add = false;
                    break;
                }
                else if (t.Contains("blue") && Convert.ToInt32(t.Replace(" blue", string.Empty).ToString()) > 14)
                {
                    add = false;
                    break;
                }
                else if (t.Contains("green") && Convert.ToInt32(t.Replace(" green", string.Empty).ToString()) > 13)
                {
                    add = false;
                    break;
                }
            }

            if (!add)
            {
                break;
            }
        }

        if (add)
        {
            counter += Convert.ToInt32(leftPart.Replace("Game ", string.Empty));
        }
    }

    Console.WriteLine("PartOne: " + counter);
}

void PartTwo(string[] input)
{
    int counter = 0;
    foreach (string line in input)
    {
        var parts = line.Split(':');
        var leftPart = parts[0];

        var rightParts = parts[1].Split(';');
        int minRed = 0;
        int minBlue = 0;
        int minGreen = 0;

        foreach (var part in rightParts)
        {
            foreach (var t in part.Split(", "))
            {
                if (t.Contains("red") && Convert.ToInt32(t.Replace(" red", string.Empty).ToString()) > minRed)
                {
                    minRed = Convert.ToInt32(t.Replace(" red", string.Empty).ToString());
                }
                else if (t.Contains("blue") && Convert.ToInt32(t.Replace(" blue", string.Empty).ToString()) > minBlue)
                {
                    minBlue = Convert.ToInt32(t.Replace(" blue", string.Empty).ToString());
                }
                else if (t.Contains("green") && Convert.ToInt32(t.Replace(" green", string.Empty).ToString()) > minGreen)
                {
                    minGreen = Convert.ToInt32(t.Replace(" green", string.Empty).ToString());
                }
            }
        }

        counter += minRed * minBlue * minGreen;
    }

    Console.WriteLine("PartTwo: " + counter);
}

Console.ReadKey();