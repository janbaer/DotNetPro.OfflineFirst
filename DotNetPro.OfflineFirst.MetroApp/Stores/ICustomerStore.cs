using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetPro.OfflineFirst.MetroApp.Models;

namespace DotNetPro.OfflineFirst.MetroApp.Stores
{
    public interface ICustomerStore : IObservable<IEnumerable<Customer>>
    {
        Task LoadAsync();
        Task SaveAsync();
        void Clear();
    }
}