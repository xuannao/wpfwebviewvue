using ba.config.ViewModels;
using ba.config.Views;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Net;
using System.Windows;

namespace ba.config;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    internal static string RootPath = AppDomain.CurrentDomain.BaseDirectory;

    private static WebApplication? AppHost;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        CheckIfProcessAlreadyRunning();

        int port = FindAvailablePort();
        if (port == -1)
        {
            //MessageBox.Show("端口号已用完，请关闭其他程序后重试！");
            Current.Shutdown();
        }
        string url = $"http://localhost:{port}/";
        Debug.WriteLine(url);

        var builder = WebApplication.CreateBuilder(new WebApplicationOptions() { WebRootPath = "Assets/wwwroot/" });
        builder.WebHost.UseUrls(url);

        builder.Services

            .AddTransient<CustomWebViewModel>(s => new CustomWebViewModel(url))

            .AddSingleton<MainWindowViewModel>()
            .AddSingleton(s => new MainWindow() { DataContext = s.GetRequiredService<MainWindowViewModel>() })

            .AddControllers();

        AppHost = builder.Build();

        AppHost.UseStaticFiles();
        AppHost.MapControllers();
        AppHost.MapFallbackToFile("/index.html");

        GetRequiredService<MainWindow>().Show();
        AppHost.RunAsync().ConfigureAwait(true);
    }

    private static void CheckIfProcessAlreadyRunning()
    {
        Process process = Process.GetCurrentProcess();
        var isRunning = Process.GetProcessesByName(process.ProcessName).Any(p => p.Id != process.Id);
        if (isRunning)
        {
            //MessageBox.Show($"程序已经运行，请勿同时打开多个程序！{process.ProcessName}");
            Current.Shutdown();
        }
    }

    private static int FindAvailablePort(int startPort = 6078)
    {
        int port = startPort;
        while (port <= 9000)
        {
            try
            {
                using HttpListener listener = new();
                listener.Prefixes.Add($"http://localhost:{port}/");
                listener.Start();
                listener.Stop();
                return port; // 返回可用端口
            }
            catch (HttpListenerException)
            {
                port++;
            }
        }
        return -1; // 如果没有可用端口，返回 -1
    }

    internal static T GetRequiredService<T>() where T : class
    {
        return AppHost!.Services.GetRequiredService<T>();
    }
}