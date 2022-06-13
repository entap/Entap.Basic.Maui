using System;

namespace Entap.Basic.Maui.Core
{
    /// <summary>
    /// Bind可能なBehaviorの基底クラス
    /// </summary>
    public class BindableBehavior<T> : Behavior<T> where T : BindableObject
    {
        public T? AssociatedObject { get; private set; }

        #region NeedsDetach BindableProperty
        public static readonly BindableProperty NeedsDetachProperty = BindableProperty.Create(
            nameof(NeedsDetach),
            typeof(bool),
            typeof(BindableBehavior<T>),
            false,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (bindable is BindableBehavior<T> behavior)
                {
                    behavior.NeedsDetach = (bool)newValue;
                    behavior.OnNeedsDetachChanged();
                }
            });

        public bool NeedsDetach
        {
            get { return (bool)GetValue(NeedsDetachProperty); }
            set { SetValue(NeedsDetachProperty, value); }
        }
        #endregion

        protected override void OnAttachedTo(T bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;
            if (bindable.BindingContext != null)
            {
                BindingContext = bindable.BindingContext;
            }

            bindable.BindingContextChanged += OnBindingContextChanged;
        }

        protected override void OnDetachingFrom(T bindable)
        {
            bindable.BindingContextChanged -= OnBindingContextChanged;
            AssociatedObject = null;
            base.OnDetachingFrom(bindable);
        }

        private void OnBindingContextChanged(object? sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject?.BindingContext;
        }

        private void OnNeedsDetachChanged()
        {
            if (AssociatedObject is null) return;
            if (!NeedsDetach) return;

            OnDetachingFrom(AssociatedObject);
        }
    }
}
