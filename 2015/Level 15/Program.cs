string[] input = File.ReadAllLines("input.txt");

var ingredients = (from line in input
                   let parts = line.Split(new string[] { ", ", " " }, StringSplitOptions.None)
                   let name = parts[0].Replace(":", string.Empty)
                   select new Ingredient(name, int.Parse(parts[2]), int.Parse(parts[4]), int.Parse(parts[6]), int.Parse(parts[8]), int.Parse(parts[10]))).ToArray();

int maxNormalScore = 0;
int maxCaloriesScore = 0;

for (int a = 1; a <= 98; a++)
{
    for (int b = 1; b <= 99 - a; b++)
    {
        for (int c = 1; c <= 100 - a - b; c++)
        {
            int d = 100 - a - b  - c;

            int capacity = a * ingredients[0].Capacity + b * ingredients[1].Capacity + c * ingredients[2].Capacity + d * ingredients[3].Capacity;
            int durability = a * ingredients[0].Durability + b * ingredients[1].Durability + c * ingredients[2].Durability + d * ingredients[3].Durability;
            int flavour = a * ingredients[0].Flavour + b * ingredients[1].Flavour + c * ingredients[2].Flavour + d * ingredients[3].Flavour;
            int texture = a * ingredients[0].Texture + b * ingredients[1].Texture + c * ingredients[2].Texture + d * ingredients[3].Texture;
            int calories = a * ingredients[0].Calories + b * ingredients[1].Calories + c * ingredients[2].Calories + d * ingredients[3].Calories;

            if (capacity > 0 && durability > 0 && flavour > 0 && texture > 0)
            {
                var currentScore = capacity * durability * flavour * texture;

                if (currentScore > maxNormalScore)
                {
                    maxNormalScore = currentScore;
                }

                if (calories == 500 && currentScore > maxCaloriesScore)
                {
                    maxCaloriesScore = currentScore;
                }
            }
        }
    }
}

Console.WriteLine("Stage 1: " + maxNormalScore);
Console.WriteLine("Stage 2: " + maxCaloriesScore);

record Ingredient(string Name, int Capacity, int Durability, int Flavour, int Texture, int Calories);
