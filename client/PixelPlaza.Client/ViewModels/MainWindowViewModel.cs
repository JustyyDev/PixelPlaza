using Avalonia.Controls;
using ReactiveUI;
using System.Reactive;
using System;

namespace PixelPlaza.Client.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        public string Status { get; set; } = "Not Connected";
        public string Username { get; set; } = "Guest";

        public ReactiveCommand<Unit, Unit> ConnectCommand { get; }
        public ReactiveCommand<Unit, Unit> ExitCommand { get; }
        public ReactiveCommand<Unit, Unit> AboutCommand { get; }
        public ReactiveCommand<Unit, Unit> ShowHomeCommand { get; }
        public ReactiveCommand<Unit, Unit> ShowPlazaCommand { get; }
        public ReactiveCommand<Unit, Unit> ShowMinigamesCommand { get; }
        public ReactiveCommand<Unit, Unit> ShowFriendsCommand { get; }
        public ReactiveCommand<Unit, Unit> OpenSettingsCommand { get; }

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set => this.RaiseAndSetIfChanged(ref _currentView, value);
        }

        public MainWindowViewModel()
        {
            // TODO: Initialize commands and views for navigation
            ConnectCommand = ReactiveCommand.Create(() => Status = "Connected!");
            ExitCommand = ReactiveCommand.Create(() => Environment.Exit(0));
            AboutCommand = ReactiveCommand.Create(() => {/* Show about dialog */});
            ShowHomeCommand = ReactiveCommand.Create(() => CurrentView = new HomeViewModel());
            ShowPlazaCommand = ReactiveCommand.Create(() => CurrentView = new PlazaViewModel());
            ShowMinigamesCommand = ReactiveCommand.Create(() => CurrentView = new MinigamesViewModel());
            ShowFriendsCommand = ReactiveCommand.Create(() => CurrentView = new FriendsViewModel());
            OpenSettingsCommand = ReactiveCommand.Create(() => CurrentView = new SettingsViewModel());

            // Set default view
            CurrentView = new HomeViewModel();
        }
    }
}