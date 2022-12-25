string[] input = File.ReadAllLines("input.txt");

var dict = new Dictionary<char, long>
{
    { '=', -2 },
    { '-', -1 },
    { '0', 0 },
    { '1', 1 },
    { '2', 2 }
};

long result = ConvertToDecimal(input, dict);
string snafu = ConvertToSnafu(result, dict);

Console.WriteLine(snafu);

static long ConvertToDecimal(string[] input, Dictionary<char, long> dict)
{
    long result = 0;

    foreach (var line in input)
    {
        var reverse = line.Reverse();
        long tmpResult = 0;
        long helper = 1;

        foreach (var charakter in reverse)
        {
            tmpResult += helper * dict[charakter];
            helper *= 5;
        }

        result += tmpResult;
    }

    return result;
}

static string ConvertToSnafu(long result, Dictionary<char, long> dict)
{
    var reverseDict = dict.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

    string snafu = string.Empty;
    while (result > 0)
    {
        snafu = reverseDict[((result + 2) % 5) - 2] + snafu;
        result = (result + 2) / 5;
    }

    return snafu;
}