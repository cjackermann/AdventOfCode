namespace Level_17
{
    public record MapPoint(int X, int Y)
    {
        public IEnumerable<(MapPoint Point, Direction Direction, int Steps)> GetNeighbours(Direction direction, int steps)
        {
            if (direction != Direction.South && (direction != Direction.North || direction == Direction.North && steps < 3))
            {
                yield return (North, Direction.North, direction == Direction.North ? steps + 1 : 0);
            }

            if (direction != Direction.West && (direction != Direction.East || direction == Direction.East && steps < 3))
            {
                yield return (East, Direction.East, direction == Direction.East ? steps + 1 : 0);
            }

            if (direction != Direction.North && (direction != Direction.South || direction == Direction.South && steps < 3))
            {
                yield return (South, Direction.South, direction == Direction.South ? steps + 1 : 0);
            }

            if (direction != Direction.East && (direction != Direction.West || direction == Direction.West && steps < 3))
            {
                yield return (West, Direction.West, direction == Direction.West ? steps + 1 : 0);
            }
        }

        public MapPoint North => new MapPoint(X, Y - 1);

        public MapPoint East => new MapPoint(X + 1, Y);

        public MapPoint South => new MapPoint(X, Y + 1);

        public MapPoint West => new MapPoint(X - 1, Y);
    }
}
