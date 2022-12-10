string[] input = File.ReadAllLines("input.txt");

HashSet<Light> currentShiningLights = new();
for (int row = 0; row < input.Length; row++)
{
    for (int column = 0; column < input[row].Length; column++)
    {
        if (input[row][column] == '#')
        {
            currentShiningLights.Add(new Light(column, row));
        }
    }
}

// part two: add stucked lights
currentShiningLights.Add(new Light(0, 0));
currentShiningLights.Add(new Light(0, 99));
currentShiningLights.Add(new Light(99, 0));
currentShiningLights.Add(new Light(99, 99));

for (int i = 0; i < 100; i++)
{
    var tmpLights = new HashSet<Light>();

    for (int y = 0; y < 100; y++)
    {
        for (int x = 0; x < 100; x++)
        {
            var currentLight = new Light(x, y);
            int shiningNeighbourLights = GetShiningLights(currentShiningLights, currentLight);

            if (currentShiningLights.Contains(currentLight) && shiningNeighbourLights == 2)
            {
                tmpLights.Add(currentLight);
            }
            else if (shiningNeighbourLights == 3)
            {
                tmpLights.Add(currentLight);
            }
        }
    }

    // part two: add stucked lights
    tmpLights.Add(new Light(0, 0));
    tmpLights.Add(new Light(0, 99));
    tmpLights.Add(new Light(99, 0));
    tmpLights.Add(new Light(99, 99));

    currentShiningLights = tmpLights;
}

Console.WriteLine(currentShiningLights.Count);

static int GetShiningLights(HashSet<Light> currentShiningLights, Light currentLight)
{
    int shiningLights = 0;

    if (currentShiningLights.Contains(currentLight with { X = currentLight.X - 1 }))
    {
        shiningLights++;
    }

    if (currentShiningLights.Contains(currentLight with { X = currentLight.X + 1 }))
    {
        shiningLights++;
    }

    if (currentShiningLights.Contains(currentLight with { Y = currentLight.Y - 1 }))
    {
        shiningLights++;
    }

    if (currentShiningLights.Contains(currentLight with { Y = currentLight.Y + 1 }))
    {
        shiningLights++;
    }

    if (currentShiningLights.Contains(currentLight with { X = currentLight.X - 1, Y = currentLight.Y - 1 }))
    {
        shiningLights++;
    }

    if (currentShiningLights.Contains(currentLight with { X = currentLight.X - 1, Y = currentLight.Y + 1 }))
    {
        shiningLights++;
    }

    if (currentShiningLights.Contains(currentLight with { X = currentLight.X + 1, Y = currentLight.Y - 1 }))
    {
        shiningLights++;
    }

    if (currentShiningLights.Contains(currentLight with { X = currentLight.X + 1, Y = currentLight.Y + 1 }))
    {
        shiningLights++;
    }

    return shiningLights;
}

record Light(int X, int Y);