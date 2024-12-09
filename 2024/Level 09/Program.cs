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
            if (empty)
            {
                data.Add(new Part(true, new string('.', count)));
            }
            else
            {
                var newPart = new Part(false, string.Empty);

                for (int j = 0; j < count; j++)
                {
                    newPart = newPart with { Data = newPart.Data + idx.ToString() };
                }

                data.Add(newPart);
            }
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

        var firstEmpty = data.FirstOrDefault(x => x.IsFree && x.Data.Length >= currentItem.Data.Length);
        if (firstEmpty == null || data.IndexOf(firstEmpty) > i)
        {
            continue;
        }

        var firstEmptyIdx = data.IndexOf(firstEmpty);
        data.RemoveAt(firstEmptyIdx);
        data.Insert(firstEmptyIdx, currentItem);
        data[currenItemIdx] = data[currenItemIdx] with { IsFree = true, Data = new string('.', currentItem.Data.Length)};

        var toAddLength = firstEmpty.Data.Length - currentItem.Data.Length;
        if (toAddLength > 0)
        {
            data.Insert(firstEmptyIdx + 1, new Part(true, new string('.', toAddLength)));
        }
    }

    long result = 0;
    string resultString = string.Join(string.Empty, data.SelectMany(x => x.Data).Select(x => x.ToString()));
    for (int i = 0; i < resultString.Length; i++)
    {
        if (resultString[i] != '.')
        {
            result += int.Parse(resultString[i].ToString()) * i;
        }
    }

    Console.WriteLine("Part 2: " + result);
}

record Part(bool IsFree, string Data);