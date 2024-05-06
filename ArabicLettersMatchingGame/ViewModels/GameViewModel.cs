using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive;
using System.Timers;
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
    // reference to MenuViewModel so can change view back to menu
    protected readonly MainMenuViewModel MenuView;
        
    // flag if game is in practice mode or not
    protected bool PracticeFlag { get; init; }
    
    // list of CardTexts, these are what text on cards in game views bind to
    public List<CardText> CardTexts { get; }
    
    // the command for pressing a card 
    protected ReactiveCommand<int, Unit> PressCardCommand { get; }

    protected Timer Timer;
    
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
        
        // start a timer that triggers an event every 1s
        Timer = new Timer(1000); // 1000 milliseconds = 1 second
        Timer.Elapsed += TimerElapsed; // Attach the event handler
        Timer.AutoReset = true; // Make the timer keep triggering an event (not just occur once)
        // timer must be started in implementation classes
        // NumPairs, which is used for checking to update pairs is not set in this class
    }
    
    // max number of pairs to make 
    public abstract int NumPairs { get; init; }
    
    // numbers of pairs made, initially start at 0
    protected int PairsMade { get; set; } = 0;
    
    // list of buttons that are used for the game area
    public abstract List<Button> Cards { get; init; }
    
    // data template that handles the game/cards
    public abstract FuncDataTemplate<List<CardText>> GameArea { get; }
    
    
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
    private RoundTimer _roundTimer = new();

    public RoundTimer RoundTimer
    {
        get => _roundTimer;

        set => this.RaiseAndSetIfChanged(ref _roundTimer, value);
    }
    
    private void TimerElapsed(object? sender, ElapsedEventArgs e)
    {
        // This method will be called every 1s by _timer
        
        // will update the RoundTimer if not yet found all pairs
        if (PairsMade < NumPairs)
        {
            // update the property, not the backing field so we call the RaiseAndSetIfChanged 
            // so Avalonia knows to update the UI
            RoundTimer = RoundTimer.AddOneSecondToTimer();
        }
        else
        {
            // stop the timer, at the moment only timer is _timer
            // so no need to check who the sender of the event is
            Timer.Stop();
            Timer.Dispose();
        }
    }

    public void Dispose()
    {
        Timer.Stop();
        Timer.Dispose();
    }
}