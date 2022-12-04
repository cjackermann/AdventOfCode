string[] input = File.ReadAllLines("input.txt");

var pairs = from line in input
            let blocks = line.Split(new char[] { ',', '-' })
            let elve1 = Enumerable.Range(int.Parse(blocks[0]), int.Parse(blocks[1]) - int.Parse(blocks[0]) + 1)
            let elve2 = Enumerable.Range(int.Parse(blocks[2]), int.Parse(blocks[3]) - int.Parse(blocks[2]) + 1)
            select new Pair(elve1, elve2);

//PartOne(pairs);
PartTwo(pairs);

static void PartOne(IEnumerable<Pair> pairs)
{
    int overlapping = 0;
    foreach (var pair in pairs)
    {
        var intersect = pair.Elve1.Intersect(pair.Elve2).ToList();
        if (intersect.Count == pair.Elve1.Count() || intersect.Count == pair.Elve2.Count())
        {
            overlapping++;
        }
    }

    Console.WriteLine(overlapping);
}

static void PartTwo(IEnumerable<Pair> pairs)
{
    int overlapping = 0;
    foreach (var pair in pairs)
    {
        if (pair.Elve1.Intersect(pair.Elve2).Any())
        {
            overlapping++;
        }
    }

    Console.WriteLine(overlapping);
}

record Pair(IEnumerable<int> Elve1, IEnumerable<int> Elve2);