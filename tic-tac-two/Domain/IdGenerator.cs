using System.Security.Cryptography;
using System.Text;

namespace Domain;

public static class IdGenerator
{
    private static readonly char[] Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
        .ToCharArray();

    public static string GenerateId(int length = 8)
    {
        if (length <= 0)
        {
            throw new ArgumentException("Length must be greater than 0");
        }

        using var rng = RandomNumberGenerator.Create();
        var data = new byte[length];
        rng.GetBytes(data);

        var idBuilder = new StringBuilder(length);
        foreach (var byteValue in data)
        {
            idBuilder.Append(Chars[byteValue % Chars.Length]);
        }

        return idBuilder.ToString();
    }
}