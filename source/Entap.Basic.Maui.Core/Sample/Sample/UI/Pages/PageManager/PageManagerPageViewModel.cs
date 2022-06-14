using Entap.Basic.Maui.Core;

namespace Sample
{
    public class PageManagerPageViewModel : PageViewModelBase
    {
        public PageManagerPageViewModel()
        {
        }

        public ProcessCommand PushCommand => PushCommand<PageManagerPage>(new PageManagerPageViewModel());

        public ProcessCommand PushModalCommand => PushModalCommand<PageManagerPage>(new PageManagerPageViewModel());

        public ProcessCommand PushNavigationModalCommand => PushNavigationModalCommand<PageManagerPage>(new PageManagerPageViewModel(), true, true);
    }
}
