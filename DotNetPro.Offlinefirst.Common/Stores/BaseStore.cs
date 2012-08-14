using System;
using System.Collections.Generic;
using DotNetPro.Offlinefirst.Common.Models;
using DotNetPro.Offlinefirst.Common.Services;

namespace DotNetPro.Offlinefirst.Common.Stores
{
    public abstract class BaseStore<T>
    {
        private static readonly object ThisLock = new object();

        private readonly Dictionary<int, IObserver<T>> _observers = new Dictionary<int, IObserver<T>>();
        private int _key;

        protected IOfflineStore OfflineStore { get; set; }
        protected IWebApiService WebApiService { get; set; }
        protected List<Type> KnownTypes { get; set; }

        protected BaseStore(IOfflineStore offlineStore, IWebApiService webApiService)
        {
            this.OfflineStore = offlineStore;
            this.WebApiService = webApiService;

            this.KnownTypes = new List<Type>() {typeof(Customer), typeof(Employee)};
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

            lock (ThisLock)
            {
                int k = _key++;
                _observers.Add(k, observer);
                return new AnonymousDisposable(() =>
                {
                    lock (ThisLock)
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