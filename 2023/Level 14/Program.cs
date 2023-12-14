using System.Drawing;

string[] input = File.ReadAllLines("input.txt");

var board = new Dictionary<Point, char>(
                from y in Enumerable.Range(0, input.Length)
                from x in Enumerable.Range(0, input[0].Length)
                select new KeyValuePair<Point, char>(new Point(x, y), input[y][x]));

RollNorth(input);

var counter = 0;
for (int y = 0; y < input.Length; y++)
{
    for (int x = 0; x < input[0].Length; x++)
    {
        if (board[new Point(x, y)] == 'O')
        {
            counter += input.Length - y;
        }
    }
}

Console.WriteLine("Part 1: " + counter);


Dictionary<int, string> dict = new Dictionary<int, string>();

string tmpBoardText = string.Empty;
for (int y = 0; y < input.Length; y++)
{
    for (int x = 0; x < input[0].Length; x++)
    {
        tmpBoardText += board[new Point(x, y)];
    }
}

dict.Add(0, tmpBoardText);

int? startKey = null;
int currentKey = 0;
for (int i = 1; i <= 1000000000; i++)
{
    if (startKey != null && currentKey < dict.Count)
    {
        currentKey++;
        continue;
    }
    else if (startKey != null)
    {
        currentKey = startKey.Value + 1;
        continue;
    }

    RollNorth(input);
    RollWest(input);
    RollSouth(input);
    RollEast(input);

    tmpBoardText = string.Empty;
    for (int y = 0; y < input.Length; y++)
    {
        for (int x = 0; x < input[0].Length; x++)
        {
            tmpBoardText += board[new Point(x, y)];
        }
    }

    if (dict.Any(x => x.Value == tmpBoardText))
    {
        startKey = dict.First(x => x.Value == tmpBoardText).Key;
        currentKey = startKey.Value;
        continue;
    }

    dict.Add(i, tmpBoardText);
}

var resultBoard = dict[currentKey].Chunk(input[0].Length).ToList();
counter = 0;
for (int y = 0; y < input.Length; y++)
{
    for (int x = 0; x < input[0].Length; x++)
    {
        if (resultBoard[y][x] == 'O')
        {
            counter += input.Length - y;
        }
    }
}

Console.WriteLine("Part 2: " + counter);
Console.ReadKey();

void RollNorth(string[] input)
{
    for (int y = 0; y < input.Length; y++)
    {
        for (int x = 0; x < input[0].Length; x++)
        {
            if (board[new Point(x, y)] == 'O')
            {
                for (int tmpY = y; tmpY > 0; tmpY--)
                {
                    var tmpPoint = new Point(x, tmpY - 1);
                    if (board[tmpPoint] == '.')
                    {
                        board[tmpPoint] = 'O';
                        board[new Point(x, tmpY != input.Length ? tmpY : 0)] = '.';
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}

void RollWest(string[] input)
{
    for (int y = 0; y < input.Length; y++)
    {
        for (int x = 0; x < input[0].Length; x++)
        {
            if (board[new Point(x, y)] == 'O')
            {
                for (int tmpX = x; tmpX > 0; tmpX--)
                {
                    var tmpPoint = new Point(tmpX - 1, y);
                    if (board[tmpPoint] == '.')
                    {
                        board[tmpPoint] = 'O';
                        board[new Point(tmpX != 0 ? tmpX : 0, y)] = '.';
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}

void RollSouth(string[] input)
{
    for (int y = input.Length - 1; y >= 0; y--)
    {
        for (int x = 0; x < input[0].Length; x++)
        {
            if (board[new Point(x, y)] == 'O')
            {
                for (int tmpY = y; tmpY < input.Length - 1; tmpY++)
                {
                    var tmpPoint = new Point(x, tmpY + 1);
                    if (board[tmpPoint] == '.')
                    {
                        board[tmpPoint] = 'O';
                        board[new Point(x, tmpY != 0 ? tmpY : 0)] = '.';
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}

void RollEast(string[] input)
{
    for (int y = 0; y < input.Length; y++)
    {
        for (int x = input[0].Length - 1; x >= 0; x--)
        {
            if (board[new Point(x, y)] == 'O')
            {
                for (int tmpX = x; tmpX < input[0].Length - 1; tmpX++)
                {
                    var tmpPoint = new Point(tmpX + 1, y);
                    if (board[tmpPoint] == '.')
                    {
                        board[tmpPoint] = 'O';
                        board[new Point(tmpX != input[0].Length ? tmpX : 0, y)] = '.';
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}