long[] input = File.ReadAllLines("input.txt").Select(long.Parse).ToArray();

var originalList = input.Select(d => new LinkedListNode<long>(d)).ToList();
LinkedList<long> editedList = new();
foreach (var item in originalList)
{
    editedList.AddLast(item);
}

ReorderList(originalList, editedList);

long result = CalculateResult(originalList, editedList);
Console.WriteLine("Stage 1: " + result);

originalList.ForEach(d => d.Value *= 811589153);
editedList.Clear();
foreach (var item in originalList)
{
    editedList.AddLast(item);
}

for (int i = 0; i < 10; i++)
{
    ReorderList(originalList, editedList);
}

result = CalculateResult(originalList, editedList);
Console.WriteLine("Stage 2: " + result);
Console.ReadKey();

static long CalculateResult(List<LinkedListNode<long>> originalList, LinkedList<long> editedList)
{
    long result = 0;
    var targetZero = originalList.FirstOrDefault(n => n.Value == 0);

    for (int i = 1; i <= 3; i++)
    {
        var moveCount = 1000 % editedList.Count;
        while (moveCount > 0)
        {
            targetZero = targetZero.Next ?? editedList.First;
            moveCount--;
        }

        result += targetZero.Value;
    }

    return result;
}

static void ReorderList(List<LinkedListNode<long>> originalList, LinkedList<long> editedList)
{
    foreach (var item in originalList)
    {
        var moveCount = item.Value % (originalList.Count - 1);

        if (moveCount > 0)
        {
            var after = item.Next ?? editedList.First;
            editedList.Remove(item);

            while (moveCount > 0)
            {
                after = after.Next ?? editedList.First;
                moveCount--;
            }

            editedList.AddBefore(after, item);
        }
        else if (moveCount < 0)
        {
            var before = item.Previous ?? editedList.Last;
            editedList.Remove(item);

            while (moveCount < 0)
            {
                before = before.Previous ?? editedList.Last;
                moveCount++;
            }

            editedList.AddAfter(before, item);
        }
    }
}