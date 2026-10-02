using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using ReactiveUI;
using System.Reactive;
using ASVC.Views;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;


namespace ASVC.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private bool _isPaneOpen;
        private UserControl _currentPage;
        private MenuItemTemplate? _selectedListItem;

        public bool IsPaneOpen
        {
            get => _isPaneOpen;
            set => this.RaiseAndSetIfChanged(ref _isPaneOpen, value);

        }
        public UserControl CurrentPage
        {
            get => _currentPage;
            set => this.RaiseAndSetIfChanged(ref _currentPage, value);
        }
        public MenuItemTemplate? SelectedListItem
        {
            get => _selectedListItem;
            set
            {
                CurrentPage = value.Content;
                this.RaiseAndSetIfChanged(ref _selectedListItem, value);
            }
        }
        public List<MenuItemTemplate> Items { get; }

        public ReactiveCommand<Unit, Unit> TriggerPaneCommand => ReactiveCommand.Create(() =>
        {
            IsPaneOpen = !IsPaneOpen; });


        public MainWindowViewModel()
        {
            Items = new List<MenuItemTemplate>()
            {
                new("Главная", new MainView(), new Uri($"avares://ASVC/Assets/home.png")),
                new("Настройки", new SettingsView(), new Uri($"avares://ASVC/Assets/settings.png")),
            };

            SelectedListItem = Items[0];
        }
    }

    public class MenuItemTemplate
    {
        public MenuItemTemplate(string name, UserControl content, Uri iconUri)
        {
            Name = name;
            Content = content;
            Icon = new Bitmap(AssetLoader.Open(iconUri));
        }

        public string Name { get; }
        public UserControl Content { get; }
        public Bitmap Icon { get; }
    }
}
