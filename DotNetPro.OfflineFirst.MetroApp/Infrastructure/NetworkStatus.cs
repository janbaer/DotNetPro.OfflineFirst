using System;
using DotNetPro.Offlinefirst.Common.Infrastructure;
using Windows.Networking.Connectivity;

namespace DotNetPro.OfflineFirst.MetroApp.Infrastructure
{
    public class NetworkStatus : INetworkStatus
    {
        public NetworkStatus()
        {
            NetworkInformation.NetworkStatusChanged += NetworkInformationOnNetworkStatusChanged;
        }

        public bool IsOffline
        {
            get { return IsOnline == false; }
        }

        public bool IsOnline
        {
            get
            {
                return (NetworkInformation.GetInternetConnectionProfile() != null &&
                        NetworkInformation.GetInternetConnectionProfile().GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);
            }
        }
        public event EventHandler<EventArgs> NetworkStatusChanged;


        private void NetworkInformationOnNetworkStatusChanged(object sender)
        {
            var theEvent = this.NetworkStatusChanged;

            if (theEvent != null)
            {
                theEvent(this, new EventArgs());
            }
        }
    }
}
