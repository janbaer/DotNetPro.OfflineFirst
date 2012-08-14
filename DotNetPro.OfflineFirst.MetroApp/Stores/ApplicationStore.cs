using System.Threading.Tasks;

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

        public async Task SaveAsync()
        {
            await _customerStore.SaveAsync();
            await _employeeStore.SaveAsync();
            await _orderStore.SaveAsync();
        }

        public void Clear()
        {
            _customerStore.Clear();
            _employeeStore.Clear();
            _orderStore.Clear();
        }

    }
}
