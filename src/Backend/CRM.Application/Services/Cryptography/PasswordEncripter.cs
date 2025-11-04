using System.Security.Cryptography;
using System.Text;

namespace CRM.Application.Services.Cryptography;
public class PasswordEncripter
{
    public readonly string _addiotionalKey;

    public PasswordEncripter(string addiotionalKey)
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
        foreach(byte b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }

        return sb.ToString();
    }
}
