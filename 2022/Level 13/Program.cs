string[] input = File.ReadAllText("input.txt").Split("\r\n\r\n");

int result = 0;
int counter = 0;
foreach (var line in input)
{
    counter++;
    bool isRightOrder = false;

    var parts = line.Split("\r\n");
    foreach (var list in parts)
    {
        var test = list.Replace("[]", "-1");
        var split = test.Split(new string[] { "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
    }

    if (isRightOrder)
    {
        result += counter;
    }
}

Console.Write(result);

List<Test> GetElements(string input)
{
    List<Test> result = new();
    Test current = null;
    foreach (var charakter in input)
    {
        Test test = null;
        if (charakter == '[')
        {
            test = new Test(current);
            current?.Subs.Add(test);

            current = test;
        }
    }
}

public class Test
{
    public Test(Test parent)
    {
        Parent = parent;
    }

    public Test Parent { get; set; }

    public List<Test> Subs { get; set; }

    public List<int> Elements { get; set; }
}