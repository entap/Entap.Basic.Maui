namespace Entap.Basic.Maui.Core
{
    public interface IPageNavigation
    {
        Page? GetCurrentPage();
        PageViewModelBase? GetViewModelBase(Page page, object? viewModel = null);

        Task PushAsync<T>(PageViewModelBase? viewModel = null, bool animated = true, bool removePages = false) where T : Page;
        Task PushAsync(Page page, PageViewModelBase viewModel, bool animated = true, bool removePages = false);
        Task PushModalAsync<T>(PageViewModelBase? viewModel = null, bool animated = true) where T : Page;
        Task PushModalAsync(Page page, PageViewModelBase? viewModel = null, bool animated = true);
        Task PushNavigationModalAsync<T>(PageViewModelBase? viewModel = null, bool animated = true, bool hasNavigationCloseButton = false) where T : Page;
        Task PushNavigationModalAsync(Page page, PageViewModelBase? viewModel = null, bool animated = true, bool hasNavigationCloseButton = false);

        Task PopAsync(bool animated = true);
        Task PopModalAsync(bool animated = true);
        Task PopToRootAsync(bool animated = true);

        void InsertPageBefore<T>(PageViewModelBase insertPageViewModel, Page? beforePage = null) where T : Page;

        void OnPagePopped(Page page, PageViewModelBase? viewModelBase = null);
        void OnPopToRoot(INavigation navigation);

        Task<Task> SetMainPage<T>(PageViewModelBase? viewModel = null) where T : Page;
        Task<Task> SetNavigationMainPage<T>(PageViewModelBase? viewModel = null) where T : Page;
    }
}