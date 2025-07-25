using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonTransitiveDiceGame
{
    public class GameManager
    {
        private readonly IReadOnlyList<Dice> _initialDice;
        private readonly FairRandom _fairRandom;
        private readonly HelpTableRenderer _helpRenderer;

        public GameManager(IReadOnlyList<Dice> dice, FairRandom fairRandom, HelpTableRenderer helpRenderer)
        {
            _initialDice = dice;
            _fairRandom = fairRandom;
            _helpRenderer = helpRenderer;
        }

        public void Run()
        {
            Console.WriteLine("--- Welcome to the Non-Transitive Dice Game! ---");

            // Step 1: Determine who makes the first move.
            Console.WriteLine("\nLet's determine who makes the first move.");
            var userGoesFirst = DetermineFirstPlayer();

            // Step 2: Players select their dice.
            Dice userDice;
            Dice computerDice;
            var availableDice = new List<Dice>(_initialDice);

            if (userGoesFirst)
            {
                Console.WriteLine("You won the toss! You get to choose your die first.");
                userDice = PlayerChooseDie(availableDice);
                availableDice.Remove(userDice); // The computer can't pick the same die.
                computerDice = ComputerChooseDie(availableDice);
            }
            else
            {
                Console.WriteLine("I won the toss. I will choose my die first.");
                computerDice = ComputerChooseDie(availableDice);
                availableDice.Remove(computerDice); // You can't pick the same die.
                Console.WriteLine($"I have chosen the {computerDice} die.");
                userDice = PlayerChooseDie(availableDice);
            }

            Console.WriteLine($"\nYour choice: {userDice} | My choice: {computerDice}");

            // Step 3: Perform the rolls.
            Console.WriteLine("\n--- Time to Roll! ---");
            var (userRoll, computerRoll) = PerformRolls(userDice, computerDice);

            // Step 4: Announce the winner.
            AnnounceWinner(userRoll, computerRoll);
        }

        private bool DetermineFirstPlayer()
        {
            var result = _fairRandom.GetFairNumber(2); // Range is 0-1
                                                       // Per the hint: if the user "guesses" the computer's number, the user wins.
                                                       // This happens if the final result is 0 (e.g., comp=0, user=0 -> sum=0; comp=1, user=1 -> sum=2, mod 2 is 0).
            return result == 0;
        }

        private Dice ComputerChooseDie(List<Dice> availableDice)
        {
            var dieIndex = _fairRandom.GetFairNumber(availableDice.Count);
            return availableDice[dieIndex];
        }

        private Dice PlayerChooseDie(List<Dice> availableDice)
        {
            Console.WriteLine("Please choose your die:");
            while (true)  // This loop now handles re-prompting after help.
            {
                for (var i = 0; i < availableDice.Count; i++)
                {
                    Console.WriteLine($"  {i}: {availableDice[i]}");
                }
                Console.WriteLine("  X: Exit game");
                Console.WriteLine("  ?: Show help table");
                Console.Write("Your selection: ");
                var input = Console.ReadLine()?.ToUpper();

                switch (input)
                {
                    case "X":
                        Console.WriteLine("Exiting game. Goodbye!");
                        Environment.Exit(0);
                        break; // Unreachable, but good practice.
                    case "?":
                        _helpRenderer.DisplayHelpTable(); // DISPLAY THE TABLE
                        Console.WriteLine("Please choose your die:"); // Re-prompt the user
                        continue; // Go back to the start of the loop.
                }

                if (int.TryParse(input, out var choice) && choice >= 0 && choice < availableDice.Count)
                {
                    return availableDice[choice];
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Invalid selection. Please try again.");
                Console.ResetColor();
            }
        }

        private (int userRoll, int computerRoll) PerformRolls(Dice userDice, Dice computerDice)
        {
            Console.WriteLine("\nIt's time for my roll.");
            var computerRollIndex = _fairRandom.GetFairNumber(computerDice.Faces.Count);
            var computerRollValue = computerDice.Faces[computerRollIndex];
            Console.WriteLine($"My roll result is: {computerRollValue}");

            Console.WriteLine("\nIt's time for your roll.");
            var userRollIndex = _fairRandom.GetFairNumber(userDice.Faces.Count);
            var userRollValue = userDice.Faces[userRollIndex];
            Console.WriteLine($"Your roll result is: {userRollValue}");

            return (userRollValue, computerRollValue);
        }

        private void AnnounceWinner(int userRoll, int computerRoll)
        {
            Console.WriteLine("\n--- Game Over ---");
            if (userRoll > computerRoll)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"You win! ({userRoll} > {computerRoll})");
                Console.ResetColor();
            }
            else if (computerRoll > userRoll)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"You lose. ({computerRoll} > {userRoll})");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"It's a draw! ({userRoll} = {computerRoll})");
                Console.ResetColor();
            }
        }
    }
}
