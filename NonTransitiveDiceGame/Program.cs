using NonTransitiveDiceGame;

try
{
    // Setup Phase: Parse arguments and initialize services.
    var parser = new DiceParser();
    var dice = parser.Parse(args);

    var cryptoService = new CryptoService();
    var fairRandom = new FairRandom(cryptoService);

    var probabilityCalculator = new ProbabilityCalculator();
    var helpRenderer = new HelpTableRenderer(dice, probabilityCalculator);
    // Execution Phase: Hand off control to the game manager.
    var game = new GameManager(dice, fairRandom, helpRenderer);
    game.Run();
}
catch (ArgumentException ex)
{
    // Error Handling: Catch validation errors and provide user-friendly feedback.
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"\nERROR: {ex.Message}");
    Console.ResetColor();
    Console.WriteLine("\nThe program requires at least 3 dice, each with 6 integer faces, as command-line arguments.");
    Console.WriteLine("\nUsage example:");
    Console.WriteLine(@"  dotnet run 2,2,4,4,9,9 6,8,1,1,8,6 7,5,3,7,5,3");
}
catch (Exception ex)
{
    // Catch any other unexpected errors to prevent a stack trace dump.
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"\nAn unexpected error occurred: {ex.Message}");
    Console.ResetColor();
}