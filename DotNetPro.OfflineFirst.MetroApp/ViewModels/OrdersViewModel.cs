using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using DotNetPro.OfflineFirst.MetroApp.Common;
using DotNetPro.OfflineFirst.MetroApp.Services;
using DotNetPro.Offlinefirst.Common.Models;
using DotNetPro.Offlinefirst.Common.Stores;
using DotNetPro.Offlinefirst.Common.Infrastructure;
using DotNetPro.Offlinefirst.Common.Services;

namespace DotNetPro.OfflineFirst.MetroApp.ViewModels
{
    public class OrdersViewModel : NavigatableViewModel
    {
        private readonly IOrderStore _orderStore;
        private readonly Observer<IEnumerable<Order>> _orderStoreObserver;
        private string _customerId;
        private bool _isLoading;

        public OrdersViewModel( GlobalViewModel globalViewModel, IOrderStore orderStore,
                                INavigationService navigationService) : base(navigationService)
        {
            this.GlobalViewModel = globalViewModel;
            this.Orders = new ObservableCollection<OrderViewModel>();

            _orderStore = orderStore;
            _orderStoreObserver = new Observer<IEnumerable<Order>>(null, null, OnNextOrders);
            _orderStore.Subscribe(_orderStoreObserver);

            this.RefreshCommand = new DelegateCommand(Refresh);
            this.ShowOrderDetailsCommand = new DelegateCommand(ShowOrderDetails);
        }

        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand ShowOrderDetailsCommand { get; set; }

        #region Properties
        public string CustomerId
        {
            get { return _customerId; }
            private set { SetProperty(ref _customerId, value); }
        }
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }
        public GlobalViewModel GlobalViewModel { get; private set; }
        public ObservableCollection<OrderViewModel> Orders { get; private set; }
        #endregion


        protected override void OnNavigatedTo(object parameter)
        {
            var customerId = parameter as string;

            if (string.IsNullOrEmpty(customerId)) return;

            if (this.CustomerId != customerId)
            {
                this.CustomerId = customerId;

                LoadOrdersAsync(customerId);
            }
        }

        private void OnNextOrders(IEnumerable<Order> orders)
        {
            var query = from o in orders where o.CustomerID == this.CustomerId select o;

            Dispatch.Action(() => CreateOrUpdateViewModels(query));
        }

        private async void LoadOrdersAsync(string customerId)
        {
            GlobalViewModel.WebServerIsOffline = false;
            this.IsLoading = true;

            try
            {
                await _orderStore.LoadAsync(customerId);
            }
            catch (WebApiService.WebServerNotAvailableException)
            {
                GlobalViewModel.WebServerIsOffline = true;
            }
            finally
            {
                this.IsLoading = false;
            }
        }

        private void CreateOrUpdateViewModels(IEnumerable<Order> orders)
        {
            this.Orders.Clear();

            foreach (var order in orders)
            {
                var viewModel = new OrderViewModel() { Order = order };
                this.Orders.Add(viewModel);
            }
        }

        private void Refresh(object parameter)
        {
            LoadOrdersAsync(this.CustomerId);
        }

        private void ShowOrderDetails(object parameter)
        {

        }
    }
}