﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Logging.Serilog;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Reception.App.Model.FileInfo;
using Reception.App.Model.PersonInfo;
using Reception.App.Models;
using Reception.App.Network.Auth;
using Reception.App.Network.Chat;
using Reception.App.Network.Server;
using Reception.App.ViewModels;
using Reception.App.Views;
using Splat;

namespace Reception.App
{
    static class Program
    {
        private static void AppMain(Application app, string[] args)
        {
            var window =
                new MainWindow
                {
                    DataContext = new MainWindowViewModel()
                };

            app.Run(window);
        }

        public static AppBuilder BuildAvaloniaApp()
        {
            InitLocatorObjects();

            return
                AppBuilder
                    .Configure<App>()
                    .UsePlatformDetect()
                    .LogToTrace()
                    .UseReactiveUI();
        }

        private static void InitLocatorObjects()
        {
            Locator.CurrentMutable.RegisterConstant(new UserService(AppSettings.UserServerPath), typeof(IUserService));
            Locator.CurrentMutable.RegisterConstant(new NetworkService<Person>(AppSettings.DataServerPath), typeof(INetworkService<Person>));
            Locator.CurrentMutable.RegisterConstant(new NetworkService<FileData>(AppSettings.FileServerPath), typeof(INetworkService<FileData>));
            Locator.CurrentMutable.RegisterConstant(new PingService(AppSettings.DataServerPath), typeof(IPingService));
            //TODO: change 333 to normal userId
            Locator.CurrentMutable.Register(() => new ClientService(333, AppSettings.ChatServerPath, true), typeof(IClientService));

            Locator.CurrentMutable.Register(() => new AuthView(), typeof(IViewFor<AuthViewModel>));
            Locator.CurrentMutable.Register(() => new SubordinateView(), typeof(IViewFor<SubordinateViewModel>));
            Locator.CurrentMutable.Register(() => new BossView(), typeof(IViewFor<BossViewModel>));
        }

        public static void Main(string[] args)
        {
            BuildAvaloniaApp().Start(AppMain, args);
        }
    }
}