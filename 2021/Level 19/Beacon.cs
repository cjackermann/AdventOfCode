using System.Numerics;

namespace Level_19
{
    public class Beacon
    {
        public Beacon(int x, int y, int z, Scanner scanner)
        {
            OriginalLocation = new Vector3(x, y, z);
            ParentScanner = scanner;
        }

        public Vector3 OriginalLocation { get; }

        public Vector3 NormalizedLocation { get; set; }

        public Scanner ParentScanner { get; }

        public Dictionary<Beacon, Vector3> OtherBeacons { get; set; } = new();

        public void AddOtherBeacon(Beacon beacon)
        {
            if (!OtherBeacons.ContainsKey(beacon))
            {
                OtherBeacons.Add(beacon, OriginalLocation - beacon.OriginalLocation);
            }
        }

        public void SetNormalizedPosition()
        {
            var vectorFromProbe = new Vector3((int)Math.Round(OriginalLocation.X), (int)Math.Round(OriginalLocation.Y), (int)Math.Round(OriginalLocation.Z));

            NormalizedLocation = Vector3.Add(ParentScanner.OriginalLocation, vectorFromProbe);
        }
    }
}
