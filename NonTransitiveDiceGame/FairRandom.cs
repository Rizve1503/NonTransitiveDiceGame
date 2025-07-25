using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonTransitiveDiceGame;

public class FairRandom
{
    private readonly CryptoService _cryptoService;

    public FairRandom(CryptoService cryptoService)
    {
        _cryptoService = cryptoService;
    }

    public int GetFairNumber(int range)
    {
        // Step 1 & 2: Computer generates a secret key and a secret number.
        var secretKey = _cryptoService.GenerateKey();
        var computerNumber = _cryptoService.GenerateSecureRandomNumber(range);

        // Step 3: Calculate and display HMAC to the user.
        var hmac = _cryptoService.CalculateHmac(secretKey, computerNumber.ToString());
        Console.WriteLine($"I've selected a random value in the range 0..{range - 1}.");
        Console.WriteLine($"HMAC (proof of my choice): {hmac}");

        // Step 4: Get the user's number.
        Console.WriteLine($"Now, you select a number in the same range (0..{range - 1}).");
        var userNumber = GetUserNumber(range); // We'll write this helper method next.

        // Step 5 & 6: Calculate the result and show the secrets for verification.
        var finalResult = (computerNumber + userNumber) % range;

        Console.WriteLine($"\nMy secret number was: {computerNumber}");
        Console.WriteLine($"My secret key was: {secretKey}");
        Console.WriteLine("You can verify the HMAC to be sure I didn't cheat.");
        Console.WriteLine($"The final result is ({computerNumber} + {userNumber}) mod {range} = {finalResult}.");

        return finalResult;
    }

    private int GetUserNumber(int range)
    {
        while (true)
        {
            Console.Write("Your selection: ");
            var input = Console.ReadLine();

            if (int.TryParse(input, out var userNumber) && userNumber >= 0 && userNumber < range)
            {
                return userNumber;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Invalid input. Please enter a whole number between 0 and {range - 1}.");
            Console.ResetColor();
        }
    }
}
