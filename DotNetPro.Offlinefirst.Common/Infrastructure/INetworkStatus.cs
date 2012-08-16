using System;

namespace DotNetPro.Offlinefirst.Common.Infrastructure
{
    public interface INetworkStatus
    {
        bool IsOffline { get; }
        bool IsOnline { get; }
        event EventHandler<EventArgs> NetworkStatusChanged;
    }
}