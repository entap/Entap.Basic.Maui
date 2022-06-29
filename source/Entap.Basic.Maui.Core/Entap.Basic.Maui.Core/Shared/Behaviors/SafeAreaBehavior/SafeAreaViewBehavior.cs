using System;

namespace Entap.Basic.Maui.Core
{
    /// <summary>
    /// Marginを使用し、ViewにSafeAreaを適用する
    /// </summary>
    public class SafeAreaViewBehavior : BindableBehavior<View>
    {
        Thickness? defaultMargin;
        public SafeAreaViewBehavior()
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

            SetSafeMargin();
        }

        #region PositionFlags BindableProperty
        public static readonly BindableProperty PositionFlagsProperty = BindableProperty.Create(
            nameof(PositionFlags),
            typeof(ThicknessPositionFlags),
            typeof(SafeAreaViewBehavior),
            null,
            defaultBindingMode: BindingMode.Default,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (bindable is not SafeAreaViewBehavior behavior) return;
                behavior.SetSafeMargin();
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
            typeof(Thickness),
            typeof(SafeAreaViewBehavior),
            default(Thickness),
            defaultBindingMode: BindingMode.Default,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (!(bindable is SafeAreaViewBehavior behavior)) return;
                behavior.SetSafeMargin();
            });

        public Thickness SafeArea
        {
            get { return (Thickness)GetValue(SafeAreaProperty); }
            set { SetValue(SafeAreaProperty, value); }
        }
        #endregion

        void SetSafeMargin()
        {
            if (AssociatedObject is null) return;
            if (defaultMargin is null)
                defaultMargin = AssociatedObject.Margin;

            var margin = defaultMargin.Value.GetSafeAreaAppliedThickness(SafeArea, PositionFlags);
            if (margin is null) return;

            AssociatedObject.Margin = margin.Value;
        }
    }
}
