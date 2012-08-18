using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetPro.OfflineFirst.MetroApp.Models;
using DotNetPro.Offlinefirst.Common.Models;

namespace DotNetPro.OfflineFirst.MetroApp.Services
{
    public interface IWebApiService
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<IEnumerable<Order>> GetOrdersForCustomerAsync(string customerId);
    }
}