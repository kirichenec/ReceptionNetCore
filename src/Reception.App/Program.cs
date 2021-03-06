﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Reception.App.Model.FileInfo;
using Reception.App.Model.PersonInfo;
using Reception.App.Network.Auth;
using Reception.App.Network.Chat;
using Reception.App.Network.Server;
using Reception.App.Service;
using Reception.App.Service.Interface;
using Reception.App.ViewModels;
using Reception.App.Views;
using Splat;
using System.Reflection;

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
            Locator.CurrentMutable.RegisterConstant(new SettingsService(), typeof(ISettingsService));
            Locator.CurrentMutable.RegisterConstant(new UserService(), typeof(IUserService));
            Locator.CurrentMutable.RegisterConstant(new PersonNetworkService(), typeof(INetworkService<Person>));
            Locator.CurrentMutable.RegisterConstant(new FileDataNetworkService(), typeof(INetworkService<FileData>));
            Locator.CurrentMutable.RegisterConstant(new PingService(), typeof(IPingService));
            Locator.CurrentMutable.RegisterConstant(new ClientService(), typeof(IClientService));

            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
        }

        public static void Main(string[] args)
        {
            BuildAvaloniaApp().Start(AppMain, args);
        }
    }
}