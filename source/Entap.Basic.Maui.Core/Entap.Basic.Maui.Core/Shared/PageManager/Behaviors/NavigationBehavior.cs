using System;

namespace Entap.Basic.Maui.Core
{
    /// <summary>
    /// NavigationPageのPoppedイベントを検出し、 OnEntry・OnDestroyを実行する。
    /// （NavigationBarの戻るボタンのクリック時等、コード制御出来ないためBehaviorで検出する。）
    /// </summary>
    public class NavigationBehavior : BindableBehavior<NavigationPage>
    {
        protected override void OnAttachedTo(NavigationPage bindable)
        {
            base.OnAttachedTo(bindable);

            if (AssociatedObject is not null)
                AssociatedObject.Popped += OnPopped;
        }

        protected override void OnDetachingFrom(NavigationPage bindable)
        {
            if (AssociatedObject is not null)
                AssociatedObject.Popped -= OnPopped;

            base.OnDetachingFrom(bindable);
        }

        void OnPopped(object? sender, NavigationEventArgs e)
        {
            var currentPage = AssociatedObject?.CurrentPage;
            if (currentPage is not null)
            {
                PageManager.Navigation.GetViewModelBase(currentPage)?.OnEntry();
                if (currentPage is TabbedPage tabbedPage)
                    PageManager.Navigation.GetViewModelBase(tabbedPage.CurrentPage)?.OnEntry();
            }
            PageManager.Navigation.OnPagePopped(e.Page);
        }
    }
}
