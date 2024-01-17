using System.Collections.Generic;
using Avalonia.Controls.Templates;
using ArabicLettersMatchingGame.Models;
using Avalonia.Controls;
using static ArabicLettersMatchingGame.Models.Constants.GameBoardSizeNumber;

namespace ArabicLettersMatchingGame.Views.DataTemplates;

public class EasyGameAreaDataTemplate : GameAreaDataTemplate
{
    public EasyGameAreaDataTemplate()
    {
        GameArea = new FuncDataTemplate<List<CardText>>(_ => true, CreateGameArea);
    }

    public override FuncDataTemplate<List<CardText>> GameArea { get; }

    protected override Grid CreateGameArea(List<CardText> cardTexts)
    {
        return CreateGrid(cardTexts, Easy);
    }
}