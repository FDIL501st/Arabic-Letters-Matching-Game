using System;
using System.Collections.Generic;
using System.Reactive;
using System.Timers;
using ArabicLettersMatchingGame.Models;
using ArabicLettersMatchingGame.Models.Constants;
using ArabicLettersMatchingGame.Models.GameTimer;
using ArabicLettersMatchingGame.Services;
using ArabicLettersMatchingGame.Views.DataTemplates;
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
    private readonly bool _practiceFlag;
    
    // list of CardTexts, these are what text on cards in game views bind to
    public List<CardText> CardTexts { get; }
    
    // the command for pressing a card 
    protected ReactiveCommand<int, Unit> PressCardCommand { get; }

    protected readonly OneSecondTimer Timer;
    
    /// <summary>
    /// Parent class for GameViewModels.
    /// Provides common constructor and variables.
    /// </summary>
    protected GameViewModel(MainMenuViewModel menuView, 
        GetTextPairsStrategy getTextPairService, int cardFont, bool practiceFlag = false)
    {
        _practiceFlag = practiceFlag;
        _menuView = menuView;
        CardTexts = CardText.GenerateGameCardTexts(getTextPairService.GetRandomPairs());
        
        // create command for pressing card
        PressCardCommand = ReactiveCommand.Create(
            (int num) => PressCommandFunction(num, cardFont)
        );

        Timer = new OneSecondTimer(TimerElapsed);
        
        
        // initialize the cards, requires knowing how many of them we need
        var numCards = GameBoardSizeNumber.Hard * GameBoardSizeNumber.Hard;
        Cards = InitCards(numCards, CardFontSize.Hard);
        
        // numbers of pairs to make for a game is
        _numPairs =  numCards / 2;
    }
    
    private List<Button> InitCards(int numCards, int maxFontSize)
    {
        // maxFontSize available in constructor, so trying moving this function
        
        // create card looks
        var cards = new List<Button>(numCards);
        for (var i = 0; i < numCards; i++)
        {
            if (!_practiceFlag)
            {
                cards.Add(GameAreaDataTemplate.CreateButton("7", 0));
                cards[i].Transitions!.Add(FontSizeTransition);
            }
            else
            {
                // in practice mode, cards are not hidden
                cards.Add(GameAreaDataTemplate.CreateButton("7", maxFontSize));
            }
        }

        return cards;
    }
    
    // max number of pairs to make 
    private readonly int _numPairs;
    
    // numbers of pairs made, initially start at 0 (auto default value is 0, no need to specify)
    private int _pairsMade;
    
    // list of buttons that are used for the game area
    protected List<Button> Cards { get; }
    
    // data template that handles the game/cards
    public abstract FuncDataTemplate<List<CardText>> GameArea { get; init; }

    // transition for font size change
    private static readonly DoubleTransition FontSizeTransition = new()
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
    /// <param name="cardFont">The font size of text on the card</param>
    private void PressCommandFunction(int i, int cardFont)
    {
        // Console.WriteLine($"Press card: {Cards[i].Content}");
        
        // first need to add card to selected, then make font visible
        _selectedCards.Add(i);

        // if in practice mode, add a border instead of making font visible
        if (_practiceFlag)
        {
            // use hard coded value of thickness 5
            Cards[i].BorderThickness = Thickness.Parse("5");
        }
        else
        {
            Cards[i].FontSize = cardFont;
        }
        
        switch (_selectedCards.Count)
        {
            // continue if selected 2 cards
            case 2:
                break;
            
            default:
                return;
            // selected first card (or more), do nothing more
        }
        
        // add a check so double-clicking the same card does nothing
        if (_selectedCards[0] == _selectedCards[1])
        {
            _selectedCards.RemoveAt(1);
            return;
        }
        
        // second card selected

        // get index of selected cards
        var card1Index = _selectedCards[0];
        var card2Index = _selectedCards[1];
        
        // add delay
        FontSizeTransition.Delay = TimeSpan.FromSeconds(1);
        
        // if both cards selected are not a match, simply make font small again after a bit
        if (CardTexts[card1Index].Id != CardTexts[card2Index].Id)
        {
            // only make hidden not in practice mode
            if (!_practiceFlag)
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
            _pairsMade++;
            
            // and change background colour of pair
            // this feature was added so user can tell mid way or end of game what were the pairs they matched
            Cards[card1Index].Background = CardTexts[card1Index].MatchColour;
            Cards[card2Index].Background = CardTexts[card2Index].MatchColour;
            
            // issue found is that when disabling button above, the background colour can no longer be changed
            // stays gray
        }
        
        // need to revert adding of border
        if (_practiceFlag)
        {
            Cards[card1Index].BorderThickness = Thickness.Parse("0");
            Cards[card2Index].BorderThickness = Thickness.Parse("0");
        }
        
        // remove all cards from Selected (this includes a third or more card)
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
        if (_pairsMade < _numPairs)
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