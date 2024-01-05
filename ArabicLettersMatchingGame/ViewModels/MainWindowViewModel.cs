using System;
using ReactiveUI;

namespace ArabicLettersMatchingGame.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";
    
    // initially show MainMenuView
    private ViewModelBase _contentViewModel = new MainMenuViewModel();
    
    // property used to access current viewmodel, thus view being used/shown
    public ViewModelBase ContentViewModel
    {
        get => _contentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
        // cause a notification to be generated every time the property changes value (ContentViewModel)
    }
}