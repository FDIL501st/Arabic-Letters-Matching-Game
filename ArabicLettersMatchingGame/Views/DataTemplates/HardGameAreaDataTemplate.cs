using System.Collections.Generic;
using System.Reactive;
using ArabicLettersMatchingGame.Models;
using ArabicLettersMatchingGame.Models.Constants;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using ReactiveUI;

namespace ArabicLettersMatchingGame.Views.DataTemplates;

public class HardGameAreaDataTemplate: GameAreaDataTemplate
{
    public HardGameAreaDataTemplate(List<Button> buttons, ReactiveCommand<int, Unit> buttonCommand) : base(buttons, buttonCommand)
    {
        GameArea = new FuncDataTemplate<List<CardText>>(_ => true, CreateGameArea);
    }

    public sealed override FuncDataTemplate<List<CardText>> GameArea { get; init; }

    protected override Grid CreateGameArea(List<CardText> cardTexts)
    {
        return CreateGrid(cardTexts, GameBoardSizeNumber.Hard);
    }
}