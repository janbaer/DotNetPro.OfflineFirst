using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DotNetPro.Offlinefirst.Common.Models;
using DotNetPro.Offlinefirst.Common.Services;

namespace DotNetPro.Offlinefirst.Common.Stores
{
    public class OrderStore : BaseStore<IEnumerable<Order>>, IOrderStore
    {
        private List<Order> _orders;
 
        public OrderStore(IOfflineStore offlineStore, IWebApiService webApiService) : base(offlineStore, webApiService)
        {
        }

        public void Clear()
        {
            if (_orders == null) return;

            _orders.Clear();
        }

        public async Task LoadAsync(string customerId)
        {
            if (_orders == null)
            {
                this._orders = await this.OfflineStore.LoadAsync<List<Order>>("orders", this.KnownTypes);
            }
            this.OnNext(from o in this._orders where o.CustomerID == customerId select o);

            var orders = await this.WebApiService.GetOrdersForCustomerAsync(customerId);

            this.OnNext(orders);

            _orders.RemoveAll(o => o.CustomerID == customerId);
            _orders.AddRange(orders);
        }

        public async Task SaveAsync()
        {
            if (_orders == null) return;

            await this.OfflineStore.SaveAsync("orders", _orders, this.KnownTypes);
        }
    }
}