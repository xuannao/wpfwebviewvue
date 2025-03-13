using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ba.config.ViewModels;

internal partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private string title = string.Empty;

    [ObservableProperty]
    private string titleSecond = string.Empty;

    [ObservableProperty]
    private Brush titleBarBackground = Brushes.White;

    [ObservableProperty]
    private Brush titleBarButtonBackground = Brushes.Black;

    [ObservableProperty]
    private Brush titleBarForground = Brushes.LightGray;

    [ObservableProperty]
    private ImageSource titleIcon;

    [ObservableProperty]
    private CustomWebViewModel webViewModel;

    public MainWindowViewModel(CustomWebViewModel webViewModel)
    {
        Title = "ba";
        TitleSecond = "2";
        string filePath = Path.Combine(App.RootPath, "Assets/icon.png");
        var bitmap = new BitmapImage();
        bitmap.BeginInit();
        bitmap.UriSource = new Uri(filePath, UriKind.Absolute);
        bitmap.EndInit();
        bitmap.Freeze();
        TitleIcon = bitmap;

        WebViewModel = webViewModel;

        SetWindowTheme();
        SystemEvents.UserPreferenceChanged += WindowsThemeChanged;
    }

    /// <summary>
    /// 最小化
    /// </summary>
    /// <param name="w"></param>
    [RelayCommand]
    private void Minimize(Window w)
    {
        SystemCommands.MinimizeWindow(w);
    }

    /// <summary>
    /// 切换窗口大小
    /// </summary>
    /// <param name="w"></param>
    [RelayCommand]
    private void Maximize(Window w)
    {
        ToggleWindowState(w);
    }

    /// <summary>
    /// 关闭窗口
    /// </summary>
    /// <param name="w"></param>
    [RelayCommand]
    private void Close(Window w)
    {
        SystemCommands.CloseWindow(w);
    }

    /// <summary>
    /// 获取Windows主题
    /// </summary>
    /// <returns></returns>
    private static bool GetWindowsTheme()
    {
        using RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
        var t = key?.GetValue("AppsUseLightTheme");
        if (t is int v)
        {
            return v == 0;
        }
        return false;
    }

    /// <summary>
    /// 设置窗口样式
    /// </summary>
    private void SetWindowTheme()
    {
        if (GetWindowsTheme())
        {
            TitleBarBackground = (Brush)(new BrushConverter().ConvertFromString("#18181C")!);
            TitleBarForground = Brushes.White;
            TitleBarButtonBackground = Brushes.Gray;
        }
        else
        {
            TitleBarBackground = Brushes.White;
            TitleBarForground = Brushes.Black;
            TitleBarButtonBackground = Brushes.LightGray;
        }
    }

    /// <summary>
    /// 切换窗口大小
    /// </summary>
    /// <param name="w"></param>
    private static void ToggleWindowState(Window w)
    {
        if (w.WindowState == WindowState.Maximized)
        {
            SystemCommands.RestoreWindow(w);
        }
        else
        {
            SystemCommands.MaximizeWindow(w);
        }
    }

    /// <summary>
    /// 检测主题切换
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void WindowsThemeChanged(object sender, UserPreferenceChangedEventArgs e)
    {
        if (e.Category != UserPreferenceCategory.Color) return;
        SetWindowTheme();
    }
}