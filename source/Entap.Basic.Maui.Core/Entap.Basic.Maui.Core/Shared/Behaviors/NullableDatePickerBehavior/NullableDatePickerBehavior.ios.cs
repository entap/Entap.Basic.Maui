using System;
using Microsoft.Maui.Platform;
using PlatformSpefic = Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using UIKit;

namespace Entap.Basic.Maui.Core
{
	public partial class NullableDatePickerBehavior
	{
        DatePicker? _datePicker;
        MauiDatePicker? _mauiDatePicker;

        protected override void OnAttachedTo(DatePicker bindable, UIView platformView)
        {
            base.OnAttachedTo(bindable, platformView);

            if (platformView is not MauiDatePicker mauiDatePicker)
            {
                return;
            }
            _datePicker = bindable;
            _mauiDatePicker = mauiDatePicker;

            if (bindable.BindingContext != null)
            {
                BindingContext = bindable.BindingContext;
            }
            bindable.DateSelected += OnDateSelected;

            var updateMode = PlatformSpefic.DatePicker.GetUpdateMode(bindable);
            if (updateMode == PlatformSpefic.UpdateMode.Immediately)
                _mauiDatePicker.Started += OnFocusChanged;
            else
                _mauiDatePicker.Ended += OnFocusChanged;

            ClearText();
        }

        protected override void OnDetachedFrom(DatePicker bindable, UIView platformView)
        {
            bindable.DateSelected -= OnDateSelected;
            if (_mauiDatePicker is not null)
            {
                _mauiDatePicker.Started -= OnFocusChanged;
                _mauiDatePicker.Ended -= OnFocusChanged;
            }
            base.OnDetachedFrom(bindable, platformView);
        }

        private void OnBindingContextChanged(object? sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = _datePicker?.BindingContext;
        }

        private void OnDateSelected(object? sender, DateChangedEventArgs e)
        {
            SetNullableDate();
        }

        private void OnFocusChanged(object? sender, EventArgs e)
        {
            if (_datePicker is null) return;
            _mauiDatePicker?.UpdateDate(_datePicker);
            SetNullableDate();
        }

        private void SetNullableDate()
        {
            NullableDate = _datePicker?.Date;
        }

        private void ClearText()
        {
            if (_mauiDatePicker is null) { return; }
            _mauiDatePicker.Text = null;
        }
    }
}

