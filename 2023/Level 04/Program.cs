string[] input = File.ReadAllLines("input.txt");

PartOne(input);

Console.ReadKey();

void PartOne(string[] input)
{
    var games = from line in input
                let blocks = line.Split(new char[] { ':', '|' })
                let left = blocks[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                let right = blocks[2].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                select new { left, right };

    var result = games.Where(x => x.left.Intersect(x.right).Any()).Select(x => x.left.Intersect(x.right).Count()).ToList();

    double counter = 0;
    foreach (var res in result)
    {
        counter += Math.Pow(2, res - 1);
    }

    Console.WriteLine("Part 1: " + counter);
}