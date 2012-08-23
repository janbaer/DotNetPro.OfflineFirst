using System.Threading.Tasks;

namespace DotNetPro.OfflineFirst.MetroApp.Stores
{
    public class ApplicationStore
    {
        private readonly ICustomerStore _customerStore;
        private readonly IOrderStore _orderStore;

        public ApplicationStore (   ICustomerStore customerStore, 
                                    IOrderStore orderStore)
        {
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
