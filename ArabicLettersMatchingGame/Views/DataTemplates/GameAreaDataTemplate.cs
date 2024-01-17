using System.Collections.Generic; 
using ArabicLettersMatchingGame.Models;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Layout;
using static ArabicLettersMatchingGame.Models.Constants.GameBoardSizeNumber;

namespace ArabicLettersMatchingGame.Views.DataTemplates;

public abstract class GameAreaDataTemplate
{
    public abstract FuncDataTemplate<List<CardText>> GameArea { get; }
    
    protected abstract Grid CreateGameArea(List<CardText> cardTexts);
    
    protected Grid CreateGrid(IReadOnlyList<CardText> cardTexts, int numSide)
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