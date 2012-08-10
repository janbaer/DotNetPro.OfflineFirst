using System;
using DotNetPro.OfflineFirst.MetroApp.Common;
using DotNetPro.OfflineFirst.MetroApp.Stores;
using DotNetPro.Offlinefirst.Common.Services;
using DotNetPro.OfflineFirst.MetroApp.Services;
using DotNetPro.OfflineFirst.MetroApp.ViewModels;
using DotNetPro.Offlinefirst.Common.Stores;

using MetroIoc;

namespace DotNetPro.OfflineFirst.MetroApp
{
    public class MetroAppContainer : MetroContainer, IResolver
    {
        public MetroAppContainer()
        {
            const string WEB_API_BASE_ADDRESS = "http://northwind-1.apphb.com";

            RegisterInstance(typeof (IResolver), this);
            Register<IViewResolver, MetroAppViewResolver>(null, new SingletonLifecycle());
            Register<INavigationService, NavigationService>(null, new SingletonLifecycle());

            RegisterInstance(typeof (IWebApiService), new WebApiService(WEB_API_BASE_ADDRESS));
            Register<IOfflineStore, OfflineStore>(null, new SingletonLifecycle());

            Register<IEmployeeStore, EmployeeStore>(null, new SingletonLifecycle());
            Register<ICustomerStore, CustomerStore>(null, new SingletonLifecycle());
            Register<IOrderStore, OrderStore>(null, new SingletonLifecycle());

            Register<GlobalViewModel, GlobalViewModel>(null, new SingletonLifecycle());

            Register<ApplicationStore, ApplicationStore>(null, new SingletonLifecycle());

            Register<CustomersViewModel, CustomersViewModel>(null, new SingletonLifecycle());
            Register<OrdersViewModel, OrdersViewModel>(null, new SingletonLifecycle());
        }

        public T Resolve<T>()
        {
            return base.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return base.Resolve(type, null);
        }
    }
}
