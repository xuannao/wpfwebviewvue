using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ba.config.ViewModels
{
    internal partial class MainWindowViewModel : ObservableObject
    {
        private const int WM_NCHITTEST = 0x0084;
        private const int WM_NCLBUTTONDOWN = 0x00A1;
        private const int WM_NCLBUTTONUP = 0x00A2;
        private const int WM_NCLBUTTONDBLCLK = 0x00A3;
        private const int WM_GETMINMAXINFO = 0x0024;
        private const int WM_SYSCOMMAND = 0x0112;
        private const int HTMAXBUTTON = 9;

        private readonly Window _w = Application.Current.MainWindow;

        private Button? _maximizeRestore;

        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private Brush titleBarBackground = Brushes.White;

        [ObservableProperty]
        private Brush titleBarButtonBackground = Brushes.Black;

        [ObservableProperty]
        private Brush titleBarForground = Brushes.LightGray;

        [ObservableProperty]
        private ImageSource titleIcon;

        public MainWindowViewModel()
        {
            Title = "ba";
            string filePath = Path.Combine(Environment.CurrentDirectory, "Assets/icon.png");
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(filePath, UriKind.Absolute);
            bitmap.EndInit();
            bitmap.Freeze();
            TitleIcon = bitmap;

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

        [RelayCommand]
        private void SourceInit(Button b)
        {
            _maximizeRestore = b;
            if (IsSnapLayoutEnabled())
            {
                var source = (HwndSource)PresentationSource.FromVisual(_w);
                source.AddHook(WndProc);
            }
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
        /// 检测Windows版本
        /// </summary>
        /// <returns></returns>
        private static bool IsWindows11()
        {
            return Environment.OSVersion.Version.Major == 10 &&
                Environment.OSVersion.Version.Minor == 0 &&
                Environment.OSVersion.Version.Build >= 22000;
        }
        /// <summary>
        /// 使用Windows11窗口样式
        /// </summary>
        /// <returns></returns>
        private static bool IsSnapLayoutEnabled()
        {
            if (!IsWindows11())
            {
                return false;
            }

            using RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced");
            object? registryValueObject = key?.GetValue("EnableSnapAssistFlyout");

            if (registryValueObject == null)
            {
                return true;
            }

            int registryValue = (int)registryValueObject;

            return registryValue > 0;
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
        /// 窗口检测
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="handled"></param>
        /// <returns></returns>
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (_maximizeRestore == null) return IntPtr.Zero;

            switch (msg)
            {
                case WM_NCHITTEST:

                    // Return HTMAXBUTTON when the mouse is over the maximize/restore button
                    var point = _w.PointFromScreen(new Point(lParam.ToInt32() & 0xFFFF, lParam.ToInt32() >> 16));
                    if (_maximizeRestore.TransformToAncestor(_w).TransformBounds(new Rect(_maximizeRestore.RenderSize)).Contains(point))
                    {
                        handled = true;
                        _maximizeRestore.Background = TitleBarButtonBackground;
                        return new IntPtr(HTMAXBUTTON);
                    }
                    else
                    {
                        _maximizeRestore.Background = TitleBarBackground;
                    }
                    break;

                case WM_NCLBUTTONDOWN:
                    if (wParam.ToInt32() == HTMAXBUTTON)
                    {
                        handled = true;
                        _maximizeRestore.Background = TitleBarBackground;
                    }
                    break;

                case WM_NCLBUTTONUP:
                    if (wParam.ToInt32() == HTMAXBUTTON)
                    {
                        _maximizeRestore.Background = TitleBarBackground;
                        ToggleWindowState(_w);
                    }
                    break;
            }
            return IntPtr.Zero;
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
            if (_maximizeRestore != null)
                _maximizeRestore.Background = TitleBarBackground;
        }
    }
}