using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace ba.config.Behaviors;

public class WebView2Behavior : Behavior<Microsoft.Web.WebView2.Wpf.WebView2>
{
    public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register(nameof(Source), typeof(string), typeof(WebView2Behavior), new PropertyMetadata(string.Empty));

    public string Source
    {
        get { return (string)GetValue(SourceProperty); }
        set { SetValue(SourceProperty, value); }
    }

    protected override void OnAttached()
    {
        base.OnAttached();

        AssociatedObject.Loaded += AssociatedObject_Loaded;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.Loaded -= AssociatedObject_Loaded;
    }

    private async void AssociatedObject_Loaded(object sender, RoutedEventArgs e) => await InitializeWebView();

    private async Task InitializeWebView()
    {
        await AssociatedObject.EnsureCoreWebView2Async();

        //AssociatedObject.CoreWebView2.SetVirtualHostNameToFolderMapping("vueapp", "Assets/wwwroot", CoreWebView2HostResourceAccessKind.Allow);
        //AssociatedObject.CoreWebView2.Navigate("https://vueapp/index.html");
        AssociatedObject.CoreWebView2.Navigate(Source);
        AssociatedObject.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
        AssociatedObject.CoreWebView2.Settings.AreBrowserAcceleratorKeysEnabled = false;

#if !DEBUG
        AssociatedObject.CoreWebView2.Settings.AreDevToolsEnabled = false;
#else
        AssociatedObject.CoreWebView2.OpenDevToolsWindow();
#endif
    }
}