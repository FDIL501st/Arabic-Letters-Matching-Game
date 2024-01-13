using ArabicLettersMatchingGame.Services;

namespace ArabicLettersMatchingGame.ViewModels;

public class EasyGameViewModel(MainMenuViewModel menuView) : GameViewModel(menuView, new EasyGetTextPairsStrategy())
{
    
    // add a timer updater?
    
    /// <summary>
    /// When click return button, change view to menu view
    /// </summary>
    public void ClickReturn()
    {
        MenuView.Window.ContentViewModel = MenuView;
    }
}