using System.Collections.Generic;
using System.Reactive;
using Avalonia.Controls.Templates;
using ArabicLettersMatchingGame.Models;
using ArabicLettersMatchingGame.Models.Constants;
using Avalonia.Controls;
using ReactiveUI;


namespace ArabicLettersMatchingGame.Views.DataTemplates;

public class EasyGameAreaDataTemplate : GameAreaDataTemplate
{
    public EasyGameAreaDataTemplate(List<Button> buttons, ReactiveCommand<int, Unit> buttonCommand) : base(buttons, buttonCommand)
    {
        GameArea = new FuncDataTemplate<List<CardText>>(_ => true, CreateGameArea);
    }

    public override FuncDataTemplate<List<CardText>> GameArea { get; }

    protected override Grid CreateGameArea(List<CardText> cardTexts)
    {
        return  CreateGrid(cardTexts, GameBoardSizeNumber.Easy);
    }
}