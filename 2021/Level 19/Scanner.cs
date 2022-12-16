using System.Numerics;

namespace Level_19
{
    public class Scanner
    {
        public Scanner(int index, List<(int X, int Y, int Z)> beacons)
        {
            Index = index;
            beacons.ForEach(d => Beacons.Add(new Beacon(d.X, d.Y, d.Z, this)));
        }

        public int Index { get; }

        public Vector3 OriginalLocation { get; }

        public Vector3 NormalizedLocation { get; }

        public List<Beacon> Beacons { get; } = new List<Beacon>();

        public Dictionary<Scanner, HashSet<(Beacon, Beacon)>> OverlappingScanners { get; set; } = new();

        public void UpdateBeacons()
        {
            foreach (var beacon1 in Beacons)
            {
                foreach (var beacon2 in Beacons)
                {
                    if (beacon1 != beacon2)
                    {
                        beacon1.AddOtherBeacon(beacon2);
                    }
                }
            }
        }
    }
}
