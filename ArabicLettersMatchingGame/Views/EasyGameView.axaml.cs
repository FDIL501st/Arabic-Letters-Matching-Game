using ArabicLettersMatchingGame.Models.Constants;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ArabicLettersMatchingGame.Views;

public partial class EasyGameView : UserControl
{
    public EasyGameView()
    {
        InitializeComponent();
        
        // load markup file
        AvaloniaXamlLoader.Load(this);
        
        // get grid so can update it 
        var grid = EasyGameGrid;

        // make grid for game
        // will be a square, with each side have number of easy game matches number of cards/grid spaces
        
        // each grid space is a card

        Grid gameArea = new();
        
        for (var i = 0; i < GameMatchNumber.Easy; i++)
        {
            // each grid space in game area is same size
            gameArea.RowDefinitions.Add(new RowDefinition(GridLength.Star));
            gameArea.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
        }

        gameArea.ShowGridLines = true;

#if false
        // test data to see if gameArea works
        TextBlock text1 = new();
        text1.Text = "Text 1";
        
        gameArea.Children.Add(text1);
        Grid.SetRow(text1,1);
        Grid.SetColumn(text1,3);
        
#endif
        
        // add gameArea to grid and set to row 1
        grid.Children.Add(gameArea);
        Grid.SetRow(gameArea, 1);
        
        
        // set content of view to grid, this will render the updates we added in code here
        Content = grid;

    }
}