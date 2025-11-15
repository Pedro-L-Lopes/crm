using CRM.Domain.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace CRM.Infrastructure.Security.Cryptography;

public class BCryptNet : IPasswordEncripter
{
    public readonly string _addiotionalKey;

    public BCryptNet(string addiotionalKey)
    {
        _addiotionalKey = addiotionalKey;
    }

    public string Encrypt(string password)
    {
        var newPassword = $"{password}{_addiotionalKey}";

        var bytes = Encoding.UTF8.GetBytes(newPassword);
        var hashBytes = SHA512.HashData(bytes);

        return StringBytes(hashBytes);
    }

    private static string StringBytes(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (byte b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }

        return sb.ToString();
    }
}
