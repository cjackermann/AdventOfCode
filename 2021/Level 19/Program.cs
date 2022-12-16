using Level_19;

string[] input = File.ReadAllText("input.txt").Split("\r\n\r\n");

var scanners = from line in input
               let parts = line.Split("\r\n")
               let scanner = int.Parse(parts[0].Split(new string[] { "--- scanner ", " ---" }, StringSplitOptions.RemoveEmptyEntries).First())
               let beacons = parts.Skip(1).Select(d => d.Split(',')).Select(d => (int.Parse(d[0]), int.Parse(d[1]), int.Parse(d[2]))).ToList()
               select new Scanner(scanner, beacons);

scanners.ToList().ForEach(d => d.UpdateBeacons());

foreach (var scanner1 in scanners)
{
    foreach (var scanner2 in scanners)
    {
        if (scanner1 == scanner2)
        {
            return;
        }

        HashSet<(Beacon, Beacon)> overlappingBeacons = new();
        foreach (var beaconScanner1 in scanner1.Beacons)
        {
            var distance1 = beaconScanner1.OtherBeacons.Select(d => d.Value.LengthSquared()).ToList();
            foreach (var beaconScanner2 in scanner2.Beacons)
            {
                var distance2 = beaconScanner2.OtherBeacons.Select(d => d.Value.LengthSquared()).ToList();

                var intersect = distance1.Intersect(distance2).Count();
                if (intersect >= 11)
                {
                    overlappingBeacons.Add((beaconScanner1, beaconScanner2));
                    break;
                }
            }
        }

        if (overlappingBeacons.Count >= 11)
        {
            if (!scanner1.OverlappingScanners.ContainsKey(scanner2))
            {
                scanner1.OverlappingScanners.Add(scanner2, overlappingBeacons);
            }
        }
    }
}

var entries = scanners.SelectMany(d => d.Beacons).Distinct().ToList();
Console.WriteLine();
Console.ReadKey();
