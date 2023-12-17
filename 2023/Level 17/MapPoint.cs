namespace Level_17
{
    public record MapPoint(int X, int Y)
    {
        public IEnumerable<(MapPoint Point, Direction Direction, int Steps)> GetPart1Neighbours(Direction direction, int steps)
        {
            if (steps < 3)
            {
                switch (direction)
                {
                    case Direction.West: yield return (West, Direction.West, steps + 1); break;
                    case Direction.South: yield return (South, Direction.South, steps + 1); break;
                    case Direction.East: yield return (East, Direction.East, steps + 1); break;
                    case Direction.North: yield return (North, Direction.North, steps + 1); break;
                }
            }

            if (direction == Direction.West || direction == Direction.East)
            {
                yield return (North, Direction.North, 1);
                yield return (South, Direction.South, 1);
            }
            else if (direction == Direction.North || direction == Direction.South)
            {
                yield return (West, Direction.West, 1);
                yield return (East, Direction.East, 1);
            }
        }

        public IEnumerable<(MapPoint Point, Direction Direction, int Steps)> GetPart2Neighbours(Direction direction, int steps)
        {
            if (steps < 10)
            {
                switch (direction)
                {
                    case Direction.West: yield return (West, Direction.West, steps + 1); break;
                    case Direction.South: yield return (South, Direction.South, steps + 1); break;
                    case Direction.East: yield return (East, Direction.East, steps + 1); break;
                    case Direction.North: yield return (North, Direction.North, steps + 1); break;
                }
            }

            if (steps >= 4)
            {
                if (direction == Direction.West || direction == Direction.East)
                {
                    yield return (North, Direction.North, 1);
                    yield return (South, Direction.South, 1);
                }
                else if (direction == Direction.North || direction == Direction.South)
                {
                    yield return (West, Direction.West, 1);
                    yield return (East, Direction.East, 1);
                }
            }
        }

        public MapPoint North => new MapPoint(X, Y - 1);

        public MapPoint East => new MapPoint(X + 1, Y);

        public MapPoint South => new MapPoint(X, Y + 1);

        public MapPoint West => new MapPoint(X - 1, Y);
    }
}
