namespace DotNetPro.OfflineFirst.MetroApp.Common
{
    public class NavigationEntry
    {
        public NavigationEntry(NavigatableViewModel viewModel, object parameter)
        {
            ViewModel = viewModel;
            Parameter = parameter;
        }

        public NavigatableViewModel ViewModel { get; set; }
        public object Parameter { get; set; }
    }
}