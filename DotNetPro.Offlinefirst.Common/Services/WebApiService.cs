using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DotNetPro.Offlinefirst.Common.Models;
using Newtonsoft.Json;

namespace DotNetPro.Offlinefirst.Common.Services
{

    public class WebApiService : IWebApiService
    {
        public WebApiService(string baseAddress)
        {
            this.BaseAddress = baseAddress;
        }

        public string BaseAddress { get; set; }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await DownloadData<IEnumerable<Customer>>("/customers") ?? new List<Customer>();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await DownloadData<IEnumerable<Employee>>("/employees") ?? new List<Employee>();
        }
        
        public async Task<IEnumerable<Order>> GetOrdersForCustomerAsync(string customerId)
        {
            return await DownloadData<IEnumerable<Order>>(string.Format("customers/{0}/orders", customerId)) ?? new List<Order>();
        }

        private async Task<T> DownloadData<T>(string url) where T:IEnumerable<object>
        {
            var httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(this.BaseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (HttpRequestException)
            {
                throw new WebServerNotAvailableException(string.Format("The WebService '{0}' is not available!", this.BaseAddress));
            }
            catch (Exception)
            {
                
            }

            return default(T);
        }

        public class WebServerNotAvailableException : Exception
        {
            public WebServerNotAvailableException(string message) : base(message)
            {
            }
        }
    }
}
