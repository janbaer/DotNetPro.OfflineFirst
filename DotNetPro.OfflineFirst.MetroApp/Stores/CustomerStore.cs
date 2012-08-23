using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetPro.OfflineFirst.MetroApp.Infrastructure;
using DotNetPro.OfflineFirst.MetroApp.Models;
using DotNetPro.OfflineFirst.MetroApp.Services;

namespace DotNetPro.OfflineFirst.MetroApp.Stores
{
    public class CustomerStore : Store<IEnumerable<Customer>>, ICustomerStore
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