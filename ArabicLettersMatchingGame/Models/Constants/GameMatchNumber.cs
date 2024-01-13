namespace ArabicLettersMatchingGame.Models.Constants;

/// <summary>
/// Holds global constants for number of matches to make to complete a game.
/// </summary>
public static class GameMatchNumber
{
    // Class is never constructed, just holds constants known at compile time

    // constants
    public static int Easy { get; } = 4;

    public static int Medium { get; } = 5;

    public static int Hard { get; } = 6;
}