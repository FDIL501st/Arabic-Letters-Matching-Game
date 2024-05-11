using ArabicLettersMatchingGame.ViewModels;
using static ArabicLettersMatchingGame.Models.Constants.GameBoardSizeNumber;

namespace Tests;

/// <summary>
/// Tests for GameViewModels. 
/// </summary>
public class TestGameViewModel
{
    [Fact]
    public void TestEasyGameCardTextsCount()
    {
        var easyGameVm = new EasyGameViewModel(new MainMenuViewModel(new MainWindowViewModel()));
        
        // test number of card texts is actually Easy*Easy
        Assert.Equal(Easy*Easy, easyGameVm.CardTexts.Count);
    }

    [Fact]
    public void TestHardGameCardTextsCount()
    {
        // test number of card texts is Hard*Hard
    }
}