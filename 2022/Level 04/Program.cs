string[] input = File.ReadAllLines("input.txt");

//PartOne(input);
PartTwo(input);

static void PartOne(string[] input)
{
    var pairs = from line in input
               let blocks = line.Split(new char[] { ',', '-' })
               let elve1 = Enumerable.Range(int.Parse(blocks[0]), int.Parse(blocks[1]) - int.Parse(blocks[0]) + 1)
               let elve2 = Enumerable.Range(int.Parse(blocks[2]), int.Parse(blocks[3]) - int.Parse(blocks[2]) + 1)
               select new { elve1, elve2 };

    int overlapping = 0;
    foreach (var pair in pairs)
    {
        var intersect = pair.elve1.Intersect(pair.elve2).ToList();
        if (intersect.Count == pair.elve1.Count() || intersect.Count == pair.elve2.Count())
        {
            overlapping++;
        }
    }

    Console.WriteLine(overlapping);
}

static void PartTwo(string[] input)
{
    var pairs = from line in input
                     let blocks = line.Split(new char[] { ',', '-' })
                     let elve1 = Enumerable.Range(int.Parse(blocks[0]), int.Parse(blocks[1]) - int.Parse(blocks[0]) + 1)
                     let elve2 = Enumerable.Range(int.Parse(blocks[2]), int.Parse(blocks[3]) - int.Parse(blocks[2]) + 1)
                     select new { elve1, elve2 };

    int overlapping = 0;
    foreach (var pair in pairs)
    {
        if (pair.elve1.Intersect(pair.elve2).Any())
        {
            overlapping++;
        }
    }

    Console.WriteLine(overlapping);
}