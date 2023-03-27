using System.Security.Cryptography;
using System.Text;

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
}