using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualBasic;

namespace ArabicLettersMatchingGame.Services;

/// <summary>
/// Provides simple rng for the program.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public static class RNG
{
    // get a seed using current millisecond

    public static Random Rng { get; } = new(DateAndTime.Now.Millisecond);
}