using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using ArabicLettersMatchingGame.Models;
using ArabicLettersMatchingGame.Services;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using ReactiveUI;

namespace ArabicLettersMatchingGame.ViewModels;

/// <summary>
/// Parent class for GameViewModels.
/// Provides common constructor and variables.
/// </summary>
public abstract class GameViewModel : ViewModelBase
{
    protected GameViewModel()
    {
        RunRoundTimer().RunSynchronously();
    }
    
    // reference to MenuViewModel so can change view back to menu
    protected readonly MainMenuViewModel MenuView;
    
    /// <summary>
    /// Parent class for GameViewModels.
    /// Provides common constructor and variables.
    /// </summary>
    protected GameViewModel(MainMenuViewModel menuView, GetTextPairsStrategy getTextPairService, bool practiceFlag = false)
    {
        PracticeFlag = practiceFlag;
        MenuView = menuView;
        CardTexts = CardText.GenerateGameCardTexts(getTextPairService.GetRandomPairs());
        
        // create command for pressing card
        PressCardCommand = ReactiveCommand.Create(
            (int num) => PressCommandFunction(num)
        );
        
    }
    
    // flag if game is in practice mode or not
    public bool PracticeFlag { get; init; }
    
    // max number of pairs to make 
    public abstract int NumPairs { get; init; }
    
    // numbers of pairs made, initially start at 0
    public int PairsMade { get; set; } = 0;
    
    // list of buttons that are used for the game area
    public abstract List<Button> Cards { get; init; }
    
    // list of CardTexts, these are what text on cards in game views bind to
    public List<CardText> CardTexts { get; }
    
    // data template that handles the game/cards
    public abstract FuncDataTemplate<List<CardText>> GameArea { get; }

    // the command for pressing a card 
    protected ReactiveCommand<int, Unit> PressCardCommand { get; }
    
    // transition for font size change
    protected static readonly DoubleTransition FontSizeTransition = new()
    {
        Duration = TimeSpan.FromSeconds(1),
        Easing = new ExponentialEaseOut(),
        Property = TemplatedControl.FontSizeProperty,
    };
    
    
    // list that holds index selected cards
    protected readonly List<int> SelectedCards = new(2);

    /// <summary>
    /// The function that gets executed when a card gets pressed.
    /// </summary>
    /// <param name="i">The index of the Card/CardText in the list of cards/card texts.</param>
    protected abstract void PressCommandFunction(int i);


    // the value of the timer of how long the round has lasted
    public TimeOnly RoundTimer { get; set; } = TimeOnly.MinValue;

    private async Task RunRoundTimer()
    {
        // keep the round timer updating until make all pairs
        while (PairsMade < NumPairs)
        {
            // add 1s to RoundTimer
            RoundTimer = RoundTimer.Add(OneSecond);
            
            // stop this function for 1s, so this loop is run about every 1s
            await Task.Delay(1000);
        }
    }
    
    // one second TimeSpan, used for adding a second to the RoundTimer
    private static readonly TimeSpan OneSecond = TimeSpan.FromSeconds(1);
}