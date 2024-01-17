using System.Collections.Generic; 
using ArabicLettersMatchingGame.Models;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Layout;
using DynamicData;
using static ArabicLettersMatchingGame.Models.Constants.GameBoardSizeNumber;

namespace ArabicLettersMatchingGame.Views.DataTemplates;

public class DataTemplateProvider
{
    public DataTemplateProvider()
    {
        EasyGameArea = new FuncDataTemplate<List<CardText>>(_ => true, CreateEasyGrid);
    }

    public FuncDataTemplate<List<CardText>> EasyGameArea { get; }

    private Grid CreateEasyGrid(List<CardText> cardTexts)
    {
        return CreateGrid(cardTexts, Easy);
    }
    
    private Grid CreateGrid(IReadOnlyList<CardText> cardTexts, int numSide)
    {
        var gameGrid = new Grid();
        
        // Define the number of rows and columns based on the global constant GameMatchNumber.Easy
        for (var i = 0; i < numSide; i++)
        {
            gameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            gameGrid.RowDefinitions.Add(new RowDefinition());
        }

        // Populate the grid with TextBlocks bound to each CardText in cardTexts
        for (var i = 0; i < cardTexts.Count; i++)
        {
            var card = new Button()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center, 
            };
            card.Bind(ContentControl.ContentProperty, new Binding(nameof(CardText.Text)) { Source = cardTexts[i] });
            ;
            
            Grid.SetColumn(card, i % numSide);
            Grid.SetRow(card, i / numSide);
            gameGrid.Children.Add(card);
        }

        gameGrid.ShowGridLines = true;
        return gameGrid;
    }
}