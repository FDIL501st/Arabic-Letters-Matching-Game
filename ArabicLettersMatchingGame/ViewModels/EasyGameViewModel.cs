using System;
using System.Collections.Generic;
using System.Reactive;
using ArabicLettersMatchingGame.Models;
using static ArabicLettersMatchingGame.Models.Constants.GameBoardSizeNumber;
using ArabicLettersMatchingGame.Services;
using ArabicLettersMatchingGame.Views.DataTemplates;
using Avalonia.Controls.Templates;
using ReactiveUI;

namespace ArabicLettersMatchingGame.ViewModels;

public class EasyGameViewModel : GameViewModel
{
    public sealed override FuncDataTemplate<List<CardText>> GameArea { get; }

    protected sealed override ReactiveCommand<int, Unit> PressCardCommand { get; }

    public EasyGameViewModel(MainMenuViewModel menuView) : base(menuView, new EasyGetTextPairsStrategy())
    {
        // create command when press card
        PressCardCommand = ReactiveCommand.Create(
            (int num) => PressCommandFunction(num)
            );
            
        GameArea = new EasyGameAreaDataTemplate(PressCardCommand).GameArea;
    }
    
    protected override void PressCommandFunction(int i)
    {
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