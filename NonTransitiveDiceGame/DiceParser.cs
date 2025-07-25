using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonTransitiveDiceGame
{
    public class DiceParser
    {
        private const int MinimumDiceCount = 3;
        private const int FacesPerDie = 6;

        public IReadOnlyList<Dice> Parse(string[] args)
        {
            if (args.Length < MinimumDiceCount)
            {
                throw new ArgumentException($"Invalid input. At least {MinimumDiceCount} dice must be specified.");
            }

            var diceList = new List<Dice>();
            foreach (var arg in args)
            {
                var faceStrings = arg.Split(',');
                if (faceStrings.Length != FacesPerDie)
                {
                    throw new ArgumentException(
                        $"Invalid configuration for die \"{arg}\". Each die must have exactly {FacesPerDie} faces."
                    );
                }

                var faces = new List<int>();
                foreach (var faceString in faceStrings)
                {
                    if (!int.TryParse(faceString.Trim(), out var faceValue))
                    {
                        throw new ArgumentException(
                            $"Invalid value \"{faceString}\" in die configuration \"{arg}\". All face values must be integers."
                        );
                    }
                    faces.Add(faceValue);
                }
                diceList.Add(new Dice(faces));
            }

            return diceList;
        }
    }
}
