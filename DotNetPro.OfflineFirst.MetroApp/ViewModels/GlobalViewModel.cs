using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DotNetPro.OfflineFirst.MetroApp.Common;

namespace DotNetPro.OfflineFirst.MetroApp.ViewModels
{
    public class GlobalViewModel : BindableBase
    {
        private bool _isOffline;
        private bool _webServerIsOffline;

        public bool IsOffline
        {
            get { return _isOffline; }
            set { SetProperty(ref _isOffline, value); }
        }

        public bool WebServerIsOffline
        {
            get { return _webServerIsOffline; }
            set { SetProperty(ref _webServerIsOffline, value); }
        }
    }
}
