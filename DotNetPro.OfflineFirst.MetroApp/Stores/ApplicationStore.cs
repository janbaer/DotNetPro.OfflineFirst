using System.Threading.Tasks;

using DotNetPro.Offlinefirst.Common.Services;
using DotNetPro.Offlinefirst.Common.Stores;

namespace DotNetPro.OfflineFirst.MetroApp.Stores
{
    public class ApplicationStore
    {
        private readonly IOfflineStore _offlineStore;
        private readonly ICustomerStore _customerStore;
        private readonly IOrderStore _orderStore;

        public ApplicationStore (   IOfflineStore offlineStore, 
                                    ICustomerStore customerStore, 
                                    IOrderStore orderStore)
        {
            _offlineStore = offlineStore;
            _customerStore = customerStore;
            _orderStore = orderStore;
        }

        public async Task SaveAsync()
        {
            await _customerStore.SaveAsync();
            await _orderStore.SaveAsync();
        }

        public void Clear()
        {
            _customerStore.Clear();
            _orderStore.Clear();
        }

    }
}
