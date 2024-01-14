using System.Collections.Generic; 
using ArabicLettersMatchingGame.Models;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using static ArabicLettersMatchingGame.Models.Constants.GameBoardSizeNumber;

namespace ArabicLettersMatchingGame.Views.DataTemplates;

public static class DataTemplateProvider
{
    public static FuncDataTemplate<List<CardText>> EasyGameArea { get; }
        = new(_ => true,
        CreateEasyGrid);

    private static Grid CreateEasyGrid(List<CardText> cardTexts)
    {
        var gameGrid = new Grid();
        
        // Define the number of rows and columns based on the global constant GameMatchNumber.Easy
        for (var i = 0; i < Easy; i++)
        {
            gameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            gameGrid.RowDefinitions.Add(new RowDefinition());
        }

        // Populate the grid with TextBlocks bound to each CardText in cardTexts
        for (var i = 0; i < cardTexts.Count; i++)
        {
            var textBlock = new TextBlock();
            textBlock.Bind(TextBlock.TextProperty, new Binding(nameof(CardText.Text)) { Source = cardTexts[i] }); 
        
            Grid.SetColumn(textBlock, i % Easy);
            Grid.SetRow(textBlock, i / Easy);
            gameGrid.Children.Add(textBlock);
        }

        gameGrid.ShowGridLines = true;
        return gameGrid;
    }
}