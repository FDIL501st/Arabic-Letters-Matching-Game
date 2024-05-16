namespace ArabicLettersMatchingGame.Models.Constants;

/// <summary>
/// Holds constants related to the font size of text on cards, depending on the difficulty.
/// </summary>
public static class CardFontSize
{
    // never constructed, just holds some constants about font size on a card

    public static int Hidden => 1;
    
    public static int Easy => 40;

    public static int Hard => 30;
}