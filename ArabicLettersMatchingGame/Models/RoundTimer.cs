using System;

namespace ArabicLettersMatchingGame.Models;

/// <summary>
/// Represents the timer of how long a round has lasted.
/// Only a class to override the toString method.
/// Doesn't actually run a timer or update itself.
/// </summary>
public sealed class RoundTimer
{
    /// <summary>
    /// The value of the timer. Is updated every second.
    /// </summary>
    private readonly TimeSpan _roundLength = TimeSpan.Zero;

    public TimeSpan RoundLength => _roundLength;

    public RoundTimer(TimeSpan? roundLength = null)
    {
        if (roundLength is not null)
        {
            _roundLength = roundLength.Value;
        }
    }
    
    /// <summary>
    /// One second TimeSpan. Used within the class to add 1s to the roundLength.
    /// </summary>
    private static readonly TimeSpan OneSecond = TimeSpan.FromSeconds(1); 
    
    public override string ToString()
    {
        // if less than 1 hour passed, show minutes and seconds
        // else show hours, minutes and seconds
        return _roundLength.ToString(_roundLength.Hours == 0 ? @"mm\:ss" : @"hh\:mm\:ss");
    }
    
    /// <summary>
    /// Returns a new RoundTimer with 1s plus the current one. Current one is unchanged.
    /// </summary>
    public RoundTimer AddOneSecondToTimer()
    {
        try
        {
            return new RoundTimer(_roundLength.Add(OneSecond));
        }
        catch (OverflowException)
        {
            // can occur when round lasts for more than 1 day
            // for now will just reset timer back to 0
            return new RoundTimer(TimeSpan.Zero);

            // maybe in future can deal with tracking days in timer as well?
            // probably not going to happen
        }
    }
    
}