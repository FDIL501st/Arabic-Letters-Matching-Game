using System.Collections.Generic;
using ArabicLettersMatchingGame.Models;
using ArabicLettersMatchingGame.Models.Constants;
using ArabicLettersMatchingGame.Services;
using ArabicLettersMatchingGame.Views.DataTemplates;
using Avalonia.Controls.Templates;

namespace ArabicLettersMatchingGame.ViewModels;

public class HardGameViewModel: GameViewModel
{
    public sealed override FuncDataTemplate<List<CardText>> GameArea { get; init; }
    
    public HardGameViewModel(MainMenuViewModel menuView, bool practiceFlag = false) 
        : base(menuView, new HardGetTextPairsStrategy(), CardFontSize.Hard, practiceFlag)
    {
        GameArea = new HardGameAreaDataTemplate(Cards, PressCardCommand).GameArea;
        
        // start timer
        Timer.StartTimer();
    }
}