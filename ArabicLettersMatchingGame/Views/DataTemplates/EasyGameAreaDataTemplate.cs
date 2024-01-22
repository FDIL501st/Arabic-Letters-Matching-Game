using System.Collections.Generic;
using System.Reactive;
using Avalonia.Controls.Templates;
using ArabicLettersMatchingGame.Models;
using ArabicLettersMatchingGame.Models.Constants;
using Avalonia.Controls;
using ReactiveUI;

using static ArabicLettersMatchingGame.Models.Constants.GameBoardSizeNumber;

namespace ArabicLettersMatchingGame.Views.DataTemplates;

public class EasyGameAreaDataTemplate : GameAreaDataTemplate
{
    public EasyGameAreaDataTemplate(ReactiveCommand<Unit, Unit> buttonCommand) : base(buttonCommand)
    {
        GameArea = new FuncDataTemplate<List<CardText>>(_ => true, CreateGameArea);
    }

    public override FuncDataTemplate<List<CardText>> GameArea { get; }

    protected override Grid CreateGameArea(List<CardText> cardTexts)
    {
        var easyGameArea = CreateGrid(cardTexts, Easy);

        foreach (var child in easyGameArea.Children)
        {
            // change font size of each button
            if (child is Button button)
            {
                button.FontSize = CardFontSize.Easy;
            }
        }
        
        return easyGameArea;
    }
}