namespace Level_24
{
    public record Hailstone(PositionXYZ Position, PositionXYZ Velocity, double Slope = 0, double Intersect = 0)
    {
        public Hailstone Move() => this with { Position = Position.CalculateNext(Velocity.X, Velocity.Y, Velocity.Z) };
    }
}
