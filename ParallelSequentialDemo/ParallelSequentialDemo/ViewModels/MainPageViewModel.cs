using System.Net.Http;
using System.Diagnostics;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Services;
using Prism.Navigation;


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

        private async void SequentialExecute()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var client = new HttpClient() { MaxResponseContentBufferSize = 1000000 };

            var length1 = await ProcessURLAsync("http://msdn.microsoft.com", client);
            var length2 = await ProcessURLAsync("http://msdn.microsoft.com/library/hh156528(VS.110).aspx", client);
            var length3 = await ProcessURLAsync("http://msdn.microsoft.com/library/67w7t67f.aspx", client);

            int total = length1 + length2 + length3;

            stopwatch.Stop();

            await _dialogService.DisplayAlertAsync("Test", $"Total bytes returned: {total}\nTime elapsed: {stopwatch.ElapsedMilliseconds}", "OK");
        }

        private async void ParallelExecute()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var client = new HttpClient() { MaxResponseContentBufferSize = 1000000 };

            var download1 = ProcessURLAsync("http://msdn.microsoft.com", client);
            var download2 = ProcessURLAsync("http://msdn.microsoft.com/library/hh156528(VS.110).aspx", client);
            var download3 = ProcessURLAsync("http://msdn.microsoft.com/library/67w7t67f.aspx", client);

            int length1 = await download1;
            int length2 = await download2;
            int length3 = await download3;

            int total = length1 + length2 + length3;

            stopwatch.Stop();

            await _dialogService.DisplayAlertAsync("Test", $"Total bytes returned: {total}\nTime elapsed: {stopwatch.ElapsedMilliseconds}", "OK");
        }

        private async Task<int> ProcessURLAsync(string url, HttpClient client)
        {
            var byteArray = await client.GetByteArrayAsync(url);
            return byteArray.Length;
        }

        //----------------------
        // Results (milliseconds)
        //----------------------
        // Parallel  Sequential
        // 5920      4788
        // 5758      6920
        // 4830      5712
        // 3891      4935
        // 3886      4730
        // 4512      5685
        // 4036      5726
        // 4196      5199
        // 3899      6982
        // 4017      5708
        // 4321      5472
        //---------------------
        // Parallel Min: 3886 
        // Parallel Max: 5920 
        // Parallel Mean: 4478 
        //---------------------
        // Sequential Min: 4730 
        // Sequential Max: 6982 
        // Sequential Mean: 5623 
        //---------------------
    }
}
