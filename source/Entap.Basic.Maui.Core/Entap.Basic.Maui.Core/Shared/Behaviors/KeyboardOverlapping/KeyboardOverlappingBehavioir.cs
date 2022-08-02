using System;
namespace Entap.Basic.Maui.Core
{
    public partial class KeyboardOverlappingBehavioir : BindableBehavior<View>
    {
        const double DefaulfBottomMargin = 2d;
        public KeyboardOverlappingBehavioir()
        {
        }

        #region BottomMargin
        /// <summary>
        /// キーボード表示時のViewとKeyboardとのMargin
        /// </summary>
        public static readonly BindableProperty BottomMarginProperty =
            BindableProperty.CreateAttached("BottomMargin", typeof(double), typeof(KeyboardOverlappingBehavioir), DefaulfBottomMargin);

        public static double GetBottomMargin(BindableObject view)
        {
            return (double)view.GetValue(BottomMarginProperty);
        }
        #endregion
    }
}

