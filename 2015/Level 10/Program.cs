using System.Text;

GetResult(40);
GetResult(50);

static void GetResult(int count)
{
    string input = File.ReadAllText("input.txt");

    for (int i = 0; i < count; i++)
    {
        StringBuilder tmpInput = new();

        char currentCharakter = input.First();
        int charakterCount = 0;
        foreach (var charakter in input)
        {
            if (currentCharakter != charakter)
            {
                tmpInput.Append(charakterCount + currentCharakter.ToString());
                currentCharakter = charakter;
                charakterCount = 1;
            }
            else
            {
                charakterCount++;
            }
        }

        tmpInput.Append(charakterCount + currentCharakter.ToString());

        input = tmpInput.ToString();
    }

    Console.WriteLine(input.Length);
}
