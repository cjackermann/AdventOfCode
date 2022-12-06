using System.Security.Cryptography;
using System.Text;

string input = "iwrupvqb";

CalculateNumber(input, "00000");
CalculateNumber(input, "000000", 700000);

static void CalculateNumber(string input, string start, int skip = 0)
{
    int number = 1 + skip;
    while (true)
    {
        string password = input + number;
        byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
        byte[] hash = (CryptoConfig.CreateFromName("MD5") as HashAlgorithm).ComputeHash(encodedPassword);
        string encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();

        if (encoded.StartsWith(start))
        {
            break;
        }

        number++;
    }

    Console.WriteLine(number);
}