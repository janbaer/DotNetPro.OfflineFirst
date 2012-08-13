using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using DotNetPro.Offlinefirst.Common.Models;

using DotNetPro.Offlinefirst.Common.Stores;
using DotNetPro.Offlinefirst.Common.Infrastructure;

using DotNetPro.OfflineFirst.MetroApp.Common;
using DotNetPro.Offlinefirst.Common.Services;

namespace DotNetPro.OfflineFirst.MetroApp.ViewModels
{
    public class CustomersViewModel : NavigatableViewModel
    {
        private readonly ICustomerStore _customerStore;
        private readonly Observer<IEnumerable<Customer>> _customerObserver;
        private bool _loaded;

        public CustomersViewModel(GlobalViewModel globalViewModel,
                                  ICustomerStore customerStore, INavigationService navigationService)
            : base(navigationService)
        {
            this.GlobalViewModel = globalViewModel;

            _customerStore = customerStore;
            _customerObserver = new Observer<IEnumerable<Customer>>(null, null, OnNextCustomers);
            _customerStore.Subscribe(_customerObserver);

            this.Customers = new ObservableCollection<CustomerViewModel>();

            this.ShowOrdersCommand = new DelegateCommand(ShowOrders);
        }

        public DelegateCommand ShowOrdersCommand { get; set; }

        public ObservableCollection<CustomerViewModel> Customers { get; private set; }
        public GlobalViewModel GlobalViewModel { get; private set; }

        protected override void OnNavigatedTo(object parameter)
        {
            if (!_loaded)
            {
                LoadCustomersAsync();

                _loaded = true;
            }
        }

        private void OnNextCustomers(IEnumerable<Customer> customers)
        {
            Dispatch.Action(() => CreateOrUpdateViewModels(customers));
        }

        private async void LoadCustomersAsync()
        {
            GlobalViewModel.WebServerIsOffline = false;

            try
            {
                await _customerStore.LoadAsync();
            }
            catch (WebApiService.WebServerNotAvailableException)
            {
                GlobalViewModel.WebServerIsOffline = true;
            }
        }

        private void CreateOrUpdateViewModels(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                string customerId = customer.CustomerId;

                var viewModel = (from vm in this.Customers where vm.CustomerId == customerId select vm).FirstOrDefault();
                if (viewModel == null)
                {
                    viewModel = new CustomerViewModel() { Customer = customer };
                    this.Customers.Add(viewModel);
                }
                else
                {
                    viewModel.Customer = customer;
                }
            }
        }

        private void ShowOrders(object parameter)
        {
            this.NavigationService.NavigateTo<OrdersViewModel>(((CustomerViewModel)parameter).Customer);
        }

    }
}