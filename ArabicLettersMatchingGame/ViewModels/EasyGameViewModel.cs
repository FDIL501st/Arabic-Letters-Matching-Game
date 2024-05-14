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
    public sealed override List<Button> Cards { get; init; }
    public sealed override FuncDataTemplate<List<CardText>> GameArea { get; }

    public sealed override int NumPairs { get; init; }

    public EasyGameViewModel(MainMenuViewModel menuView, bool practiceFlag = false) 
        : base(menuView, new EasyGetTextPairsStrategy(), practiceFlag)
    {

        int numCards = GameBoardSizeNumber.Easy * GameBoardSizeNumber.Easy;
        
        // numbers of pairs to make for an easy game is
        NumPairs =  numCards / 2;
        
        // create card looks
        Cards = new List<Button>(numCards);
        for (var i = 0; i < numCards; i++)
        {
            if (!PracticeFlag)
            {
                Cards.Add(GameAreaDataTemplate.CreateButton("7", 0));
                Cards[i].Transitions!.Add(FontSizeTransition);
            }
            else
            {
                // in practice mode, cards are not hidden
                Cards.Add(GameAreaDataTemplate.CreateButton("7", CardFontSize.Easy));
            }
            
        }
        
        GameArea = new EasyGameAreaDataTemplate(Cards, PressCardCommand).GameArea;
        
        // start timer
        Timer.StartTimer();
    }
    
}