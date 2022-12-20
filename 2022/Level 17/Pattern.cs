namespace Level_17
{
    public class Pattern
    {
        static readonly IEqualityComparer<HashSet<(long, long)>> setComparer = HashSet<(long, long)>.CreateSetComparer();

        public Pattern(int airIndex, HashSet<(long, long)> stones)
        {
            AirIndex = airIndex;
            Stones = stones;
        }

        public int AirIndex { get; }

        public HashSet<(long, long)> Stones { get; }

        public override bool Equals(object obj) => obj is Pattern other && other != null && AirIndex == other.AirIndex && setComparer.Equals(Stones, other.Stones);

        public override int GetHashCode() => (27 * 13 + AirIndex.GetHashCode()) + (13 * 27 + setComparer.GetHashCode(Stones));
    }
}