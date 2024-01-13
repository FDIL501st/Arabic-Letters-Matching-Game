namespace ArabicLettersMatchingGame.Models;

/// <summary>
/// A Data type to store pairs of text for games.
/// This data type has 2 strings which for the game are matching texts.
/// </summary>
/// <param name="Text1">a text of the pair</param>
/// <param name="Text2">a text of the pair</param>
public readonly record struct TextPair(string Text1, string Text2);