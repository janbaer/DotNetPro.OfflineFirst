using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DotNetPro.Offlinefirst.Common.Models;

namespace DotNetPro.Offlinefirst.Common.Stores
{
    public interface IOrderStore : IObservable<IEnumerable<Order>>
    {
        Task LoadAsync(string customerId);
        Task SaveAsync();
        void Clear();
    }
}