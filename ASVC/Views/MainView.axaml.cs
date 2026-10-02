using ASVC.ViewModels;
using Avalonia.Controls;

namespace ASVC.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}
