using System;

namespace DotNetPro.OfflineFirst.MetroApp.Infrastructure
{
    public interface INetworkStatus
    {
        bool IsOffline { get; }
        bool IsOnline { get; }
        event EventHandler<EventArgs> NetworkStatusChanged;
    }
}