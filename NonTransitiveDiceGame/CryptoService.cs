using System.Security.Cryptography;
using System.Text;

namespace NonTransitiveDiceGame;

public class CryptoService
{
    private const int KeySizeInBytes = 32; 
    public string GenerateKey()
    {
        // RandomNumberGenerator is the modern API for cryptographic randomness.
        var keyBytes = RandomNumberGenerator.GetBytes(KeySizeInBytes);
        return Convert.ToHexString(keyBytes);
    }

    public int GenerateSecureRandomNumber(int maxValue)
    {
        // This method provides a uniform distribution, avoiding the bias of the modulo operator.
        return RandomNumberGenerator.GetInt32(maxValue);
    }

    public string CalculateHmac(string keyHex, string message)
    {
        var keyBytes = Convert.FromHexString(keyHex);
        var messageBytes = Encoding.UTF8.GetBytes(message);

        using var hmac = new HMACSHA256(keyBytes);
        var hashBytes = hmac.ComputeHash(messageBytes);

        return Convert.ToHexString(hashBytes);
    }
}