using System.Drawing;

namespace Level_04
{
    public static class Helper
    {
        public static void CheckRight(Point currPoint, Dictionary<Point, char> board, HashSet<Word> points)
        {
            try
            {
                if (board[currPoint with { X = currPoint.X + 1 }] == 'M')
                {
                    if (board[currPoint with { X = currPoint.X + 2 }] == 'A')
                    {
                        if (board[currPoint with { X = currPoint.X + 3 }] == 'S')
                        {
                            points.Add(new Word(currPoint, new Point(currPoint.X + 3, currPoint.Y)));
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public static void CheckLeft(Point currPoint, Dictionary<Point, char> board, HashSet<Word> points)
        {
            try
            {
                if (board[currPoint with { X = currPoint.X - 1 }] == 'M')
                {
                    if (board[currPoint with { X = currPoint.X - 2 }] == 'A')
                    {
                        if (board[currPoint with { X = currPoint.X - 3 }] == 'S')
                        {
                            points.Add(new Word(currPoint, new Point(currPoint.X - 3, currPoint.Y)));
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public static void CheckUp(Point currPoint, Dictionary<Point, char> board, HashSet<Word> points)
        {
            try
            {
                if (board[currPoint with { Y = currPoint.Y + 1 }] == 'M')
                {
                    if (board[currPoint with { Y = currPoint.Y + 2 }] == 'A')
                    {
                        if (board[currPoint with { Y = currPoint.Y + 3 }] == 'S')
                        {
                            points.Add(new Word(currPoint, new Point(currPoint.X, currPoint.Y + 3)));
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public static void CheckDown(Point currPoint, Dictionary<Point, char> board, HashSet<Word> points)
        {
            try
            {
                if (board[currPoint with { Y = currPoint.Y - 1 }] == 'M')
                {
                    if (board[currPoint with { Y = currPoint.Y - 2 }] == 'A')
                    {
                        if (board[currPoint with { Y = currPoint.Y - 3 }] == 'S')
                        {
                            points.Add(new Word(currPoint, new Point(currPoint.X, currPoint.Y - 3)));
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public static void CheckRightUp(Point currPoint, Dictionary<Point, char> board, HashSet<Word> points)
        {
            try
            {
                if (board[currPoint with { X = currPoint.X + 1, Y = currPoint.Y + 1 }] == 'M')
                {
                    if (board[currPoint with { X = currPoint.X + 2, Y = currPoint.Y + 2 }] == 'A')
                    {
                        if (board[currPoint with { X = currPoint.X + 3, Y = currPoint.Y + 3 }] == 'S')
                        {
                            points.Add(new Word(currPoint, new Point(currPoint.X + 3, currPoint.Y + 3)));
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public static void CheckRightDown(Point currPoint, Dictionary<Point, char> board, HashSet<Word> points)
        {
            try
            {
                if (board[currPoint with { X = currPoint.X + 1, Y = currPoint.Y - 1 }] == 'M')
                {
                    if (board[currPoint with { X = currPoint.X + 2, Y = currPoint.Y - 2 }] == 'A')
                    {
                        if (board[currPoint with { X = currPoint.X + 3, Y = currPoint.Y - 3 }] == 'S')
                        {
                            points.Add(new Word(currPoint, new Point(currPoint.X + 3, currPoint.Y - 3)));
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public static void CheckLeftUp(Point currPoint, Dictionary<Point, char> board, HashSet<Word> points)
        {
            try
            {
                if (board[currPoint with { X = currPoint.X - 1, Y = currPoint.Y + 1 }] == 'M')
                {
                    if (board[currPoint with { X = currPoint.X - 2, Y = currPoint.Y + 2 }] == 'A')
                    {
                        if (board[currPoint with { X = currPoint.X - 3, Y = currPoint.Y + 3 }] == 'S')
                        {
                            points.Add(new Word(currPoint, new Point(currPoint.X - 3, currPoint.Y + 3)));
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public static void CheckLeftDown(Point currPoint, Dictionary<Point, char> board, HashSet<Word> points)
        {
            try
            {
                if (board[currPoint with { X = currPoint.X - 1, Y = currPoint.Y - 1 }] == 'M')
                {
                    if (board[currPoint with { X = currPoint.X - 2, Y = currPoint.Y - 2 }] == 'A')
                    {
                        if (board[currPoint with { X = currPoint.X - 3, Y = currPoint.Y - 3 }] == 'S')
                        {
                            points.Add(new Word(currPoint, new Point(currPoint.X - 3, currPoint.Y - 3)));
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public static bool CheckXMAS(Point currPoint, Dictionary<Point, char> board)
        {
            try
            {
                var rightUp = board[currPoint with { X = currPoint.X + 1, Y = currPoint.Y + 1 }];
                var rightDown = board[currPoint with { X = currPoint.X + 1, Y = currPoint.Y - 1 }];
                var leftUp = board[currPoint with { X = currPoint.X - 1, Y = currPoint.Y + 1 }];
                var leftDown = board[currPoint with { X = currPoint.X - 1, Y = currPoint.Y - 1 }];

                return ((rightUp == 'M' && leftDown == 'S') || (rightUp == 'S' && leftDown == 'M')) && ((leftUp == 'M' && rightDown == 'S') || (leftUp == 'S' && rightDown == 'M'));
            }
            catch
            {
                return false;
            }
        }
    }
}