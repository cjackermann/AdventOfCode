string[] input = File.ReadAllLines("input.txt");

var aunts = (from line in input
             let parts = line.Split(new string[] { ", ", "Sue ", " " }, StringSplitOptions.None)
             let count = int.Parse(parts[1].Replace(":", string.Empty))
             let pair1 = new Thing(parts[2].Replace(":", string.Empty), int.Parse(parts[3]))
             let pair2 = new Thing(parts[4].Replace(":", string.Empty), int.Parse(parts[5]))
             let pair3 = new Thing(parts[6].Replace(":", string.Empty), int.Parse(parts[7]))
             select new Aunt(count, pair1, pair2, pair3)).ToArray();

var possibleAunts = aunts.Where(d => Check(d.Thing1, d.Thing2, d.Thing3, "children", 3)).ToList();
possibleAunts = possibleAunts.Where(d => Check(d.Thing1, d.Thing2, d.Thing3, "cats", 7)).ToList();
possibleAunts = possibleAunts.Where(d => Check(d.Thing1, d.Thing2, d.Thing3, "samoyeds", 2)).ToList();
possibleAunts = possibleAunts.Where(d => Check(d.Thing1, d.Thing2, d.Thing3, "pomeranians", 3)).ToList();
possibleAunts = possibleAunts.Where(d => Check(d.Thing1, d.Thing2, d.Thing3, "akitas", 0)).ToList();
possibleAunts = possibleAunts.Where(d => Check(d.Thing1, d.Thing2, d.Thing3, "vizslas", 0)).ToList();
possibleAunts = possibleAunts.Where(d => Check(d.Thing1, d.Thing2, d.Thing3, "goldfish", 5)).ToList();
possibleAunts = possibleAunts.Where(d => Check(d.Thing1, d.Thing2, d.Thing3, "trees", 3)).ToList();
possibleAunts = possibleAunts.Where(d => Check(d.Thing1, d.Thing2, d.Thing3, "cars", 2)).ToList();
possibleAunts = possibleAunts.Where(d => Check(d.Thing1, d.Thing2, d.Thing3, "perfumes", 1)).ToList();

Console.WriteLine(possibleAunts.FirstOrDefault().Count);

static bool Check(Thing thing1, Thing thing2, Thing thing3, string key, int amount)
{
    if (thing1.Key != key && thing2.Key != key && thing3.Key != key)
    {
        return true;
    }
    else if (thing1.Key == key)
    {
        return CheckAmount(thing1.Amount, amount, key);
    }
    else if (thing2.Key == key)
    {
        return CheckAmount(thing2.Amount, amount, key);
    }
    else if (thing3.Key == key)
    {
        return CheckAmount(thing3.Amount, amount, key);
    }

    return false;
}

static bool CheckAmount(int thingAmount, int amount, string key)
{
    return key switch
    {
        "cats" or "trees" => thingAmount > amount,
        "pomeranians" or "goldfish" => thingAmount < amount,
        _ => thingAmount == amount,
    };
}

record Aunt(int Count, Thing Thing1, Thing Thing2, Thing Thing3);

record Thing(string Key, int Amount);