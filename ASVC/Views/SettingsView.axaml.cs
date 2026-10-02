using ASVC.ViewModels;
using Avalonia.Controls;

namespace ASVC.Views
{
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            DataContext = new SettingsViewModel();
        }
    }
}
