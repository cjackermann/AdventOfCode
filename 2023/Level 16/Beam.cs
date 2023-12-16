using System.Drawing;

namespace Level_16
{
    public class Beam
    {
        public Beam(int x, int y, Direction direction)
        {
            CurrentPosition = new Point { X = x, Y = y };
            CurrentDirection = direction;
        }

        public Point CurrentPosition { get; set; }

        public Direction CurrentDirection { get; set; }

        public void ChangePosition(Direction direction)
        {
            CurrentDirection = direction;

            if (direction == Direction.Right)
            {
                CurrentPosition = new Point(CurrentPosition.X + 1, CurrentPosition.Y);
            }
            else if (direction == Direction.Left)
            {
                CurrentPosition = new Point(CurrentPosition.X - 1, CurrentPosition.Y);
            }
            else if (direction == Direction.Up)
            {
                CurrentPosition = new Point(CurrentPosition.X, CurrentPosition.Y - 1);
            }
            else if (direction == Direction.Down)
            {
                CurrentPosition = new Point(CurrentPosition.X, CurrentPosition.Y + 1);
            }
        }

        public bool CheckPosition(int maxX, int maxY) => CurrentPosition.X >= 0 && CurrentPosition.X <= maxX && CurrentPosition.Y >= 0 && CurrentPosition.Y <= maxY;
    }
}
