using System;
using ArabicLettersMatchingGame.Views;
using ReactiveUI;

namespace ArabicLettersMatchingGame.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    
    // initially show MainMenuView
    private ViewModelBase _contentViewModel = new MainMenuViewModel();
    
    // property used to access current viewmodel, thus the view being used/shown
    public ViewModelBase ContentViewModel
    {
        get => _contentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
        // cause a notification to be generated every time the property changes value (ContentViewModel)
    }
    
    // constructor, for now set inital page to easy game for testing that page
    public MainWindowViewModel()
    {
        ContentViewModel = new EasyGameViewModel();
    }
}