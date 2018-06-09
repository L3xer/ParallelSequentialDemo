using Prism.Mvvm;
using Prism.Navigation;

namespace ParallelSequentialDemo.ViewModels
{
    public class ViewModelBase : BindableBase
    {
        protected INavigationService NavigationService { get; private set; }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }
    }
}
