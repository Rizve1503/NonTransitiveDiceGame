using ConsoleTables;

namespace NonTransitiveDiceGame;

public class HelpTableRenderer
{
    private readonly IReadOnlyList<Dice> _dice;
    private readonly ProbabilityCalculator _calculator;

    public HelpTableRenderer(IReadOnlyList<Dice> dice, ProbabilityCalculator calculator)
    {
        _dice = dice;
        _calculator = calculator;
    }

    public void DisplayHelpTable()
    {
        Console.WriteLine("\n--- Win Probability Table ---");
        Console.WriteLine("This table shows the probability of the 'Row' die winning against the 'Column' die.");

        // The headers will be the string representations of the dice.
        var headers = new[] { "User Dice v" }.Concat(_dice.Select(d => d.ToString())).ToArray();
        var table = new ConsoleTable(headers);

        foreach (var rowDie in _dice)
        {
            var tableRow = new List<object> { rowDie.ToString() };
            foreach (var colDie in _dice)
            {
                if (rowDie == colDie)
                {
                    // It's ambiguous what playing against yourself means. We'll mark it.
                    tableRow.Add("---");
                }
                else
                {
                    var probability = _calculator.CalculateWinProbability(rowDie, colDie);
                    tableRow.Add(probability.ToString("F4")); // Format to 4 decimal places.
                }
            }
            table.AddRow(tableRow.ToArray());
        }

        table.Write(Format.Alternative); // This format looks good in most terminals.
        Console.WriteLine();
    }
}