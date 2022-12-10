var input = File.ReadAllText("input.txt").Split("\r\n").Select(int.Parse).ToArray();
var _containers = Enumerable.Range(0, input.Length).Select(d => new Container(d, input[d])).ToArray();

List<List<Container>> combinations = new();
foreach (var container in _containers)
{
    combinations.AddRange(GetContainers(container.Index, 150));
}

var distinctCombinations = combinations.GroupBy(d => string.Join(" - ", d.Select(x => x.Index))).ToList();
var minimumCombinationsCount = distinctCombinations.Select(d => d.Key.Split(" - ").Length).Min();
var minimumCombinations = distinctCombinations.Select(d => d.Key.Split(" - ").Length).Where(d => d == minimumCombinationsCount).ToList();

Console.WriteLine(distinctCombinations.Count);
Console.WriteLine(minimumCombinations.Count);

IEnumerable<List<Container>> GetContainers(int index, int remainingLiters)
{
    if (index == _containers.Length)
    {
        yield break;
    }
    else
    {
        if (remainingLiters == _containers[index].Liters)
        {
            yield return new List<Container> { _containers[index] };
        }
        if (remainingLiters >= _containers[index].Liters)
        {
            foreach (var v in GetContainers(index + 1, remainingLiters - _containers[index].Liters))
            {
                v.Add(_containers[index]);
                yield return v;
            }
        }
        foreach (var v in GetContainers(index + 1, remainingLiters))
        {
            yield return v;

        }
    }
}

record Container(int Index, int Liters);
