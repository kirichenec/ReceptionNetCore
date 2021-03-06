﻿using Reception.Model.Interface;

namespace Reception.App.Service.Interface
{
    public interface ISettingsService
    {
        string ChatServerPath { get; }
        string DataServerPath { get; }
        string FileServerPath { get; }
        bool IsBoss { get; }
        int PingDelay { get; }
        IToken Token { get; }
        string UserServerPath { get; }
        string WelcomeMessage { get; }
        bool WithReconnect { get; }
    }
}