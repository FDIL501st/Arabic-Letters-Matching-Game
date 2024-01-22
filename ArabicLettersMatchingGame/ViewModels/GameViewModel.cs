using System;
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
public abstract class GameViewModel(MainMenuViewModel menuView, GetTextPairsStrategy getTextPairService)
    : ViewModelBase
{
    // reference to MenuViewModel so can change view back to menu
    protected readonly MainMenuViewModel MenuView = menuView;
    
    // list of CardTexts, these are what text on cards in game views bind to
    public List<CardText> CardTexts { get; } = CardText.GenerateGameCardTexts(getTextPairService.GetRandomPairs());
    
    // data template that handles the game/cards
    public abstract FuncDataTemplate<List<CardText>> GameArea { get; }

    // the command for pressing a card 
    protected ReactiveCommand<Unit, Unit> PressCardCommand { get; } = ReactiveCommand.Create(
        () => Console.WriteLine("Pressed a card")
    );
}