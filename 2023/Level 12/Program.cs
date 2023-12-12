string[] input = File.ReadAllLines("input.txt");

long counter = 0;
foreach (var line in input)
{
    var split = line.Split(' ');
    var left = split[0];
    var rightParts = split[1].Split(',').Select(x => Convert.ToInt32(x));
    int partsCounter = 0;

    string currentCheck = string.Empty;
    for (int i = 0; i < left.Length; i++)
    {
        if (left[i] == '?')
        {
            if (i == 0 || (i > 0 && left[i - 1] != '#'))
            {
                currentCheck += left[i];
            }
        }
        else
        {
            if (currentCheck != string.Empty)
            {
                if (i > 0 && left[i - 1] == '#')
                {
                    currentCheck = currentCheck.Substring(1);
                }
                
                if (left[i] == '#')
                {
                    currentCheck = currentCheck.Substring(0, currentCheck.Length - 1);
                }

                var count = currentCheck.Length;

                List<int> parts = new List<int>();
                foreach (var part in rightParts.Skip(partsCounter))
                {
                    if (count / 2 > part && count / 2 > parts.Count)
                    {
                        parts.Add(part);
                    }
                }

                partsCounter++;
            }

            currentCheck = string.Empty;
        }
    }
}

Console.WriteLine("Part 1: " + counter);
Console.ReadKey();