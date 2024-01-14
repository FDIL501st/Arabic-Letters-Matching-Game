namespace ArabicLettersMatchingGame.Models.Constants;

/// <summary>
/// Holds global constants for the number of cards on 1 side for a Game.
/// This means if a constant is n, game board is n x n, you have to make n*n/2 matches.
/// </summary>
public static class GameBoardSizeNumber
{
    // Class is never constructed, just holds constants known at compile time

    // constants
    public static int Easy => 4;

    public static int Medium => 5;

    public static int Hard => 6;
}