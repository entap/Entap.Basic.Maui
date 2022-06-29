using System;

namespace Entap.Basic.Maui.Core
{
    /// <summary>
    /// Paddingを使用し、LayoutにSafeAreaを適用する
    /// </summary>
    public class SafeAreaLayoutBehavior : BindableBehavior<Layout>
    {
        Thickness? defaultPadding;
        public SafeAreaLayoutBehavior()
        {
        }

        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);

            if (!OperatingSystem.IsIOS())
            {
                OnDetachingFrom(bindable);
                return;
            }

            SetSafePadding();
        }

        #region PositionFlags BindableProperty
        public static readonly BindableProperty PositionFlagsProperty = BindableProperty.Create(
            nameof(PositionFlags),
            typeof(ThicknessPositionFlags),
            typeof(SafeAreaLayoutBehavior),
            null,
            defaultBindingMode: BindingMode.Default,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (bindable is not SafeAreaLayoutBehavior behavior) return;
                behavior.PositionFlags = (ThicknessPositionFlags)newValue;
                behavior.SetSafePadding();
            });

        public ThicknessPositionFlags PositionFlags
        {
            get { return (ThicknessPositionFlags)GetValue(PositionFlagsProperty); }
            set { SetValue(PositionFlagsProperty, value); }
        }
        #endregion

        #region SafeArea BindableProperty
        public static readonly BindableProperty SafeAreaProperty = BindableProperty.Create(
            nameof(SafeArea),
            typeof(Thickness?),
            typeof(SafeAreaLayoutBehavior),
            null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (!(bindable is SafeAreaLayoutBehavior behavior)) return;
                behavior.SafeArea = (Thickness)newValue;
                behavior.SetSafePadding();
            });

        public Thickness SafeArea
        {
            get { return (Thickness)GetValue(SafeAreaProperty); }
            set { SetValue(SafeAreaProperty, value); }
        }
        #endregion

        void SetSafePadding()
        {
            if (AssociatedObject is null) return;
            if (defaultPadding is null)
                defaultPadding = AssociatedObject.Padding;

            var padding = defaultPadding.Value.GetSafeAreaAppliedThickness(SafeArea, PositionFlags);
            if (padding is null) return;

            AssociatedObject.Padding = padding.Value;
        }
    }
}
