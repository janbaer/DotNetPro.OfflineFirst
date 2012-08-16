using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetPro.Offlinefirst.Common.Models;

namespace DotNetPro.Offlinefirst.Common.Services
{
    public interface IWebApiService
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<IEnumerable<Order>> GetOrdersForCustomerAsync(string customerId);
    }
}