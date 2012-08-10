using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DotNetPro.Offlinefirst.Common.Models;

namespace DotNetPro.Offlinefirst.Common.Stores
{
    public interface ICustomerStore : IObservable<IEnumerable<Customer>>
    {
        Task LoadAsync();
        Task SaveAsync();
        void Clear();
    }
}