using System.Collections.Generic;
using System.Linq;
using ArabicLettersMatchingGame.Models;
using static ArabicLettersMatchingGame.Models.Constants.GameBoardSizeNumber;

namespace ArabicLettersMatchingGame.Services;

public class HardGetTextPairsStrategy: GetTextPairsStrategy
{
    public override List<TextPair> GetRandomPairs()
    {
        var numPairs = Hard * Hard / 2;
        
        var textPairs = new List<TextPair>(numPairs);
        
        var lettersLen = JsonRoot.GetProperty("len_words").GetUInt16();
        
        // keep track of index of words, as we randomly choose index for our random words
        var indexArray = new List<int>(lettersLen);
        for (var i = 0; i < lettersLen; i++) 
            indexArray.Add(i);
        // make an array where each element is the same as the index
        // eg. [0, 1, 2, 3]
        
        
        // go and get the random words and fill up textPairs
        for (var i = 0; i < numPairs; i++)
        {
            // get a random index from indexArray
            var index = Rng.Next(0, indexArray.Count);
            var wordArray = GetWordArray(indexArray.ElementAt(index));
            // used index, so remove from indexArray
            indexArray.RemoveAt(index);
            
            // create the text pair
            textPairs.Add(CreateTextPairFromArray(wordArray, 0, 1));
            // each word has 2 elements, so use them for our pair
        }
        
        return textPairs;
    }
}