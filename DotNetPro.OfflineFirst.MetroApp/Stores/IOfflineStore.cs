using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetPro.Offlinefirst.Common.Services
{
    public interface IOfflineStore
    {
        Task SaveAsync<T>(string key, T objectToSerialize, List<Type> knownTypes);
        Task<T> LoadAsync<T>(string key, List<Type> knownTypes) where T : new();
    }
}