using Entap.Basic.Maui.Core;

namespace Sample
{
    public class MyTabbedPageViewModel : PageViewModelBase
    {
        public MyTabbedPageViewModel()
        {
        }

        public PageManagerPageViewModel PageManagerPageViewModel { get; set; } = new PageManagerPageViewModel();

        public MainPageViewModel MainPageViewModel { get; set; } = new MainPageViewModel();
    }
}
