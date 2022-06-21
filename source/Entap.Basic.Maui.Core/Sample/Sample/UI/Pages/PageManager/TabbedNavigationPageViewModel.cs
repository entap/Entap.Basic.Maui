using Entap.Basic.Maui.Core;

namespace Sample
{
	public class TabbedNavigationPageViewModel : PageViewModelBase
    {
		public TabbedNavigationPageViewModel()
		{
		}

        public PageManagerPageViewModel PageManagerPageViewModel { get; set; } = new PageManagerPageViewModel();

        public MainPageViewModel MainPageViewModel { get; set; } = new MainPageViewModel();
    }
}

