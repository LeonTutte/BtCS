using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BtCS_Desktop.ViewModels;

public partial class SimpleDialogPromptWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _title;
    [ObservableProperty]
    private string _message;
    public bool Answer;

    [RelayCommand]
    void Accept() => Answer = true;

    [RelayCommand]
    void Refuse() => Answer = false;
    }