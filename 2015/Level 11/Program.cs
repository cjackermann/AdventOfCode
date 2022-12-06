var oldPassword = "vzbxxyzz".ToArray();

var newPassword = DoCalculation(oldPassword, false);
newPassword = DoCalculation(newPassword, true);

Console.WriteLine(newPassword);

static char[] DoCalculation(char[] password, bool skipRequirements)
{
    while (true)
    {
        if (CheckRequirements(password) && !skipRequirements)
        {
            break;
        }

        skipRequirements = false;

        for (int i = password.Length - 1; i >= 0; i--)
        {
            if (password[i] == 'z')
            {
                password[i] = 'a';
            }
            else
            {
                password[i]++;
                break;
            }
        }
    }

    return password;
}

static bool CheckRequirements(char[] password)
{
    bool successRequirement1 = false;
    for (int i = 0; i < password.Length - 2; i++) // first requirement: 3 straight charakters
    {
        if ((password[i + 1] == password[i] + 1) && (password[i + 2] == password[i] + 2))
        {
            successRequirement1 = true;
            break;
        }
    }

    if (!successRequirement1)
    {
        return false;
    }

    if (password.Contains('i') || password.Contains('o') || password.Contains('l')) // second requirement: no "i", "o", "l"
    {
        return false;
    }

    var pairs = new List<char>();
    for (int i = 1; i < password.Length; i++)
    {
        if (password[i - 1] == password[i])
        {
            pairs.Add(password[i]);
        }
    }

    if (pairs.Distinct().Count() < 2) // third requirement: double letters
    {
        return false;
    }

    return true;
}