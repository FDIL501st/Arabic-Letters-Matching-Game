using System.Collections.Generic;
using ListShuffle;

namespace ArabicLettersMatchingGame.Models;

/// <summary>
/// A data type that holds information about the text of a card in a game.
/// Can be used to check if two cards have a matching text.
/// </summary>
/// <param name="Text">String shown on a card</param>
/// <param name="Id">Id between matching cards is shared. 2 CardText with same Id are a pair.</param>
public record CardText(string Text, int Id)
{
    // a factory function to create a list of CardText from a list of TextPair

    // ID will simply be index of TextPair in their list.

    public static List<CardText> GenerateGameCardTexts(List<TextPair> textPairs)
    {
        List<CardText> cardTexts = new(textPairs.Count * 2);
        
        // fill cardTexts using each test in textPairs
        var id = 0;
        foreach (var textPair in textPairs)
        {
            cardTexts.Add(new CardText(textPair.Text1, id));
            cardTexts.Add(new CardText(textPair.Text2, id));

            id++;
        }
        
        // because need to be used in game, need to randomize order so each game is different match locations
        cardTexts.Shuffle();
        
        return cardTexts;
    }
}


