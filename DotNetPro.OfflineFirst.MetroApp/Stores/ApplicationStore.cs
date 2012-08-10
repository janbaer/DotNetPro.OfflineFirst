using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DotNetPro.Offlinefirst.Common.Infrastructure;
using DotNetPro.Offlinefirst.Common.Models;
using DotNetPro.Offlinefirst.Common.Services;
using DotNetPro.Offlinefirst.Common.Stores;

namespace DotNetPro.OfflineFirst.MetroApp.Stores
{
    public class ApplicationStore
    {
        private readonly IOfflineStore _offlineStore;
        private readonly ICustomerStore _customerStore;
        private readonly IEmployeeStore _employeeStore;
        private readonly IOrderStore _orderStore;

        public ApplicationStore (   IOfflineStore offlineStore, 
                                    ICustomerStore customerStore, 
                                    IEmployeeStore employeeStore, 
                                    IOrderStore orderStore)
        {
            _offlineStore = offlineStore;
            _customerStore = customerStore;
            _employeeStore = employeeStore;
            _orderStore = orderStore;
        }

        public async Task LoadAsync()
        {
        }

        public async Task SaveAsync()
        {
            _customerStore.SaveAsync();
            _employeeStore.SaveAsync();
            _orderStore.SaveAsync();
        }

        public void Clear()
        {
            _customerStore.Clear();
            _employeeStore.Clear();
            _orderStore.Clear();
        }

    }
}
