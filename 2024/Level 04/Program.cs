using System.Drawing;
using Level_04;

string[] input = File.ReadAllLines("input.txt");

var board = new Dictionary<Point, char>(
                from y in Enumerable.Range(0, input.Length)
                from x in Enumerable.Range(0, input[0].Length)
                select new KeyValuePair<Point, char>(new Point(x, y), input[y][x]));

Part1(input, board);
Part2(input, board);

Console.ReadKey();

static void Part1(string[] input, Dictionary<Point, char> board)
{
    var points = new HashSet<Word>();

    for (int y = 0; y < input.Length; y++)
    {
        for (int x = 0; x < input[0].Length; x++)
        {
            var currPoint = new Point(x, y);

            if (board[currPoint] == 'X')
            {
                Helper.CheckRight(currPoint, board, points);
                Helper.CheckLeft(currPoint, board, points);
                Helper.CheckUp(currPoint, board, points);
                Helper.CheckDown(currPoint, board, points);
                Helper.CheckRightDown(currPoint, board, points);
                Helper.CheckRightUp(currPoint, board, points);
                Helper.CheckLeftDown(currPoint, board, points);
                Helper.CheckLeftUp(currPoint, board, points);
            }
        }
    }

    Console.WriteLine("Part1: " + points.Count);
}

static void Part2(string[] input, Dictionary<Point, char> board)
{
    long result = 0;

    for (int y = 0; y < input.Length; y++)
    {
        for (int x = 0; x < input[0].Length; x++)
        {
            var currPoint = new Point(x, y);

            if (board[currPoint] == 'A' && Helper.CheckXMAS(currPoint, board))
            {
                result++;
            }
        }
    }

    Console.WriteLine("Part2: " + result);
}