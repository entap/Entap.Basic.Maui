using System;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Page = Microsoft.Maui.Controls.Page;

namespace Entap.Basic.Maui.Core
{
    public class GetPageSafeAreaBehavior : BindableBehavior<Page>
    {
        protected override void OnAttachedTo(Page bindable)
        {
            base.OnAttachedTo(bindable);
            if (!OperatingSystem.IsIOS())
            {
                OnDetachingFrom(bindable);
                return;
            }
            bindable.Appearing += OnPageAppearing;
        }

        protected override void OnDetachingFrom(Page bindable)
        {
            bindable.Appearing -= OnPageAppearing;
            base.OnDetachingFrom(bindable);
        }

        #region SafeArea BindableProperty
        public static readonly BindableProperty SafeAreaProperty = BindableProperty.Create(
            nameof(SafeArea),
            typeof(Thickness),
            typeof(GetPageSafeAreaBehavior),
            default(Thickness),
            defaultBindingMode: BindingMode.OneWayToSource,
            propertyChanged: (bindable, oldValue, newValue) =>
            ((GetPageSafeAreaBehavior)bindable).SafeArea = (Thickness)newValue);

        public Thickness SafeArea
        {
            get { return (Thickness)GetValue(SafeAreaProperty); }
            set { SetValue(SafeAreaProperty, value); }
        }
        #endregion

        void OnPageAppearing(object? sender, EventArgs e)
        {
            UpdateSafeArea();
        }

        void UpdateSafeArea()
        {
            if (!OperatingSystem.IsIOS()) return;
            if (AssociatedObject is null) return;

            var safeArea = AssociatedObject.On<Microsoft.Maui.Controls.PlatformConfiguration.iOS>().SafeAreaInsets();
            if (SafeArea.Equals(safeArea)) return;

            SafeArea = safeArea;
        }
    }
}
