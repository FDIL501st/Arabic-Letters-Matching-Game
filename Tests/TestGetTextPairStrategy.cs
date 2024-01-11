using System.Text.Json;
using ArabicLettersMatchingGame.Models;
using ArabicLettersMatchingGame.Services;

namespace Tests;

public class TestGetTextPairStrategy
{
    private GetTextPairsStrategyTestChild _testChild = new();
    
    [Fact]
    public void TestOpenJsonFile()
    {
        // call test function to open json file
        _testChild.TestChildOpenJsonFile();
        
        // need to test for 2 things
        // 1. JsonFs is not null
        // 2. JsonFs is open (can read)

        var fs = _testChild.TestChildGetFileStream();
        // file path seems to be an issue as test executable is within Tests/bin/Debug directory
        
        Assert.NotNull(fs);
        Assert.True(fs.CanRead);
        
        // make sure to close json file
        fs.Close();
    }

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
}

/*
 * Test child that makes protected things public so able to test them in this file.
 */
internal class GetTextPairsStrategyTestChild : GetTextPairsStrategy
{
    public JsonElement TestChildOpenJsonFile()
    {
        return OpenJsonFile();
    }

    public FileStream? TestChildGetFileStream()
    {
        return JsonFs;
    }

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