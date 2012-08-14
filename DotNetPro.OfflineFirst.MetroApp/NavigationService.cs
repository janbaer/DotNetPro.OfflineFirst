using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using DotNetPro.OfflineFirst.MetroApp.Common;

namespace DotNetPro.OfflineFirst.MetroApp
{
    public class NavigationService : INavigationService
    {
        private readonly Stack<NavigationEntry> _navigationParameters = new Stack<NavigationEntry>();
        private readonly IResolver _resolver;
        private readonly IViewResolver _viewResolver;

        protected NavigationService(IResolver resolver, IViewResolver viewResolver)
        {
            _resolver = resolver;
            _viewResolver = viewResolver;
            Frame.Navigated += OnFrameNavigated;        
        }

        public event EventHandler CanGoBackChanged;

        public bool CanGoBack
        {
            get { return Frame.CanGoBack && _navigationParameters.Count > 0; }
        }

        public void GoBack()
        {
            Frame.GoBack();
            OnCanGoBackChanged(EventArgs.Empty);

            _navigationParameters.Pop();
            var navigationEntry = _navigationParameters.Peek();

            if (navigationEntry != null && navigationEntry.ViewModel != null)
            {
                var viewModel = navigationEntry.ViewModel;

                viewModel.Navigate(navigationEntry.Parameter);
            }
        }

        public void NavigateTo<TViewModel>(object parameter) where TViewModel : NavigatableViewModel
        {
            // Get the matching view for this view model
            Type id = _viewResolver.ResolveView(typeof(TViewModel));

            // Call the platform-specific override
            NavigateTo(id, parameter);
            var viewModel = (NavigatableViewModel)_resolver.Resolve<TViewModel>();
            viewModel.Navigate(parameter);

            _navigationParameters.Push(new NavigationEntry(viewModel, parameter));

            OnCanGoBackChanged(EventArgs.Empty);
        }

        protected void Bind(Type id, IView view)
        {
            Type viewModelType = _viewResolver.ResolveViewModel(view.GetType());

            if (viewModelType != null)
            {
                var viewModel = (NavigatableViewModel)_resolver.Resolve(viewModelType);
                view.Bind(viewModel);
            }
        }
        public Frame Frame
        {
            get { return (Frame)Window.Current.Content; }
        }

        protected void NavigateTo(Type viewType, object parameter)
        {
            Frame.Navigate(viewType, parameter);
        }

        private void OnFrameNavigated(object sender, NavigationEventArgs e)
        {
            Bind(e.SourcePageType, (IView)e.Content);
        }

        protected virtual void OnCanGoBackChanged(EventArgs e)
        {
            var handler = CanGoBackChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
