using System.Collections.Generic;
using ArabicLettersMatchingGame.Models;

namespace ArabicLettersMatchingGame.Services;


public class EasyGetTextPairsStrategy : GetTextPairsStrategy
{
    public override List<TextPair> GetRandomPairs()
    {
        var textPairs = new List<TextPair>();
        
        // this implementation will only read from letters
        // and will have use [0] of array chosen
        // choose 4 letters (might want to make this a shared constant with view)

        const int numPairs = 4;
        var lettersLen = OpenJsonFile().GetProperty("len_letters").GetUInt16();
        
        // move file back to beginning as will be parsing it again
        // Parse function won't be able to recognize json file if pointer in middle
        // after using the GetProperty function
        JsonFs!.Seek(0, 0);
        
        for (var i = 0; i < numPairs; i++)
        {
            var letterArray = GetLetterArray(Rng.Next(lettersLen));
            textPairs.Add(CreateTextPairFromArray(letterArray, 0, Rng.Next(1, letterArray.Length)));
        }

        return textPairs;
    }
}