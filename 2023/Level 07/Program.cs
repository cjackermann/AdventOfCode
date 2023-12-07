string[] input = File.ReadAllLines("input.txt");

Part1(input);
Part2(input);

Console.ReadKey();

void Part1(string[] input)
{
    var hands = new List<Hand>();
    foreach (string line in input)
    {
        var parts = line.Split(" ");
        hands.Add(new Hand(parts[0].Select(x => GetSortOrder(x, true)).ToList(), GetType(parts[0]), long.Parse(parts[1])));
    }

    var orderd = hands.OrderBy(x => x.Type).ThenBy(x => x.Order2[0]).ThenBy(x => x.Order2[1]).ThenBy(x => x.Order2[2]).ThenBy(x => x.Order2[3]).ThenBy(x => x.Order2[4]).ToList();
    long res = 0;
    foreach (var card in orderd)
    {
        res += (orderd.IndexOf(card) + 1) * card.Multiplier;
    }

    Console.WriteLine("Part 1: " + res);
}

void Part2(string[] input)
{
    var hands = new List<Hand>();
    foreach (string line in input)
    {
        var parts = line.Split(" ");

        var newHand = new Hand(parts[0].Select(x => GetSortOrder(x, false)).ToList(), Type.HighCard, long.Parse(parts[1]));

        Type type = Type.HighCard;
        for (int i = 2; i < 14; i++)
        {
            string left = parts[0];
            if (i < 10)
            {
                left = left.Replace("J", i.ToString());
            }
            else if (i == 10)
            {
                left = left.Replace("J", "T");
            }
            else if (i == 11)
            {
                left = left.Replace("J", "Q");
            }
            else if (i == 12)
            {
                left = left.Replace("J", "K");
            }
            else if (i == 13)
            {
                left = left.Replace("J", "A");
            }

            var tmpType = GetType(left);
            if ((int)tmpType > (int)type)
            {
                type = tmpType;
            }
        }

        newHand = newHand with { Type = type };

        hands.Add(newHand);
    }

    var orderd = hands.OrderBy(x => x.Type).ThenBy(x => x.Order2[0]).ThenBy(x => x.Order2[1]).ThenBy(x => x.Order2[2]).ThenBy(x => x.Order2[3]).ThenBy(x => x.Order2[4]).ToList();
    long res = 0;
    foreach (var card in orderd)
    {
        res += (orderd.IndexOf(card) + 1) * card.Multiplier;
    }

    Console.WriteLine("Part 2: " + res);
}

static Type GetType(string v)
{
    var differentCards = v.GroupBy(x => x);

    if (differentCards.Count() == 1)
    {
        return Type.FiveOfKind;
    }
    else if (differentCards.Count() == 2 && differentCards.Any(x => x.Count() == 4))
    {
        return Type.FourOfKind;
    }
    else if (differentCards.Count() == 2)
    {
        return Type.FullHouse;
    }
    else if (differentCards.Count() == 3 && differentCards.Any(x => x.Count() == 3))
    {
        return Type.ThreeOfKind;
    }
    else if (differentCards.Count() == 3 && differentCards.Count(x => x.Count() == 2) == 2)
    {
        return Type.TwoPair;
    }
    else if (differentCards.Count() == 4)
    {
        return Type.OnePair;
    }

    return Type.HighCard;
}

static long GetSortOrder(char c, bool part1)
{
    if (c == 'A')
    {
        return 14;
    }
    else if (c == 'K')
    {
        return 13;
    }
    else if (c == 'Q')
    {
        return 12;
    }
    else if (c == 'J')
    {
        return part1 ? 11 : 1;
    }
    else if (c == 'T')
    {
        return 10;
    }
    else
    {
        return long.Parse(c.ToString());
    }
}

enum Type
{
    HighCard,
    OnePair,
    TwoPair,
    ThreeOfKind,
    FullHouse,
    FourOfKind,
    FiveOfKind,
}

record Hand(List<long> Order2, Type Type, long Multiplier);