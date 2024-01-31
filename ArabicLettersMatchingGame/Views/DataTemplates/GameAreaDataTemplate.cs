using System.Collections.Generic;
using System.Reactive;
using ArabicLettersMatchingGame.Models;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Layout;
using ReactiveUI;

namespace ArabicLettersMatchingGame.Views.DataTemplates;

public abstract class GameAreaDataTemplate(List<Button> buttons, ReactiveCommand<int, Unit> buttonCommand)
{
    public abstract FuncDataTemplate<List<CardText>> GameArea { get; }
    
    // holds all buttons, initialized in child class as number of buttons not known
    public List<Button> Buttons { get; } = buttons;
    
    // holds the command for the buttons for each card
    private ReactiveCommand<int, Unit> ButtonCommands { get; } = buttonCommand;


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
            // var card = new Button()
            // {
            //     HorizontalAlignment = HorizontalAlignment.Stretch,
            //     VerticalAlignment = VerticalAlignment.Stretch,
            //     HorizontalContentAlignment = HorizontalAlignment.Center,
            //     VerticalContentAlignment = VerticalAlignment.Center, 
            //     Margin = Thickness.Parse("7"),
            //     Command = ButtonCommands,
            //     CommandParameter = i
            // };
            var card = Buttons[i];
            card.Command = ButtonCommands;
            card.CommandParameter = i;
            card.Bind(ContentControl.ContentProperty, new Binding(nameof(CardText.Text)) { Source = cardTexts[i] });
            
            
            Grid.SetColumn(card, i % numSide);
            Grid.SetRow(card, i / numSide);
            gameGrid.Children.Add(card);
        }
        
        return gameGrid;
    }
    
    // factory function to create a generic button for use within the GameAreaDataTemplate
    public static Button CreateButton(string marginThickness, int fontSize)
    {
        return new Button()
        {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
            HorizontalContentAlignment = HorizontalAlignment.Center,
            VerticalContentAlignment = VerticalAlignment.Center,
            Margin = Thickness.Parse(marginThickness),
            FontSize = fontSize
        };
    }
}