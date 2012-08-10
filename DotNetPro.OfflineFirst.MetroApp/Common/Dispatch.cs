using System;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;


namespace DotNetPro.OfflineFirst.MetroApp.Common
{
    /// <summary>
    ///   Provides helper methods for dispatching method calls between
    ///   different threads.
    /// </summary>
    public static class Dispatch
    {
        #region Properties

        /// <summary>
        ///   Sets or Returns the instance of the Dispatcher that runs on the 
        ///   main (UI) thread of the application.
        /// </summary>
        public static CoreDispatcher MainDispatcher { get; set; }

        #endregion

        #region Public Methods

        public static void Action(Action action)
        {
            if (MainDispatcher.HasThreadAccess)
            {
                action.Invoke();
            }
            else
            {
                MainDispatcher.RunAsync(CoreDispatcherPriority.Normal, action.Invoke);
            }    
        }
        
        #endregion
    }
}
