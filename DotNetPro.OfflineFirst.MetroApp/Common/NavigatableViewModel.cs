using System;
using System.Windows.Input;

namespace DotNetPro.OfflineFirst.MetroApp.Common
{
    public abstract class NavigatableViewModel : BindableBase
    {
        private ICommand _goBackCommand;
        private ICommand _goHomeCommand;

        protected NavigatableViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        protected INavigationService NavigationService { get; private set; }

        public ICommand GoBackCommand
        {
            get { return _goBackCommand ?? (_goBackCommand = new NavigationCommand(GoBack, NavigationService)); }
        }

        public ICommand GoHomeCommand
        {
            get { return _goHomeCommand ?? (_goHomeCommand = new NavigationCommand(GoHome, NavigationService)); }
        }

        private void GoBack(object parameter)
        {
            NavigationService.GoBack();
        }

        private void GoHome(object paramter)
        {
            while (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        public void Navigate(object parameter)
        {
            OnNavigatedTo(parameter);
        }

        protected virtual void OnNavigatedTo(object parameter)
        {
        }

        private class NavigationCommand : DelegateCommand
        {
            private readonly INavigationService _navigationService;

            public NavigationCommand(Action<object> action, INavigationService navigationService)
                : base(action)
            {
                _navigationService = navigationService;
                _navigationService.CanGoBackChanged += EventService.MakeWeak(OnCanGoBackChanged, h => _navigationService.CanGoBackChanged -= h);
            }

            public override bool CanExecute(object parameter)
            {
                return _navigationService.CanGoBack;
            }

            private void OnCanGoBackChanged(object sender, EventArgs e)
            {
                RaiseCanExecuteChanged();
            }
        }
    }
}
