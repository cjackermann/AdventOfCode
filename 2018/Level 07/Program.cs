string[] input = File.ReadAllLines("input.txt");

IEnumerable<(string PreviousStep, string NextStep)> tmp = from line in input
                                                          let parts = line.Split(new string[] { "Step ", " must be finished before step ", " can begin." }, StringSplitOptions.RemoveEmptyEntries)
                                                          select (parts[0], parts[1]);

PartOne(tmp.ToList());

static void PartOne(List<(string PreviousStep, string NextStep)> input)
{
    string correctOrder = string.Empty;



    Console.WriteLine(correctOrder);
}