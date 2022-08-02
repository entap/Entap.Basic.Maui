using System;
using Foundation;
using UIKit;

namespace Entap.Basic.Maui.Core
{
    public partial class KeyboardOverlappingBehavioir
    {
        // KeyboardObserver
        NSObject? _keyboardShownObserver;
        NSObject? _keyboardHiddenObserver;

        Thickness? _margin;

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);
            SubscribeKeyboardObserver();
        }

        protected override void OnDetachingFrom(View bindable)
        {
            UnsubscribeKeyboardObserver();
            base.OnDetachingFrom(bindable);
        }

        void SubscribeKeyboardObserver()
        {
            _keyboardShownObserver = UIKeyboard.Notifications.ObserveWillShow(OnKeyboardShown);
            _keyboardHiddenObserver = UIKeyboard.Notifications.ObserveWillHide(OnKeyboardHidden);
        }

        void UnsubscribeKeyboardObserver()
        {
            _keyboardShownObserver?.Dispose();
            _keyboardHiddenObserver?.Dispose();
        }

        void OnKeyboardShown(object? sender, UIKeyboardEventArgs e)
        {
            if (AssociatedObject is null) return;
            if (_margin is null)
            {
                // ViewのMaringを退避
                _margin = AssociatedObject.Margin;
            }

            var bottomMargin = GetBottomMargin(AssociatedObject);
            var bottom = e.FrameEnd.Height + bottomMargin;
            if (bottom <= 0) return;

            AssociatedObject.Margin = new Thickness(
                _margin.Value.Left,
                _margin.Value.Top,
                _margin.Value.Right,
                bottom);
        }

        void OnKeyboardHidden(object? sender, UIKeyboardEventArgs e)
        {
            if (AssociatedObject is null) return;
            if (_margin is null) return;

            // Viewが保有していたMaringに戻す
            AssociatedObject.Margin = _margin.Value;
        }
    }
}

