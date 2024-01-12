using System;
using ArabicLettersMatchingGame.Views;
using ReactiveUI;

namespace ArabicLettersMatchingGame.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    
    private ViewModelBase _contentViewModel;
    
    // property used to access current viewmodel, thus the view being used/shown
    public ViewModelBase ContentViewModel
    {
        get => _contentViewModel;
        set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
        // cause a notification to be generated every time the property changes value (ContentViewModel)
    }
    
    // constructor, sets up starting view to be main menu
    public MainWindowViewModel()
    {
        _contentViewModel = new MainMenuViewModel(this);
    }
}