using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DotNetPro.Offlinefirst.Common.Models;

namespace DotNetPro.Offlinefirst.Common.Stores
{
    public interface IEmployeeStore : IObservable<IEnumerable<Employee>> 
    {
        Task LoadAsync();
        Task SaveAsync();
        void Clear();
    }
}