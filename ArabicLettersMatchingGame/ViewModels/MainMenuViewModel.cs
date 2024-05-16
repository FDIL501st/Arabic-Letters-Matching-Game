namespace ArabicLettersMatchingGame.ViewModels;

/**
 * Logic for MainMenu of game. Has the buttons
 */
public class MainMenuViewModel(MainWindowViewModel window) : ViewModelBase
{
    // store a public variable for reference to MainWindow
    // this allows for changing the view easier
    // readonly so window reference is not changed
    public readonly MainWindowViewModel Window = window;

    /*
     * When easy button is clicked, change view to Easy Game.
     */
    public void ClickEasy()
    {
        Window.ContentViewModel = new EasyGameViewModel(this);
    }

    public void ClickEasyPractice()
    {
        Window.ContentViewModel = new EasyGameViewModel(this, true);
    }

    public void ClickHard()
    {
        Window.ContentViewModel = new HardGameViewModel(this);
    }

    public void ClickHardPractice()
    {
        Window.ContentViewModel = new HardGameViewModel(this, true);
    }
}