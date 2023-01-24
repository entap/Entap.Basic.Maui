using System;
namespace Entap.Basic.Maui.Core
{
    public partial class KeyboardOverlappingBehavior : BindableBehavior<View>
    {
        const double DefaulfBottomMargin = 2d;
        public KeyboardOverlappingBehavior()
        {
        }

        #region IsShownKeyboard BindableProperty
        /// <summary>
        /// キーボードが表示中か
        /// </summary>
        public static readonly BindableProperty IsShownKeyboardProperty = BindableProperty.Create(
            nameof(IsShownKeyboard),
            typeof(bool),
            typeof(KeyboardOverlappingBehavior),
            false,
            defaultBindingMode: BindingMode.TwoWay);

        public bool IsShownKeyboard
        {
            get { return (bool)GetValue(IsShownKeyboardProperty); }
            set { SetValue(IsShownKeyboardProperty, value); }
        }
        #endregion

        #region BottomMargin
        /// <summary>
        /// キーボード表示時のViewとKeyboardとのMargin
        /// </summary>
        public static readonly BindableProperty BottomMarginProperty =
            BindableProperty.CreateAttached("BottomMargin", typeof(double), typeof(KeyboardOverlappingBehavior), DefaulfBottomMargin);

        public static double GetBottomMargin(BindableObject view)
        {
            return (double)view.GetValue(BottomMarginProperty);
        }
        #endregion
    }
}

