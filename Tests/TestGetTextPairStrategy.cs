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

    [Fact]
    public void TestEasyGetRandomPairs()
    {
        EasyGetTextPairsStrategy testEasy = new ();
        
        // attempt to get random pairs 10 times
        for (var i = 0; i < 10; i++)
        {
            var randomPairs = testEasy.GetRandomPairs();
        
            // check if got expected number of pairs
            // which should be Easy*Easy/2
            Assert.Equal(Easy*Easy/2, randomPairs.Count);
        
            // check each TextPair is not null or empty
            foreach (var (text1, text2) in randomPairs)
            {
                Assert.NotNull(text1);
                Assert.NotNull(text2);
                
                Assert.NotEqual("", text1);
                Assert.NotEqual("", text2);
            }
        }
        
        // attempt to improve unit test, as this test is poorly made
    }

    private readonly HardGetTextPairsStrategy _testHard = new();

    [Fact]
    public void TestHardGetRandomPairsCount()
    {
        var randomPairs = _testHard.GetRandomPairs();
        Assert.Equal(Hard*Hard/2, randomPairs.Count);
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