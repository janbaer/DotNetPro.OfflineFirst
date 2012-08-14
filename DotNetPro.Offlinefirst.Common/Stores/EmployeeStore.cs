using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DotNetPro.Offlinefirst.Common.Models;
using DotNetPro.Offlinefirst.Common.Services;

namespace DotNetPro.Offlinefirst.Common.Stores
{
    public class EmployeeStore : BaseStore<IEnumerable<Employee>>, IEmployeeStore
    {
        private List<Employee> _employees;
 
        public EmployeeStore(IOfflineStore offlineStore, IWebApiService webApiService) : base(offlineStore, webApiService)
        {
        }

        public void Clear()
        {
            if (_employees == null) return; 

            _employees.Clear();
        }

        public async Task LoadAsync()
        {
            if (_employees == null)
            {
                _employees = await this.OfflineStore.LoadAsync<List<Employee>>("employees", this.KnownTypes);
                this.OnNext(_employees);
            }

            var employees = new List<Employee>(await this.WebApiService.GetEmployeesAsync());
            this.OnNext(employees);

            _employees.Clear();
            _employees.AddRange(employees);
        }

        public async Task SaveAsync()
        {
            if (_employees == null) return;

            await this.OfflineStore.SaveAsync("employees", _employees, this.KnownTypes);
        }
    }
}
