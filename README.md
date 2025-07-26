## Non-Transitive Dice Game

A C#/.NET console application that implements a generalized, non-transitive dice game with a provably fair random number generation protocol. This project was created as a solution for Task #3 of the iTransition traineeship program.

**Overview**

This application allows users to define a set of custom dice via command-line arguments and play a game where the winner is determined by a single roll. The core of this project is its "provably fair" randomness protocol, which uses a cryptographic HMAC-based system to ensure the computer cannot cheat. This requires collaborative input from both the user and the computer for every random event.

The project is built with a strong emphasis on clean architecture, adhering to SOLID principles, with distinct classes for parsing, game logic, cryptographic services, and UI rendering.

**Features**
**Configurable Dice**: Define 3 or more dice with custom face values directly from the command line.

**Provably Fair Randomness**: All random events (first-player selection, computer die choice, and die rolls) use a secure HMAC-based protocol that prevents computer cheating and ensures fairness.

**Detailed Input Validation**: Provides clear, user-friendly error messages for invalid inputs (e.g., wrong number of dice, incorrect face values) instead of stack traces.

**Interactive Help Menu**: Includes a built-in help system (triggered by ?) that displays a dynamically generated ASCII table of win probabilities for every possible dice matchup.

**Clean, SOLID Architecture**: The codebase is separated into multiple single-responsibility classes for maintainability, testability, and clarity.

**Technology Stack**
Language: C#

Framework: .NET 8

**Key Libraries:**
System.Security.Cryptography for secure random number generation and HMAC-SHA256.

ConsoleTables (NuGet package) for rendering the help table.

**Getting Started
Prerequisites**

.NET 8 SDK

Git

Installation

**Clone the repository to your local machine:**

git clone https://github.com/Rizve1503/itransition-dice-game.git


**Navigate to the project directory:**
cd itransition-dice-game/NonTransitiveDiceGame

The application is run from the command line using the dotnet run command, followed by the dice configurations. Each die is a single string of 6 comma-separated integers.

Basic Example

# The format is: dotnet run <die1> <die2> <die3> ...
dotnet run 2,2,4,4,9,9 6,8,1,1,8,6 7,5,3,7,5,3
Displaying the Help Table

When prompted to select a die, enter ? to display the win-probability table.

Please choose your die:
  0: [2,2,4,4,9,9]
  1: [6,8,1,1,8,6]
  2: [7,5,3,7,5,3]
  X: Exit game
  ?: Show help table
Your selection: ?```

### Invalid Input Examples

The application will provide helpful error messages for invalid arguments.

-   **Not enough dice:**
    ```powershell
    dotnet run 1,1,1,1,1,1 2,2,2,2,2,2
    # Output: ERROR: Invalid input. At least 3 dice must be specified.
    ```
-   **Incorrect number of faces:**
    ```powershell
    dotnet run 1,2,3,4,5,6 1,2,3,4,5 1,2,3,4,5,6
    # Output: ERROR: Invalid configuration for die "1,2,3,4,5". Each die must have exactly 6 faces.
    ```
-   **Non-integer value:**
    ```powershell
    dotnet run 1,2,3,4,5,6 1,2,hello,4,5,6 1,2,3,4,5,6
    # Output: ERROR: Invalid value "hello" in die configuration "1,2,hello,4,5,6". All face values must be integers.
    ```

## Code Structure

The project is organized into several classes, each with a single responsibility:

-   `Program.cs`: The application entry point. Responsible for dependency setup and top-level error handling.
-   `Dice.cs`: A C# `record` representing a single die with its face values.
-   `DiceParser.cs`: Handles parsing and validation of command-line arguments.
-   `CryptoService.cs`: Provides all cryptographic primitives: secure key/number generation and HMAC calculation.
-   `FairRandom.cs`: Implements the user-facing "provably fair" protocol using the `CryptoService`.
-   `ProbabilityCalculator.cs`: Contains the logic for calculating the win probability between any two dice.
-   `HelpTableRenderer.cs`: Uses the `ConsoleTables` library to render the probability help table.
-   `GameManager.cs`: Orchestrates the entire game flow, from the first-player toss to declaring the winner.

---
