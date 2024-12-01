string[] input = File.ReadAllLines("input.txt");

List<long> leftList = [];
List<long> rightList = [];
foreach (string line in input)
{
    var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    leftList.Add(long.Parse(parts[0]));
    rightList.Add(long.Parse(parts[1]));
}

Part1(leftList, rightList);
Part2(leftList, rightList);

Console.ReadKey();

static void Part1(List<long> leftList, List<long> rightList)
{
    leftList = leftList.OrderBy(x => x).ToList();
    rightList = rightList.OrderBy(x => x).ToList();

    long result = 0;
    for (int i = 0; i < leftList.Count; i++)
    {
        result += (long)Math.Abs(Convert.ToDecimal(rightList[i] - leftList[i]));
    }

    Console.WriteLine("Part1: " + result);
}

static void Part2(List<long> leftList, List<long> rightList)
{
    long result = 0;
    foreach (var item in leftList)
    {
        result += item * rightList.Count(x => x == item);
    }

    Console.WriteLine("Part2: " + result);
}