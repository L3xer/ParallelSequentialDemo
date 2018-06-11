using System;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace ParallelSequentialDemo.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public DelegateCommand ParallelCommand { get; set; }
        public DelegateCommand SequentialCommand { get; set; }

        private IPageDialogService _dialogService;

        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            _dialogService = dialogService;

            ParallelCommand = new DelegateCommand(ParallelExecute);
            SequentialCommand = new DelegateCommand(SequentialExecute);
        }

        private void SequentialExecute()
        {
            _dialogService.DisplayAlertAsync("Test", "Sequential clicked", "OK");
        }

        private void ParallelExecute()
        {
            _dialogService.DisplayAlertAsync("Test", "Paralell clicked", "OK");
        }
    }
}
