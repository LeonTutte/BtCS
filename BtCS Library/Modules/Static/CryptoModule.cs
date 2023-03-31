using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Security;

namespace BtCS_Library.Modules.Static;

public static class CryptoModule
{
    public static string CreateSHA512(string strData)
    {
        var message = Encoding.UTF8.GetBytes(strData);
        using var alg = SHA512.Create();

        var hashValue = alg.ComputeHash(message);
        return hashValue.Aggregate("", (current, x) => current + $"{x:x2}");
    }

    public static string CreateStoragePassword()
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < 4; i++)
        {
            builder.Append(RandomString(6, true));
            builder.Append(new  SecureRandom().Next(1000, 9999));
            builder.Append(RandomString(2, false));
        }
        return builder.ToString();
    }
    
    // Generate a random string with a given size and case.
    // If second parameter is true, the return string is lowercase
    private static  string RandomString(int size, bool lowerCase)
    {
        StringBuilder builder = new StringBuilder();
        Random random = new SecureRandom();
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }
        if (lowerCase) return builder.ToString().ToLower();
        return builder.ToString();
    }
}