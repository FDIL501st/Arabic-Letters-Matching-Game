using System.Collections.Generic;
using ArabicLettersMatchingGame.Models;
using ArabicLettersMatchingGame.Models.Constants;
using ArabicLettersMatchingGame.Services;
using ArabicLettersMatchingGame.Views.DataTemplates;
using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace ArabicLettersMatchingGame.ViewModels;

public class EasyGameViewModel : GameViewModel
{
    public sealed override List<Button> Cards { get; protected set; }
    public sealed override FuncDataTemplate<List<CardText>> GameArea { get; protected set; }

    public sealed override int NumPairs { get; protected set; }

    public EasyGameViewModel(MainMenuViewModel menuView, bool practiceFlag = false) 
        : base(menuView, new EasyGetTextPairsStrategy(), practiceFlag)
    {
        
        var numCards = GameBoardSizeNumber.Easy * GameBoardSizeNumber.Easy;
        
        // numbers of pairs to make for a game is
        NumPairs =  numCards / 2;
        
        Cards = InitCards(numCards, CardFontSize.Easy);
        
        GameArea = new EasyGameAreaDataTemplate(Cards, PressCardCommand).GameArea;
        
        // start timer
        Timer.StartTimer();
    }

}