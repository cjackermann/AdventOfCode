string input = File.ReadAllText("input.txt");

var parts = input.Split("\r\n\r\n");
var rules = parts[0].Split("\r\n").Select(x => x.Split("|").Select(x => long.Parse(x)).ToList()).ToList();
var pages = parts[1].Split("\r\n").Select(x => x.Split(",").Select(x => long.Parse(x)).ToList()).ToList();

Part1(rules, pages);
Part2(rules, pages);

Console.ReadKey();

static void Part1(List<List<long>> rules, List<List<long>> pages)
{
    long result = 0;
    foreach (var page in pages)
    {
        bool isCorrect = true;
        foreach (var rule in rules)
        {
            if (rule.TrueForAll(page.Contains))
            {
                var idx1 = page.IndexOf(rule.First());
                var idx2 = page.IndexOf(rule.Last());

                if (idx1 > idx2)
                {
                    isCorrect = false;
                    break;
                }
            }
        }

        if (isCorrect)
        {
            result += page[page.Count / 2];
        }
    }

    Console.WriteLine("Part 1: " + result);
}

static void Part2(List<List<long>> rules, List<List<long>> pages)
{
    long result = 0;
    foreach (var page in pages)
    {
        bool isCorrect = true;
        foreach (var rule in rules)
        {
            if (rule.TrueForAll(page.Contains))
            {
                var idx1 = page.IndexOf(rule.First());
                var idx2 = page.IndexOf(rule.Last());

                if (idx1 > idx2)
                {
                    isCorrect = false;
                    break;
                }
            }
        }

        if (!isCorrect)
        {
            var tmpNewPage = page.ToList();
            while (true)
            {
                bool tmpIsCorrect = true;

                foreach (var rule in rules)
                {
                    if (rule.TrueForAll(tmpNewPage.Contains))
                    {
                        var idx1 = tmpNewPage.IndexOf(rule.First());
                        var idx2 = tmpNewPage.IndexOf(rule.Last());

                        if (idx1 > idx2)
                        {
                            tmpNewPage.RemoveAt(idx1);
                            tmpNewPage.Insert(idx2, rule.First());

                            tmpIsCorrect = false;
                            break;
                        }
                    }
                }

                if (tmpIsCorrect)
                {
                    break;
                }
            }

            result += tmpNewPage[tmpNewPage.Count / 2];
        }
    }

    Console.WriteLine("Part 2: " + result);
}