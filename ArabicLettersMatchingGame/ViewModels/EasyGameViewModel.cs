using System.Collections.Generic;
using ArabicLettersMatchingGame.Models;
using ArabicLettersMatchingGame.Models.Constants;
using ArabicLettersMatchingGame.Services;
using ArabicLettersMatchingGame.Views.DataTemplates;
using Avalonia.Controls.Templates;

namespace ArabicLettersMatchingGame.ViewModels;

public class EasyGameViewModel : GameViewModel
{
    public sealed override FuncDataTemplate<List<CardText>> GameArea { get; init; }

    public EasyGameViewModel(MainMenuViewModel menuView, bool practiceFlag = false) 
        : base(menuView, new EasyGetTextPairsStrategy(), CardFontSize.Easy, practiceFlag)
    {
        GameArea = new EasyGameAreaDataTemplate(Cards, PressCardCommand).GameArea;
        
        // start timer
        Timer.StartTimer();
    }

}