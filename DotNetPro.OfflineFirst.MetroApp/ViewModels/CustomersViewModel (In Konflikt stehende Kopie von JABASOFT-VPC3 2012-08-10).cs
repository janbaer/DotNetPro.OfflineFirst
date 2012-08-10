using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using DotNetPro.Offlinefirst.Common.Models;
using DotNetPro.OfflineFirst.MetroApp.Common;
using DotNetPro.Offlinefirst.Common.Stores;
using DotNetPro.Offlinefirst.Common.Infrastructure;


namespace DotNetPro.OfflineFirst.MetroApp.ViewModels
{
    public class CustomersViewModel : NavigatableViewModel
    {
        private readonly ICustomerStore _customerStore;
        private readonly Observer<IEnumerable<Customer>> _customerObserver; 
        private bool _loaded;

        public CustomersViewModel(GlobalViewModel globalViewModel,
                                  ICustomerStore customerStore, INavigationService navigationService) : base(navigationService)
        {
            this.GlobalViewModel = globalViewModel;

            _customerStore = customerStore;
            _customerObserver = new Observer<IEnumerable<Customer>>(null, null, OnNextCustomers);
            _customerStore.Subscribe(_customerObserver);

            this.Customers = new ObservableCollection<Customer>();

            this.ShowOrdersCommand = new DelegateCommand(ShowOrders);
        }

        public DelegateCommand ShowOrdersCommand { get; set; }

        public ObservableCollection<Customer> Customers { get; private set; }
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
            Dispatch.Action(()=> CreateOrUpdateViewModels(customers));
        }

        private async void LoadCustomersAsync()
        {
            await _customerStore.LoadAsync();
        }

        private void CreateOrUpdateViewModels(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                string id = customer.CustomerId;

                if (this.Customers.Any(c => c.CustomerId == id) == false)
                {
                    this.Customers.Add(customer);
                }
            }            
        }

        private void ShowOrders(object parameter)
        {
            this.NavigationService.NavigateTo<OrdersViewModel>((Customer)parameter);
        }

    }
}