using CommunityToolkit.Mvvm.ComponentModel;

namespace ba.config.ViewModels;

internal partial class CustomWebViewModel : ObservableObject
{
    [ObservableProperty]
    private string url;

    public CustomWebViewModel(string url)
    {
        Url = url;
    }
}