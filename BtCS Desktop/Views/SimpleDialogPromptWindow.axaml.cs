using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BtCS_Desktop.ViewModels;

namespace BtCS_Desktop.Views;

public partial class SimpleDialogPromptWindow : Window
{
    public SimpleDialogPromptWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}