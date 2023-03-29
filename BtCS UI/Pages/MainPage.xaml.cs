using BtCS_UI.ViewModel;

namespace BtCS_UI.Pages;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        LoginBtn.IsEnabled = true;
        ExitBtn.IsEnabled = true;
    }
}