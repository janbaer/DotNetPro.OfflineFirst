using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DotNetPro.OfflineFirst.MetroApp.Models;
using DotNetPro.OfflineFirst.MetroApp.Services;
using DotNetPro.OfflineFirst.MetroApp.Stores;
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
        private bool _isLoading;

        public CustomersViewModel(GlobalViewModel globalViewModel,
                                  ICustomerStore customerStore, INavigationService navigationService) : base(navigationService)
        {
            this.GlobalViewModel = globalViewModel;

            _customerStore = customerStore;
            _customerObserver = new Observer<IEnumerable<Customer>>(null, null, OnNextCustomers);
            _customerStore.Subscribe(_customerObserver);

            this.Customers = new ObservableCollection<CustomerViewModel>();

            this.RefreshCommand = new DelegateCommand(Refresh);
            this.ShowOrdersCommand = new DelegateCommand(ShowOrders);
        }

        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand ShowOrdersCommand { get; set; }

        public ObservableCollection<CustomerViewModel> Customers { get; private set; }
        public GlobalViewModel GlobalViewModel { get; private set; }
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        protected override void OnNavigatedTo(object parameter)
        {
            if (this.Customers.Count == 0)
            {
                LoadCustomersAsync();
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
                this.IsLoading = true;

                await _customerStore.LoadAsync();
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

        private void Refresh(object parameter)
        {
           LoadCustomersAsync();
        }

        private void ShowOrders(object parameter)
        {
            this.NavigationService.NavigateTo<OrdersViewModel>(((CustomerViewModel)parameter).Customer.CustomerId);
        }

    }
}