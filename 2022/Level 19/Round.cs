namespace Level_19
{
    public class Round
    {
        public Round(int oreCount, int oreRobotsCount, int clayCount, int clayRobotsCount, int obsidianCount, int obsidianRobotsCount, int geodeCount, int geodeRobotsCount, int time)
        {
            OreCount = oreCount;
            OreRobotsCount = oreRobotsCount;
            ClayCount = clayCount;
            ClayRobotsCount = clayRobotsCount;
            ObsidianCount = obsidianCount;
            ObsidianRobotsCount = obsidianRobotsCount;
            GeodeCount = geodeCount;
            GeodeRobotsCount = geodeRobotsCount;
            Time = time;
        }

        public int OreCount { get; }

        public int OreRobotsCount { get; }

        public int ClayCount { get; }

        public int ClayRobotsCount { get; }

        public int ObsidianCount { get; }

        public int ObsidianRobotsCount { get; }

        public int GeodeCount { get; }

        public int GeodeRobotsCount { get; }

        public int Time { get; }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 187;
                hash = (187 * hash) + OreCount;
                hash = (187 * hash) + OreRobotsCount;
                hash = (187 * hash) + ClayCount;
                hash = (187 * hash) + ClayRobotsCount;
                hash = (187 * hash) + ObsidianCount;
                hash = (187 * hash) + ObsidianRobotsCount;
                hash = (187 * hash) + GeodeCount;
                hash = (187 * hash) + GeodeRobotsCount;
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is not Round other)
            {
                return false;
            }

            return OreCount == other.OreCount && OreRobotsCount == other.OreRobotsCount && ClayCount == other.ClayCount && ClayRobotsCount == other.ClayRobotsCount && ObsidianCount == other.ObsidianCount && ObsidianRobotsCount == other.ObsidianRobotsCount && GeodeCount == other.GeodeCount && GeodeRobotsCount == other.GeodeRobotsCount && Time == other.Time;
        }
    }
}