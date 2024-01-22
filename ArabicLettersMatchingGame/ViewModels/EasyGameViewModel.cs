using System;
using System.Collections.Generic;
using System.Reactive;
using ArabicLettersMatchingGame.Models;
using ArabicLettersMatchingGame.Services;
using ArabicLettersMatchingGame.Views.DataTemplates;
using Avalonia.Controls.Templates;
using ReactiveUI;

namespace ArabicLettersMatchingGame.ViewModels;

public class EasyGameViewModel : GameViewModel
{
    public sealed override FuncDataTemplate<List<CardText>> GameArea { get; }

    
    public EasyGameViewModel(MainMenuViewModel menuView) : base(menuView, new EasyGetTextPairsStrategy())
    {
        GameArea = new EasyGameAreaDataTemplate(PressCardCommand).GameArea;
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