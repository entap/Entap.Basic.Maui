using System;
namespace Entap.Basic.Maui.Core
{
    public partial class PaddingBehavior : PlatformBehavior<View>
    {
        public PaddingBehavior()
        {
        }

        #region Padding BindableProperty
        public static readonly BindableProperty PaddingProperty = BindableProperty.Create(
            nameof(Padding),
            typeof(Thickness),
            typeof(PaddingBehavior),
            null,
            defaultBindingMode: BindingMode.Default,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (bindable is not PaddingBehavior behavior) return;
                behavior.UpdatePadding();
            });

        public Thickness Padding
        {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        }
        #endregion

        void UpdatePadding()
        {
#if ANDROID
            PlatformUpdatePadding(Padding);
#endif
        }
    }
}

