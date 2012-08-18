using DotNetPro.OfflineFirst.MetroApp.Infrastructure;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Networking.Connectivity;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using MetroIoc;

using DotNetPro.Offlinefirst.Common.Infrastructure;

using DotNetPro.OfflineFirst.MetroApp.Common;
using DotNetPro.OfflineFirst.MetroApp.Stores;
using DotNetPro.OfflineFirst.MetroApp.ViewModels;

// The Grid App template is documented at http://go.microsoft.com/fwlink/?LinkId=234226

namespace DotNetPro.OfflineFirst.MetroApp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        private INetworkStatus _networkStatus;
        /// <summary>
        /// Initializes the singleton Application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        public static IContainer Container { get; set; }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
 
            // Do not repeat app initialization when already running, just ensure that
            // the window is active
            if (args.PreviousExecutionState == ApplicationExecutionState.Running)
            {
                Window.Current.Activate();
                return;
            }

            // Create a Frame to act as the navigation context and associate it with
            // a SuspensionManager key
            var rootFrame = new Frame();
            SuspensionManager.RegisterFrame(rootFrame, "AppFrame");

            // Place the frame in the current Window and ensure that it is active
            Window.Current.Content = rootFrame;
            Window.Current.Activate();

            Dispatch.MainDispatcher = Window.Current.Dispatcher;

            Container = new MetroAppContainer();

            var navigationService = Container.Resolve<INavigationService>();
            navigationService.NavigateTo<CustomersViewModel>();

            if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
                // Restore the saved session state only when appropriate
//                await SuspensionManager.RestoreAsync();
            }

            _networkStatus = App.Container.Resolve<INetworkStatus>();
            _networkStatus.NetworkStatusChanged += (sender, eventArgs) => CheckInternetConnection();
            CheckInternetConnection();
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            await SuspensionManager.SaveAsync();

            await Container.Resolve<ApplicationStore>().SaveAsync();

            deferral.Complete();
        }

        private void CheckInternetConnection()
        {
            CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Container.Resolve<GlobalViewModel>().IsOffline = _networkStatus.IsOffline);
        }
    }
}
