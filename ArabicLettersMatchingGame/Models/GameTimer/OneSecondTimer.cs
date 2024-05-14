using System;
using System.Timers;

namespace ArabicLettersMatchingGame.Models.GameTimer;



/// <summary>
/// A 1 second timer to be used in rounds.
/// </summary>
public class OneSecondTimer: IDisposable
{
    private readonly Timer _timer;

    /// <summary>
    /// A timer that triggers an event every 1s. Does not start the timer.
    /// </summary>
    /// <param name="timerElapsed">The event handler of what to do every 1s</param>
    public OneSecondTimer(ElapsedEventHandler timerElapsed)
    {
        // start a timer that triggers an event every 1s
        _timer = new Timer(1000); // 1000 milliseconds = 1 second
        _timer.Elapsed += timerElapsed; // Attach the event handler
        _timer.AutoReset = true; // Make the timer keep triggering an event (not just occur once)
    }
    
    public void Dispose()
    {
        _timer.Stop();
        _timer.Dispose();
    }

    public void StartTimer()
    {
        _timer.Start();
    }
}