using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DotNetPro.OfflineFirst.MetroApp.Models;
using DotNetPro.Offlinefirst.Common.Models;
using Newtonsoft.Json;

namespace DotNetPro.OfflineFirst.MetroApp.Services
{
    public class WebApiService : IWebApiService
    {
        private const string BaseAddress = "http://northwind-1.apphb.com/";
        private const string CustomersResourceUrl = "customers";
        private const string OrdersResourceUrl = "customers/{0}/orders";

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await DownloadData<IEnumerable<Customer>>(CustomersResourceUrl) ?? new List<Customer>();
        }

        public async Task<IEnumerable<Order>> GetOrdersForCustomerAsync(string customerId)
        {
            return await DownloadData<IEnumerable<Order>>(string.Format(OrdersResourceUrl, customerId)) ?? new List<Order>();
        }

        private async Task<T> DownloadData<T>(string url) where T:IEnumerable<object>
        {
            var httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(WebApiService.BaseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (HttpRequestException exception)
            {
                throw new WebServerNotAvailableException(string.Format("Der WebServer '{0}' ist nicht erreichbar!", WebApiService.BaseAddress), exception);
            }
            catch (Exception)
            {
                // ???  
            }

            return default(T);
        }

        public class WebServerNotAvailableException : Exception
        {
            public WebServerNotAvailableException(string message, Exception innerException) : base(message, innerException)
            {
            }
        }
    }
}
