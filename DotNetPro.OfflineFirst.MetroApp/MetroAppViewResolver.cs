using System;

using ControllerRT;

namespace DotNetPro.OfflineFirst.MetroApp
{
    public interface IResolver
    {
        object Resolve(Type type);

        T Resolve<T>();
    }

    public interface IViewResolver
    {
        Type ResolveView<TViewModel>();

        Type ResolveView(Type viewModelType);

        Type ResolveViewModel<TView>();

        Type ResolveViewModel(Type viewType);
    }

    public class MetroAppViewResolver : IViewResolver
    {
        public Type ResolveView<TViewModel>()
        {
            return ResolveView(typeof(TViewModel));
        }

        public Type ResolveView(Type viewModelType)
        {
            string viewModelName = viewModelType.Name;
            string viewNamespace = viewModelType.Namespace.Replace("ViewModels", "Views");

            viewModelName = viewModelName.TrimStart('I');

            string viewName = viewModelName.Replace("ViewModel", "Page");
            viewName = viewNamespace + "." + viewName;

            return Type.GetType(viewName);
        }

        public Type ResolveViewModel<TView>()
        {
            return ResolveViewModel(typeof(TView));
        }

        public Type ResolveViewModel(Type viewType)
        {
            string viewName = viewType.Name;
            string viewModelNamespace = viewType.Namespace.Replace("Views", "ViewModels");

            viewName = viewName.TrimStart('I');

            string viewModelName = viewName.Replace("Page", "ViewModel");
            viewModelName = viewModelNamespace + "." + viewModelName;

            return Type.GetType(viewModelName);
        }
    }
}