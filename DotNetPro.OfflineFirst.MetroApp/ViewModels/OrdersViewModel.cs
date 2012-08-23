using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using DotNetPro.OfflineFirst.MetroApp.Common;
using DotNetPro.OfflineFirst.MetroApp.Infrastructure;
using DotNetPro.OfflineFirst.MetroApp.Services;
using DotNetPro.OfflineFirst.MetroApp.Stores;
using DotNetPro.Offlinefirst.Common.Models;

namespace DotNetPro.OfflineFirst.MetroApp.ViewModels
{
    public class OrdersViewModel : BindableBase
    {
        private readonly IOrderStore _orderStore;
        private readonly Observer<IEnumerable<Order>> _orderStoreObserver;
        private string _customerId;
        private bool _isLoading;

        public OrdersViewModel( GlobalViewModel globalViewModel, IOrderStore orderStore)
        {
            this.GlobalViewModel = globalViewModel;
            this.Orders = new ObservableCollection<OrderViewModel>();

            _orderStore = orderStore;
            _orderStoreObserver = new Observer<IEnumerable<Order>>(null, null, OnNextOrders);
            _orderStore.Subscribe(_orderStoreObserver);

            this.RefreshCommand = new DelegateCommand(Refresh);
        }

        public DelegateCommand RefreshCommand { get; set; }

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

        public void LoadOrders(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                throw new ArgumentException("customerId can not be empty!");
            }

            if (this.CustomerId != customerId)
            {
                this.CustomerId = customerId;

                LoadOrdersAsync(customerId);
            }           
        }

        private void OnNextOrders(IEnumerable<Order> orders)
        {
            var query = from order in orders where order.CustomerID == this.CustomerId select order;

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
                var viewModel = App.Container.Resolve<OrderViewModel>();
                viewModel.Order = order;
                this.Orders.Add(viewModel);
            }
        }

        private void Refresh(object parameter)
        {
            LoadOrdersAsync(this.CustomerId);
        }

    }
}