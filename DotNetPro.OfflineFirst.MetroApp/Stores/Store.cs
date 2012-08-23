using System;
using System.Collections.Generic;
using DotNetPro.OfflineFirst.MetroApp.Infrastructure;
using DotNetPro.OfflineFirst.MetroApp.Models;
using DotNetPro.OfflineFirst.MetroApp.Services;
using DotNetPro.Offlinefirst.Common.Models;

namespace DotNetPro.OfflineFirst.MetroApp.Stores
{
    public abstract class Store<T>
    {
        private static readonly object lockObject = new object();

        private readonly Dictionary<int, IObserver<T>> _observers = new Dictionary<int, IObserver<T>>();
        private int _key;

        protected IOfflineStore OfflineStore { get; set; }
        protected IWebApiService WebApiService { get; set; }
        protected INetworkStatus NetworkStatus { get; set; }
        protected List<Type> KnownTypes { get; set; }

        protected Store(IOfflineStore offlineStore, IWebApiService webApiService, INetworkStatus networkStatus)
        {
            this.OfflineStore = offlineStore;
            this.WebApiService = webApiService;
            this.NetworkStatus = networkStatus;

            this.KnownTypes = new List<Type>() {typeof(Customer), typeof(Order)};
        }

        protected void OnNext(T items)
        {
            foreach (var observer in _observers.Values)
            {
                observer.OnNext(items);
            }
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (observer == null) throw new ArgumentNullException("observer");

            lock (lockObject)
            {
                int k = _key++;
                _observers.Add(k, observer);
                return new AnonymousDisposable(() =>
                {
                    lock (lockObject)
                    {
                        _observers.Remove(k);
                    }
                });
            }

        }

        private class AnonymousDisposable : IDisposable
        {
            readonly Action _dispose;

            public AnonymousDisposable(Action dispose)
            {
                this._dispose = dispose;
            }

            public void Dispose()
            {
                _dispose();
            }
        }
    }


}