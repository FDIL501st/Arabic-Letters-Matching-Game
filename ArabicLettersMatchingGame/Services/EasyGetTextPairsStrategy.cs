using System.Collections.Generic;
using System.Linq;
using ArabicLettersMatchingGame.Models;
using DynamicData;
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
        
        // create an array of all numbers 0 to lettersLen
        // these keep track of indexes not used from letters
        var indexArray = new List<int>(lettersLen);
        for (var i = 0; i < lettersLen; i++) 
            indexArray.Add(i);
        
        for (var i = 0; i < numPairs ; i++)
        {
            // get a random index from indexArray
            var index = Rng.Next(0, indexArray.Count);
            var letterArray = GetLetterArray(indexArray.ElementAt(index));
            // used index, so remove from indexArray
            indexArray.RemoveAt(index);
            
            // create text pair
            textPairs.Add(CreateTextPairFromArray(letterArray, 0, Rng.Next(1, letterArray.Length)));
        }

        return textPairs;
    }
}