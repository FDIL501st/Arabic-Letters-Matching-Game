using System;
using System.Collections.Generic;
using System.Reactive;
using System.Timers;
using ArabicLettersMatchingGame.Models;
using ArabicLettersMatchingGame.Models.Constants;
using ArabicLettersMatchingGame.Models.GameTimer;
using ArabicLettersMatchingGame.Services;
using Avalonia;
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
    private readonly MainMenuViewModel _menuView;
        
    // flag if game is in practice mode or not
    protected bool PracticeFlag { get; init; }
    
    // list of CardTexts, these are what text on cards in game views bind to
    public List<CardText> CardTexts { get; }
    
    // the command for pressing a card 
    protected ReactiveCommand<int, Unit> PressCardCommand { get; }

    protected readonly OneSecondTimer Timer;
    
    /// <summary>
    /// Parent class for GameViewModels.
    /// Provides common constructor and variables.
    /// </summary>
    protected GameViewModel(MainMenuViewModel menuView, GetTextPairsStrategy getTextPairService, bool practiceFlag = false)
    {
        PracticeFlag = practiceFlag;
        _menuView = menuView;
        CardTexts = CardText.GenerateGameCardTexts(getTextPairService.GetRandomPairs());
        
        // create command for pressing card
        PressCardCommand = ReactiveCommand.Create(
            (int num) => PressCommandFunction(num)
        );

        Timer = new OneSecondTimer(TimerElapsed);
    }
    
    // max number of pairs to make 
    public abstract int NumPairs { get; init; }
    
    // numbers of pairs made, initially start at 0
    private int PairsMade { get; set; } = 0;
    
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
    private readonly List<int> _selectedCards = new(2);

    /// <summary>
    /// The function that gets executed when a card gets pressed.
    /// </summary>
    /// <param name="i">The index of the Card/CardText in the list of cards/card texts.</param>
    private void PressCommandFunction(int i)
    {
        // Console.WriteLine($"Press card: {Cards[i].Content}");
        
        // first need to add card to selected, then make font visible
        _selectedCards.Add(i);
        
        
        // if in practice mode, add a border instead of making font visible
        if (PracticeFlag)
        {
            // use hard coded value of thickness 5
            Cards[i].BorderThickness = Thickness.Parse("5");
        }
        else
        {
            Cards[i].FontSize = CardFontSize.Easy;
        }
        
        switch (_selectedCards.Count)
        {
            // if at the moment only selected 1 card, wait for second card to be selected
            case 1:
            case > 2:
                return;
            // both cases where 1 card or more than 1 card, stop 
        }
        
        // second card selected
        
        // add a check so double-clicking the same card does nothing
        if (_selectedCards[0] == _selectedCards[1])
        {
            _selectedCards.RemoveAt(1);
            return;
        }
        
        // get index of selected cards
        var card1Index = _selectedCards[0];
        var card2Index = _selectedCards[1];
        
        // add delay
        FontSizeTransition.Delay = TimeSpan.FromSeconds(1);
        
        // if both cards selected are not a match, simply make font small again after a bit
        if (CardTexts[card1Index].Id != CardTexts[card2Index].Id)
        {
            // only make hidden not in practice mode
            if (!PracticeFlag)
            {
                // make cards hidden again as no match
                Cards[card1Index].FontSize = CardFontSize.Hidden;
                Cards[card2Index].FontSize = CardFontSize.Hidden;
            }
            
            // can't use && and move condition to outer if as need else to run
            // no matter if we are in practice mode or not
        }
        
        // both cards are a match, so disable cards
        else
        {
            Cards[card1Index].IsEnabled = false;
            Cards[card2Index].IsEnabled = false;
            
            // also add 1 to PairsMade
            PairsMade++;
        }
        
        // need to revert adding of border
        if (PracticeFlag)
        {
            Cards[card1Index].BorderThickness = Thickness.Parse("0");
            Cards[card2Index].BorderThickness = Thickness.Parse("0");
        }
        
        // remove all cards from Selected
        _selectedCards.RemoveAll((_) => true);
        
        // make delay 0 again
        FontSizeTransition.Delay = TimeSpan.Zero;
    }
    
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
            Timer.Dispose();
        }
    }

    /// <summary>
    /// When click return button, change view to menu view
    /// </summary>
    public void ClickReturn()
    {
        _menuView.Window.ContentViewModel = _menuView;
    }
}