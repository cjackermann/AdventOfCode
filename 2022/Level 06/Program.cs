string input = File.ReadAllText("input.txt");

//GetCalculation(input, 4);
GetCalculation(input, 14);

static void GetCalculation(string input, int count)
{
    for (int i = 0; i < input.Length; i++)
    {
        List<char> list = new(input.Substring(i, count));
        if (list.Distinct().Count() == count)
        {
            Console.WriteLine(i + count);
            Console.WriteLine(i + count);
            break;
        }
    }
}
