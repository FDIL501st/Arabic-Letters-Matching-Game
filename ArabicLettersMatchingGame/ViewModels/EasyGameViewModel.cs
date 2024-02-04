using System;
using System.Collections.Generic;
using System.Linq;
using ArabicLettersMatchingGame.Models;
using ArabicLettersMatchingGame.Models.Constants;
using ArabicLettersMatchingGame.Services;
using ArabicLettersMatchingGame.Views.DataTemplates;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using DynamicData.Kernel;
using ReactiveUI;
using Timer = System.Timers.Timer;

namespace ArabicLettersMatchingGame.ViewModels;

public class EasyGameViewModel : GameViewModel
{
    public sealed override List<Button> Cards { get; init; }
    public sealed override FuncDataTemplate<List<CardText>> GameArea { get; }

    public EasyGameViewModel(MainMenuViewModel menuView) : base(menuView, new EasyGetTextPairsStrategy())
    {
        
        // create card looks
        Cards = new List<Button>(GameBoardSizeNumber.Easy * GameBoardSizeNumber.Easy);
        for (var i = 0; i < GameBoardSizeNumber.Easy * GameBoardSizeNumber.Easy; i++)
        {
            Cards.Add(GameAreaDataTemplate.CreateButton("7", CardFontSize.Easy));
            Cards[i].Transitions!.Add(FontSizeTransition);
        }
        
        GameArea = new EasyGameAreaDataTemplate(Cards, PressCardCommand).GameArea;
    }
    
    protected override void PressCommandFunction(int i)
    {
        Console.WriteLine($"Press card: {Cards[i].Content}");

        
        // first need to add card to selected, then make font visible
        SelectedCards.Add(i);
        
        Cards[i].FontSize = CardFontSize.Easy;
        
        // if at the moment only selected 1 card, wait for second card to be selected
        if (SelectedCards.Count == 1) return;
        
        // second card selected
        
        // add delay
        FontSizeTransition.Delay = TimeSpan.FromSeconds(1);
        
        // get index of selected cards
        var card1Index = SelectedCards[0];
        var card2Index = SelectedCards[1];
        
        // if both cards selected are not a match, simply make font small again after a bit
        if (CardTexts[card1Index].Id != CardTexts[card2Index].Id )
        {
            // hide text of cards
            Cards[card1Index].FontSize = CardFontSize.Hidden;
            Cards[card2Index].FontSize = CardFontSize.Hidden;
        }
        
        // both cards are a match, so disable cards
        else
        {
            Cards[card1Index].IsEnabled = false;
            Cards[card2Index].IsEnabled = false;
        }
        
        // remove both from selected
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