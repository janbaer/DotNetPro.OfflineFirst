using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetPro.Offlinefirst.Common.Infrastructure;
using DotNetPro.Offlinefirst.Common.Models;
using DotNetPro.Offlinefirst.Common.Services;

namespace DotNetPro.Offlinefirst.Common.Stores
{
    public class CustomerStore : BaseStore<IEnumerable<Customer>>, ICustomerStore
    {
        private List<Customer> _customers;

        public CustomerStore(IOfflineStore offlineStore, IWebApiService webApiService, INetworkStatus networkStatus) : base(offlineStore, webApiService, networkStatus)
        {

        }

        public void Clear()
        {
            _customers.Clear();
        }

        public async Task LoadAsync()
        {
            if (_customers == null)
            {
                _customers = await this.OfflineStore.LoadAsync<List<Customer>>("customers", this.KnownTypes);
                this.OnNext(_customers);
            }

            if (this.NetworkStatus.IsOnline)
            {
                var customers = new List<Customer>(await this.WebApiService.GetCustomersAsync());
                this.OnNext(customers);

                _customers.Clear();
                _customers.AddRange(customers);
            }        
        }

        public async Task SaveAsync()
        {
            if (_customers == null) return;

            await this.OfflineStore.SaveAsync("customers", _customers, this.KnownTypes);  
        }
        
    }


}