using System;

namespace DotNetPro.OfflineFirst.MetroApp.Common
{
    public interface INavigationService
    {
        event EventHandler CanGoBackChanged;
        bool CanGoBack { get; }
        void GoBack();
        void NavigateTo<TViewModel>(object parameter = null) where TViewModel : NavigatableViewModel;
    }
}