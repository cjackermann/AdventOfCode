string[] input = File.ReadAllLines("input.txt");

List<Tree> trees = GetTrees(input);

for (int y = 0; y < input.Length; y++)
{
    for (int x = 0; x < input[0].Length; x++)
    {
        var currentTree = trees.FirstOrDefault(d => d.X == x && d.Y == y);

        var leftTrees = trees.Where(d => d != currentTree && d.Y == y && d.X < x).OrderByDescending(d => d.X).Select(d => d.Height).ToList();
        var rightTrees = trees.Where(d => d != currentTree && d.Y == y && d.X > x).OrderBy(d => d.X).Select(d => d.Height).ToList();
        var upTrees = trees.Where(d => d != currentTree && d.Y < y && d.X == x).OrderByDescending(d => d.Y).Select(d => d.Height).ToList();
        var downTrees = trees.Where(d => d != currentTree && d.Y > y && d.X == x).OrderBy(d => d.Y).Select(d => d.Height).ToList();

        PartOne(currentTree, leftTrees, rightTrees, upTrees, downTrees);
        PartTwo(currentTree, leftTrees, rightTrees, upTrees, downTrees);
    }
}

var visibleTrees = trees.Where(d => d.HasGoodView);
Console.WriteLine(visibleTrees.Count());

var highestTree = trees.OrderByDescending(d => d.Score).FirstOrDefault();
Console.WriteLine(highestTree.Score);

static void PartOne(Tree tree, List<int> leftTrees, List<int> rightTrees, List<int> upTrees, List<int> downTrees)
{
    if (!leftTrees.Any() || leftTrees.All(d => d < tree.Height))
    {
        tree.HasGoodView = true;
    }
    else if (!rightTrees.Any() || rightTrees.All(d => d < tree.Height))
    {
        tree.HasGoodView = true;
    }
    else if (!upTrees.Any() || upTrees.All(d => d < tree.Height))
    {
        tree.HasGoodView = true;
    }
    else if (!downTrees.Any() || downTrees.All(d => d < tree.Height))
    {
        tree.HasGoodView = true;
    }
}

static void PartTwo(Tree tree, List<int> leftTrees, List<int> rightTrees, List<int> upTrees, List<int> downTrees)
{
    int leftScore = 0;
    foreach (var leftTree in leftTrees)
    {
        if (leftTree >= tree.Height)
        {
            leftScore++;
            break;
        }

        leftScore++;
    }

    int rightScore = 0;
    foreach (var rightTree in rightTrees)
    {
        if (rightTree >= tree.Height)
        {
            rightScore++;
            break;
        }

        rightScore++;
    }

    int upScore = 0;
    foreach (var upTree in upTrees)
    {
        if (upTree >= tree.Height)
        {
            upScore++;
            break;
        }

        upScore++;
    }

    int downScore = 0;
    foreach (var downTree in downTrees)
    {
        if (downTree >= tree.Height)
        {
            downScore++;
            break;
        }

        downScore++;
    }

    tree.Score = leftScore * rightScore * upScore * downScore;
}

static List<Tree> GetTrees(string[] input)
{
    List<Tree> allPoints = new();
    for (int y = 0; y < input.Length; y++)
    {
        for (int x = 0; x < input[0].Length; x++)
        {
            allPoints.Add(new Tree(x, y, int.Parse(input[y][x].ToString())));
        }
    }

    return allPoints;
}

class Tree
{
    public Tree(int x, int y, int height)
    {
        X = x;
        Y = y;
        Height = height;
    }

    public int X { get; }

    public int Y { get; }

    public int Height { get; }

    public bool HasGoodView { get; set; } // needed in part one

    public int Score { get; set; } // needed in part two
}