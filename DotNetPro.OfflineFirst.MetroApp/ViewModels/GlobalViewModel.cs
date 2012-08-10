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

        public bool IsOffline
        {
            get { return _isOffline; }
            set { SetProperty(ref _isOffline, value); }
        }
    }
}
