using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using DotNetPro.OfflineFirst.MetroApp.Common;
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
        private Customer _customer;

        public OrdersViewModel(GlobalViewModel globalViewModel, IOrderStore orderStore,
                                INavigationService navigationService)
            : base(navigationService)
        {
            this.GlobalViewModel = globalViewModel;
            this.Orders = new ObservableCollection<OrderViewModel>();

            _orderStore = orderStore;
            _orderStoreObserver = new Observer<IEnumerable<Order>>(null, null, OnNextOrders);
            _orderStore.Subscribe(_orderStoreObserver);

            ShowOrderDetailsCommand = new DelegateCommand(ShowOrderDetails);
        }

        public DelegateCommand ShowOrderDetailsCommand { get; set; }

        public Customer Customer
        {
            get { return _customer; }
            private set { SetProperty(ref _customer, value); }
        }
        public ObservableCollection<OrderViewModel> Orders { get; private set; }
        public GlobalViewModel GlobalViewModel { get; private set; }

        protected override void OnNavigatedTo(object parameter)
        {
            var customer = (Customer)parameter;

            if (this.Customer == null || this.Customer.CustomerId != customer.CustomerId)
            {
                this.Customer = (Customer)parameter;

                LoadOrdersAsync(this.Customer.CustomerId);
            }
        }

        private void OnNextOrders(IEnumerable<Order> orders)
        {
            var query = from o in orders where o.CustomerID == this.Customer.CustomerId select o;

            Dispatch.Action(() => CreateOrUpdateViewModels(query));
        }

        private async void LoadOrdersAsync(string customerId)
        {
            GlobalViewModel.WebServerIsOffline = false;

            try
            {
                await _orderStore.LoadAsync(customerId);
            }
            catch (WebApiService.WebServerNotAvailableException)
            {
                GlobalViewModel.WebServerIsOffline = true;
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

        private void ShowOrderDetails(object parameter)
        {
            var order = (Order)parameter;

            //            NavigationService.NavigateTo<OrderDetailsViewModel>(order);
        }
    }
}