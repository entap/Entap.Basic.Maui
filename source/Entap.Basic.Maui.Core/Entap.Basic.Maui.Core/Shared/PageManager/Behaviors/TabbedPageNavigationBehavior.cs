using System;

namespace Entap.Basic.Maui.Core
{
    public class TabbedPageNavigationBehavior : BindableBehavior<TabbedPage>
    {
        TabbedPage? _tabbedPage;
        Page? _oldPage;
        protected override void OnAttachedTo(TabbedPage bindable)
        {
            base.OnAttachedTo(bindable);

            _tabbedPage = bindable;
            _oldPage = bindable.CurrentPage;

            if (AssociatedObject is not null)
                AssociatedObject.CurrentPageChanged += OnCurrentPageChanged;
        }
        protected override void OnDetachingFrom(TabbedPage bindable)
        {
            if (AssociatedObject is not null)
                AssociatedObject.CurrentPageChanged -= OnCurrentPageChanged;

            _tabbedPage = null;
            _oldPage = null;
            base.OnDetachingFrom(bindable);
        }

        void OnCurrentPageChanged(object? sender, EventArgs e)
        {
            if (_oldPage is not null)
                ((PageViewModelBase)_oldPage.BindingContext)?.OnExit();

            if (_tabbedPage?.CurrentPage is NavigationPage navigationPage)
            {
                var currentPage = navigationPage.CurrentPage;
                var currentBindingContext = currentPage.BindingContext;
                if (currentBindingContext is null)
                {
                    EventHandler handler = null;
                    handler += (sender, e) =>
                    {
                        currentPage.BindingContextChanged -= handler;
                        GetCuttentPageViewModel(currentPage)?.OnEntry();
                    };
                    currentPage.BindingContextChanged += handler;
                }
                else
                    GetCuttentPageViewModel(currentPage)?.OnEntry();
            }
            else
                (_tabbedPage?.CurrentPage?.BindingContext as PageViewModelBase)?.OnEntry();

            _oldPage = _tabbedPage?.CurrentPage;

            PageViewModelBase? GetCuttentPageViewModel(Page currentPage) =>
                currentPage.BindingContext as PageViewModelBase;

        }
        protected override void OnBindingContextChanged()
        {
            // TabbedPageの初期表示時に、CurrentPageのBindingContextがNullのため
            // BindingContextChange後にOnEntryを実行
            (_tabbedPage?.CurrentPage?.BindingContext as PageViewModelBase)?.OnEntry();
        }
    }
}