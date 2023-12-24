namespace Level_24
{
    public record PositionXYZ(double X, double Y, double Z = 0)
    {
        public PositionXYZ CalculateNext(double x, double y, double z) => new PositionXYZ(X + x, Y + y, Z + z);

        public bool CheckBounds(long min, long max) => X >= min && X <= max && Y >= min && Y <= max;
    }
}
