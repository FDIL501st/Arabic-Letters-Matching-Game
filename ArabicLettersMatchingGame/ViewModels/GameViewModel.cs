using System.Collections.Generic;
using System.Reactive;
using ArabicLettersMatchingGame.Models;
using ArabicLettersMatchingGame.Services;
using Avalonia.Controls.Templates;
using ReactiveUI;

namespace ArabicLettersMatchingGame.ViewModels;

/// <summary>
/// Parent class for GameViewModels.
/// Provides common constructor and variables.
/// </summary>
public abstract class GameViewModel : ViewModelBase
{
    // reference to MenuViewModel so can change view back to menu
    protected readonly MainMenuViewModel MenuView;

    /// <summary>
    /// Parent class for GameViewModels.
    /// Provides common constructor and variables.
    /// </summary>
    protected GameViewModel(MainMenuViewModel menuView, GetTextPairsStrategy getTextPairService)
    {
        MenuView = menuView;
        CardTexts = CardText.GenerateGameCardTexts(getTextPairService.GetRandomPairs());
    }

    // list of CardTexts, these are what text on cards in game views bind to
    public List<CardText> CardTexts { get; }
    
    // data template that handles the game/cards
    public abstract FuncDataTemplate<List<CardText>> GameArea { get; }

    // the command for pressing a card 
    protected abstract ReactiveCommand<int, Unit> PressCardCommand { get; }
    
    // list that holds selected cards
    protected List<CardText> SelectedCards = new List<CardText>(2);

    protected abstract void PressCommandFunction(int i);
}