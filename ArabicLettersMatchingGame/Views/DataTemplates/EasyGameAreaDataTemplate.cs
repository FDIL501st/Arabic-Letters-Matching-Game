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
    public EasyGameAreaDataTemplate(ReactiveCommand<int, Unit> buttonCommand) : base(buttonCommand)
    {
        // initialize buttons
        Buttons = new List<Button>(GameBoardSizeNumber.Easy*GameBoardSizeNumber.Easy);

        for (var i = 0; i < GameBoardSizeNumber.Easy*GameBoardSizeNumber.Easy; i++)
        {
            // create easy button looks (no binding done here)
            // binding done in CreateGrid function in parent class
            Buttons.Add(CreateButton("7", CardFontSize.Easy));
        }
        
        GameArea = new FuncDataTemplate<List<CardText>>(_ => true, CreateGameArea);
    }

    public sealed override List<Button> Buttons { get; init; }

    public override FuncDataTemplate<List<CardText>> GameArea { get; }

    protected override Grid CreateGameArea(List<CardText> cardTexts)
    {
        return  CreateGrid(cardTexts, GameBoardSizeNumber.Easy);
    }
}