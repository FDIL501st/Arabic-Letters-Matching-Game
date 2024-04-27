using System;
using System.Collections.Generic;
using ArabicLettersMatchingGame.Models;
using ArabicLettersMatchingGame.Models.Constants;
using ArabicLettersMatchingGame.Services;
using ArabicLettersMatchingGame.Views.DataTemplates;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Media;

namespace ArabicLettersMatchingGame.ViewModels;

public class EasyGameViewModel : GameViewModel
{
    public sealed override List<Button> Cards { get; init; }
    public sealed override FuncDataTemplate<List<CardText>> GameArea { get; }

    public EasyGameViewModel(MainMenuViewModel menuView, bool practiceFlag = false) 
        : base(menuView, new EasyGetTextPairsStrategy(), practiceFlag)
    {
        // create card looks
        Cards = new List<Button>(GameBoardSizeNumber.Easy * GameBoardSizeNumber.Easy);
        for (var i = 0; i < GameBoardSizeNumber.Easy * GameBoardSizeNumber.Easy; i++)
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
    }
    
    protected override void PressCommandFunction(int i)
    {
        // Console.WriteLine($"Press card: {Cards[i].Content}");
        
        // first need to add card to selected, then make font visible
        SelectedCards.Add(i);
        
        
        // if in practice mode, just make size larger, otherwise visible
        Cards[i].FontSize = (PracticeFlag) ? CardFontSize.Easy + 20 : CardFontSize.Easy;
        
        switch (SelectedCards.Count)
        {
            // if at the moment only selected 1 card, wait for second card to be selected
            case 1:
            case > 2:
                return;
            // both cases where 1 card or more than 1 card, stop 
        }
        
        // second card selected
        
        // add a check so double-clicking the same card does nothing
        if (SelectedCards[0] == SelectedCards[1])
        {
            SelectedCards.RemoveAt(1);
            return;
        }
        
        // get index of selected cards
        var card1Index = SelectedCards[0];
        var card2Index = SelectedCards[1];
        
        // add delay
        FontSizeTransition.Delay = TimeSpan.FromSeconds(1);
        
        // if both cards selected are not a match, simply make font small again after a bit
        if (CardTexts[card1Index].Id != CardTexts[card2Index].Id)
        {
            // only make hidden not in practice mode
            if (!PracticeFlag)
            {
                // make cards hidden again as no match
                Cards[card1Index].FontSize = CardFontSize.Hidden;
                Cards[card2Index].FontSize = CardFontSize.Hidden;
            }
            
            // can't use && and move condition to outer if as need else to run
            // no matter if we are in practice mode or not
        }
        
        // both cards are a match, so disable cards
        else
        {
            Cards[card1Index].IsEnabled = false;
            Cards[card2Index].IsEnabled = false;
        }
        
        // need to revert size of cards back to regular, no matter if cards are a match or not
        if (PracticeFlag)
        {
            Cards[card1Index].FontSize = CardFontSize.Easy;
            Cards[card2Index].FontSize = CardFontSize.Easy;
        }
        
        // remove all cards from Selected
        SelectedCards.RemoveAll((_) => true);
        
        // make delay 0 again
        FontSizeTransition.Delay = TimeSpan.Zero;
    }
    
    // add a timer updater?
    
    /// <summary>
    /// When click return button, change view to menu view
    /// </summary>
    public void ClickReturn()
    {
        MenuView.Window.ContentViewModel = MenuView;
    }
}