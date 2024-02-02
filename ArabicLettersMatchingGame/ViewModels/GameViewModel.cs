using System.Collections.Generic;
using System.Reactive;
using ArabicLettersMatchingGame.Models;
using ArabicLettersMatchingGame.Services;
using Avalonia.Controls;
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
        
        // create command for pressing card
        PressCardCommand = ReactiveCommand.Create(
            (int num) => PressCommandFunction(num)
        );
        
    }
    
    // list of buttons that are used for the game area
    public abstract List<Button> Cards { get; init; }
    
    // list of CardTexts, these are what text on cards in game views bind to
    public List<CardText> CardTexts { get; }
    
    // data template that handles the game/cards
    public abstract FuncDataTemplate<List<CardText>> GameArea { get; }

    // the command for pressing a card 
    protected ReactiveCommand<int, Unit> PressCardCommand { get; }
    
    // list that holds index selected cards
    protected readonly List<int> SelectedCards = new(2);

    /// <summary>
    /// The function that gets executed when a card gets pressed.
    /// </summary>
    /// <param name="i">The index of the Card/CardText in the list of cards/card texts.</param>
    protected abstract void PressCommandFunction(int i);
}