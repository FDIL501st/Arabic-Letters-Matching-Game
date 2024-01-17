using System.Collections.Generic;
using ArabicLettersMatchingGame.Models;
using ArabicLettersMatchingGame.Services;
using ArabicLettersMatchingGame.Views.DataTemplates;
using Avalonia.Controls.Templates;

namespace ArabicLettersMatchingGame.ViewModels;

public class EasyGameViewModel(MainMenuViewModel menuView) : GameViewModel(menuView, new EasyGetTextPairsStrategy())
{
    public sealed override FuncDataTemplate<List<CardText>> GameArea { get; } = new EasyGameAreaDataTemplate().GameArea;

    // add a timer updater?
    
    /// <summary>
    /// When click return button, change view to menu view
    /// </summary>
    public void ClickReturn()
    {
        MenuView.Window.ContentViewModel = MenuView;
    }
}