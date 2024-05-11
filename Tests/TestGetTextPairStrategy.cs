using ArabicLettersMatchingGame.Models;
using ArabicLettersMatchingGame.Services;
using static ArabicLettersMatchingGame.Models.Constants.GameBoardSizeNumber;

namespace Tests;

public class TestGetTextPairStrategy
{
    private readonly GetTextPairsStrategyTestChild _testChild = new();
    
    [Theory]
    [InlineData(1, new [] { "ﺏ", "ﺑ", "ﺐ", "ﺒ" })]
    [InlineData(-1, new [] {"ﺍ", "ﺎ"})]
    [InlineData(40, new [] {"ﺓ", "ﺔ"})]
    [InlineData(7, new [] {"ﺩ", "ﺪ"})]
    [InlineData(26, new [] {"ﻭ", "ﻮ"})]
    public void TestGetLetterArray(int i, string[] expected)
    {
        Assert.Equal(_testChild.TestChildGetLetterArray(i), expected);
    }
    
    // [Theory]
    // [InlineData(1, new [] { })]
    // [InlineData(-1, new [] { })]
    // [InlineData(40, new [] { })]
    // public void TestGetWordArray(int i, string[] expected)
    // {
    //     Assert.Equal(_testChild.TestChildGetWordArray(i), expected);
    // } 

    public class EasyGame
    {
        private readonly EasyGetTextPairsStrategy _testEasy = new ();
        private readonly List<TextPair> _randomPairs;

        public EasyGame()
        {
            _randomPairs = _testEasy.GetRandomPairs();
        }
        
        // no need for cleanup code, we use different List object everytime (_randomPairs is set to a new one)
        // so simply let it go out of scope and let garbage collector deal with it
        
        [Fact]
        public void TestEasyGetRandomPairsCount()
        {
            Assert.Equal(Easy*Easy/2, _randomPairs.Count);
        }

        [Fact]
        public void TestEasyGetRandomPairsTextIsDifferent()
        {
            // check out HardGame for comments on thoughts while making the test
            // as that test was coded first

            HashSet<string> allText = new (Easy*Easy);

            const string msg = "Unexpected duplicate text found in GetRandomPairs of EasyGame.";
            foreach (var textPair in _randomPairs)
            {
                Assert.False(allText.Add(textPair.Text1), msg);
                Assert.False(allText.Add(textPair.Text2), msg);
            }
        }
    }
    
    public class HardGame
    {
        private readonly HardGetTextPairsStrategy _testHard = new();
        private readonly List<TextPair> _randomPairs;

        public HardGame()
        {
            _randomPairs = _testHard.GetRandomPairs();
        }
        
        // no need for cleanup code
        
        [Fact]
        public void TestHardGetRandomPairsCount()
        {
            Assert.Equal(Hard*Hard/2, _randomPairs.Count);
        }

        [Fact]
        public void TestHardGetRandomPairsTextIsDifferent()
        {
            // testing all the text of all the pairs is all different from each other
        
            // do this by using a set
        
            // loop through, asserting the text is not within the set, then adding it to the set
        
            // this test will fail if at some point we find same text in the set 
        
            // it is okay if for some reason there is a pair of same text, we add first text into set and passes assert
            // as second one will match, causing failure

            HashSet<string> allText = new (Hard*Hard);

            const string msg = "Unexpected duplicate text found in GetRandomPairs of HardGame.";
            foreach (var textPair in _randomPairs)
            {
                // each textPair has 2 strings, so we check both
                
                Assert.False(allText.Add(textPair.Text1), msg);
                Assert.False(allText.Add(textPair.Text2), msg);
            }
            
            // another way is to add all text first, then check of _randomPairs and allText have same count
            // if they don't that means there were some duplicates, thus weren't added to the set
        }
    }
    
}

/*
 * Test child that makes protected things public so able to test them in this file.
 */
internal class GetTextPairsStrategyTestChild : GetTextPairsStrategy
{
    public string[] TestChildGetLetterArray(int i)
    {
        return GetLetterArray(i);
    }

    public string[] TestChildGetWordArray(int i)
    {
        return GetWordArray(i);
    }

    public override List<TextPair> GetRandomPairs()
    {
        throw new NotImplementedException();
    }
}