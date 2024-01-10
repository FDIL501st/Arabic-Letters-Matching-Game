using System.Text.Json;
using ArabicLettersMatchingGame.Services;
using Xunit;

namespace Tests;

public class TestGetTextPairStrategy
{
    [Fact]
    public void TestOpenJsonFile()
    {
        // test OpenJsonFile() function to see if file is actually open
        var testChild = new GetTextPairsStrategyTestChild();
        
        // call test function to open json file
        testChild.TestChildOpenJsonFile();
        
        // need to test for 2 things
        // 1. JsonFs is not null
        // 2. JsonFs is open (can read)

        var fs = testChild.TestChildGetFileStream();
        // file path seems to be an issue as test executable is within Tests/bin/Debug directory
        
        Assert.NotNull(fs);
        Assert.True(fs.CanRead);
    }
}

/*
 * Test child that makes protected things public so able to test them in this file.
 */
internal class GetTextPairsStrategyTestChild : GetTextPairsStrategy
{
    public JsonDocument TestChildOpenJsonFile()
    {
        return OpenJsonFile();
    }

    public FileStream? TestChildGetFileStream()
    {
        return JsonFs;
    }
}