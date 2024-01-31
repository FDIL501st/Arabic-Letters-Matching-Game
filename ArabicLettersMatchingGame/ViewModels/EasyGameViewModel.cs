using System;
using System.Collections.Generic;
using System.Reactive;
using ArabicLettersMatchingGame.Models;
using ArabicLettersMatchingGame.Models.Constants;
using ArabicLettersMatchingGame.Services;
using ArabicLettersMatchingGame.Views.DataTemplates;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using ReactiveUI;

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
        }
        
        GameArea = new EasyGameAreaDataTemplate(Cards, PressCardCommand).GameArea;
    }
    
    protected override void PressCommandFunction(int i)
    {
        // first need to add card to selected, then make font visible
        
        
        Console.WriteLine($"Press card {i}");
        
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