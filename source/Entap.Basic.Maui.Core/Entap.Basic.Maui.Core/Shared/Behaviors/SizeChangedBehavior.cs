using System;
using System.Windows.Input;

namespace Entap.Basic.Maui.Core
{
	public class SizeChangedBehavior : BindableBehavior<VisualElement>
	{
		public SizeChangedBehavior()
		{
		}

        protected override void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.SizeChanged += OnSizeChanged;
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            bindable.SizeChanged -= OnSizeChanged;
            base.OnDetachingFrom(bindable);
        }

        #region Width BindableProperty
        public static readonly BindableProperty WidthProperty = BindableProperty.Create(
            nameof(Width),
            typeof(double),
            typeof(SizeChangedBehavior),
            -1d,
            defaultBindingMode: BindingMode.OneWayToSource);

        public double Width
        {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }
        #endregion

        #region Height BindableProperty
        public static readonly BindableProperty HeightProperty = BindableProperty.Create(
            nameof(Height),
            typeof(double),
            typeof(SizeChangedBehavior),
            -1d,
            defaultBindingMode: BindingMode.OneWayToSource);

        public double Height
        {
            get { return (double)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }
        #endregion

        #region SizeChangedCommand BindableProperty
        public static readonly BindableProperty SizeChangedCommandProperty = BindableProperty.Create(
            nameof(SizeChangedCommand),
            typeof(ICommand),
            typeof(SizeChangedBehavior),
            null,
            defaultBindingMode: BindingMode.Default);

        public ICommand SizeChangedCommand
        {
            get { return (ICommand)GetValue(SizeChangedCommandProperty); }
            set { SetValue(SizeChangedCommandProperty, value); }
        }
        #endregion

        private void OnSizeChanged(object? sender, EventArgs e)
        {
            if (AssociatedObject is null) return;
            Width = AssociatedObject.Width;
            Height = AssociatedObject.Height;

            ExecuteSizeChangedCommand();
        }

        void ExecuteSizeChangedCommand()
        {
            if (SizeChangedCommand is null)
                return;

            var size = new Size(Width, Height);
            if (SizeChangedCommand?.CanExecute(size) == true)
                SizeChangedCommand?.Execute(size);
        }
    }
}

