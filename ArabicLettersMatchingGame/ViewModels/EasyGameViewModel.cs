namespace ArabicLettersMatchingGame.ViewModels;

public class EasyGameViewModel(MainMenuViewModel menuView) : ViewModelBase
{
    // menuView in constructor used within class for return button

    /*
     * When press return button, change view back to menu.
     */
    public void ClickReturn()
    {
        menuView.Window.ContentViewModel = menuView;
    }
}