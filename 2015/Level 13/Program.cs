string[] input = File.ReadAllLines("input.txt");

var sympathies = (from line in input
                  let blocks = line.Split(new string[] { " would ", " happiness units by sitting next to " }, StringSplitOptions.None)
                  let name = blocks[0]
                  let operation = blocks[1]
                  let partner = blocks[2].Replace(".", string.Empty)
                  select new Sympathy(name, operation.Split(' ')[0] == "gain" ? int.Parse(operation.Split(' ')[1]) : (0 - int.Parse(operation.Split(' ')[1])), partner)).ToList();

HashSet<string> persons = sympathies.Select(d => d.Name).ToHashSet();

// adding myself unasked for the party (part two)
foreach (var person in persons)
{
    sympathies.Add(new Sympathy(person, 0, "Mr. Punz"));
    sympathies.Add(new Sympathy("Mr. Punz", 0, person));
}

persons.Add("Mr. Punz");
// end adding myself unasked

var seatingArrangements = GetAllSeatingArrangements(persons).ToList();
seatingArrangements.ForEach(d => d.Add(d[0]));
var allSeatingHappinesses = GetOverallSeatingHappiness(seatingArrangements, sympathies);

Console.WriteLine(allSeatingHappinesses.Max());

static IEnumerable<List<string>> GetAllSeatingArrangements(IEnumerable<string> persons)
{
    foreach (var person in persons)
    {
        if (!persons.Where(c => c != person).Any())
        {
            yield return new List<string> { person };
        }
        else
        {
            var tmp = GetAllSeatingArrangements(persons.Where(c => c != person));

            foreach (var t in tmp)
            {
                t.Insert(0, person);
                yield return t;
            }
        }
    }
}

static IEnumerable<int> GetOverallSeatingHappiness(IEnumerable<List<string>> seatingArrangements, IEnumerable<Sympathy> sympathies)
{
    foreach (var seatingArrangement in seatingArrangements)
    {
        int result = 0;
        for (int i = 0; i < seatingArrangement.Count - 1; i++)
        {
            result += sympathies.Where(d => (d.Name == seatingArrangement[i] || d.Name == seatingArrangement[i + 1]) && (d.Partner == seatingArrangement[i] || d.Partner == seatingArrangement[i + 1])).Sum(d => d.Value);
        }

        yield return result;
    }
}

record Sympathy(string Name, int Value, string Partner);