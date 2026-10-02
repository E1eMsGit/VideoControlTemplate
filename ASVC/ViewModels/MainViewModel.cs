using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using ReactiveUI;

namespace ASVC.ViewModels;

public class MainViewModel : ViewModelBase
{
    private List<Uri> _camerasImages; // Потом выпилить.
    
    public Bitmap? CameraImage { get; set; } // Потом выпилить.
    public List<CameraControlBtn> CamerasControls { get; }
    public List<CameraControlBtn> CamerasBacklightControls { get; }
    public string? ActiveCameraName { get; private set; }
    
    public MainViewModel()
    {
        CamerasControls = new List<CameraControlBtn>()
        {
            new("ВК 1", "Выкл.") { IsEnabled = true },
            new("ВК 2", "Выкл.") { IsEnabled = true },
            new("ВК 3", "Выкл.") { IsEnabled = true },
            new("ВК 4", "Выкл.") { IsEnabled = true },
        };
        CamerasBacklightControls = new List<CameraControlBtn>()
        {
            new("ВК 1", "Выкл."),
            new("ВК 2", "Выкл."),
            new("ВК 3", "Выкл."),
            new("ВК 4", "Выкл."),
        };
        
        for (int i = 0; i < CamerasControls.Count; i++)
        {
            int j = i;
            CamerasControls[i].StatusChanged += delegate(bool status) { SetButtonsEnableStatus(j, status); };
        }

        // Потом выпилить.
        _camerasImages = new List<Uri>
        {
            new($"avares://ASVC/Assets/2.png"),
            new($"avares://ASVC/Assets/1.png"),
            new($"avares://ASVC/Assets/3.png"),
            new($"avares://ASVC/Assets/4.png"),
        };
    }

    private void SetButtonsEnableStatus(int ind, bool status)
    {
        for (int i = 0; i < CamerasControls.Count; i++)
        {
            if (i == ind)
            {
                ActiveCameraName = status ? CamerasControls[ind].Name : string.Empty;
                this.RaisePropertyChanged(nameof(ActiveCameraName));

                // Потом выпилить.
                CameraImage = status ? new Bitmap(AssetLoader.Open(_camerasImages[ind])) : null;
                this.RaisePropertyChanged(nameof(CameraImage));
                
                continue;
            }
            CamerasControls[i].IsEnabled = !status;
        }
        CamerasBacklightControls[ind].IsEnabled = status;
    }
}

public class CameraControlBtn : INotifyPropertyChanged
{
    private bool _isChecked;
    private bool _isEnabled;

    public string Name { get; }
    public string Status { get; private set; }
    public bool IsEnabled
    {
        get => _isEnabled;
        set
        {
            _isEnabled = value;
            OnPropertyChanged(nameof(IsEnabled));
        }
    }
    public bool IsChecked
    {
        get => _isChecked;
        set
        {
            _isChecked = value;
            Status = _isChecked ? "Вкл." : "Выкл.";
            OnPropertyChanged(nameof(Status));
            StatusChanged?.Invoke(_isChecked);
        }
    }

    public event Action<bool>? StatusChanged;
    public event PropertyChangedEventHandler? PropertyChanged;

    public CameraControlBtn(string name, string status)
    {
        Name = name;
        Status = status;
    }
    
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
