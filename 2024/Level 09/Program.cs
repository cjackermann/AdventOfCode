string input = File.ReadAllText("input.txt");

Part1(input);
Part2(input);

Console.ReadKey();

static void Part1(string input)
{
    bool empty = false;
    List<string> data = [];
    int idx = 0;

    for (int i = 0; i < input.Length; i++)
    {
        for (int x = 0; x < int.Parse(input[i].ToString()); x++)
        {
            if (empty)
            {
                data.Add(".");
            }
            else
            {
                data.Add(idx.ToString());
            }
        }

        idx = !empty ? idx + 1 : idx;
        empty = !empty;
    }

    long count = data.Count(x => x != ".");
    for (int i = 0; i < count; i++)
    {
        if (data[i] == ".")
        {
            var item = data.Last(x => x != ".");
            var lastIdx = data.LastIndexOf(item);

            data[i] = item;
            data[lastIdx] = ".";
        }
    }

    long result = 0;
    var tmpList = data.Where(x => x != ".").ToList();
    for (int i = 0; i < tmpList.Count; i++)
    {
        result += int.Parse(tmpList[i].ToString()) * i;
    }

    Console.WriteLine("Part 1: " + result);
}

static void Part2(string input)
{
    bool empty = false;
    List<Part> data = [];
    int idx = 0;

    for (int i = 0; i < input.Length; i++)
    {
        int count = int.Parse(input[i].ToString());

        if (count != 0)
        {
            var newPart = new Part(empty, count, []);
            for (int j = 0; j < count; j++)
            {
                newPart.Values.Add(empty ? -1 : idx);
            }

            data.Add(newPart);
        }

        idx = !empty ? idx + 1 : idx;
        empty = !empty;
    }

    for (int i = data.Count - 1; i >= 0; i--)
    {
        var currentItem = data[i];
        if (currentItem.IsFree)
        {
            continue;
        }

        var currenItemIdx = data.IndexOf(currentItem);

        var firstEmpty = data.FirstOrDefault(x => x.IsFree && x.Space >= currentItem.Space);
        if (firstEmpty == null || data.IndexOf(firstEmpty) > i)
        {
            continue;
        }

        var firstEmptyIdx = data.IndexOf(firstEmpty);
        data.RemoveAt(firstEmptyIdx);
        data.Insert(firstEmptyIdx, currentItem);
        data.RemoveAt(currenItemIdx);

        var backFiller = new Part(true, currentItem.Space, []);
        for (int j = 0; j < currentItem.Space; j++)
        {
            backFiller.Values.Add(-1);
        }
        data.Insert(currenItemIdx, backFiller);

        var toAddLength = firstEmpty.Space - currentItem.Space;
        if (toAddLength > 0)
        {
            var filler = new Part(true, toAddLength, []);
            for (int j = 0; j < toAddLength; j++)
            {
                filler.Values.Add(-1);
            }

            data.Insert(firstEmptyIdx + 1, filler);

            i++;
        }
    }

    long result = 0;
    var values = data.SelectMany(x => x.Values).ToList();
    for (int i = 0; i < values.Count; i++)
    {
        var value = values[i];
        if (value != -1)
        {
            result += value * i;
        }
    }

    Console.WriteLine("Part 2: " + result);
}

record Part(bool IsFree, int Space, List<long> Values);