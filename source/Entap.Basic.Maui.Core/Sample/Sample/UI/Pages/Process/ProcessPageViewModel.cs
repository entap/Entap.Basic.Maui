using System;
using Entap.Basic.Maui.Core;

namespace Sample
{
    public class ProcessPageViewModel : PageViewModelBase
    {
        public ProcessPageViewModel()
        {
            ProcessCanExecuteCommand = new(DelayAsync, () => CanExecute);
        }

        public ProcessCommand ProcessCommand => new (DelayAsync);

        public ProcessCommand<string> ProcessTCommand => new((arg) => DelayAsync(arg));

        public ProcessCommand ProcessCanExecuteCommand { get; set; }
        public bool CanExecute
        {
            get => _canExecute;
            set
            {
                if (SetProperty(ref _canExecute, value))
                    ProcessCanExecuteCommand?.ChangeCanExecute();
            }
        }
        bool _canExecute;

        public ProcessCommand ExceptionProcessCommand => new(ThrowExceotionAsync);

        async Task DelayAsync()
        {
            System.Diagnostics.Debug.WriteLine($"{nameof(DelayAsync)} : {DateTime.Now.ToString("HH:mm:ss.fff")}");
            await Task.Delay(2000);
        }

        async Task DelayAsync(string arg)
        {
            System.Diagnostics.Debug.WriteLine($"{nameof(DelayAsync)} : {DateTime.Now.ToString("HH:mm:ss.fff")}  arg:{arg}");
            await Task.Delay(2000);
        }

        async Task ThrowExceotionAsync()
        {
            System.Diagnostics.Debug.WriteLine($"{nameof(ThrowExceotionAsync)} : {DateTime.Now.ToString("HH:mm:ss.fff")}");
            await Task.Delay(10);
            throw new Exception("Test");
        }
        
    }
}

