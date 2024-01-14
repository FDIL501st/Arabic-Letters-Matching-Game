using System.Collections.Generic;
using ArabicLettersMatchingGame.Models;
using static ArabicLettersMatchingGame.Models.Constants.GameBoardSizeNumber;

namespace ArabicLettersMatchingGame.Services;


public class EasyGetTextPairsStrategy : GetTextPairsStrategy
{
    public override List<TextPair> GetRandomPairs()
    {
        var numPairs = Easy * Easy / 2;
        
        var textPairs = new List<TextPair>(numPairs);
        
        // this implementation will only read from letters
        // and will have use [0] of array chosen
        // choose # of letters = # of matches in easy game
        
        var lettersLen = JsonRoot.GetProperty("len_letters").GetUInt16();
        
        
        for (var i = 0; i < numPairs ; i++)
        {
            var letterArray = GetLetterArray(Rng.Next(lettersLen));
            textPairs.Add(CreateTextPairFromArray(letterArray, 0, Rng.Next(1, letterArray.Length)));
        }

        return textPairs;
    }
}