using Prism.Navigation;

namespace ParallelSequentialDemo.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService) 
            : base (navigationService)
        {

        }
    }
}
