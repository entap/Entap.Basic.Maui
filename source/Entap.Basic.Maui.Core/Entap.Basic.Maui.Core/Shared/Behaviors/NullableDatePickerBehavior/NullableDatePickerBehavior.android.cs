using System;
using Microsoft.Maui.Platform;
using AView = global::Android.Views.View;

namespace Entap.Basic.Maui.Core
{
	public partial class NullableDatePickerBehavior
	{
        DatePicker? _datePicker;
        MauiDatePicker? _mauiDatePicker;

        protected override void OnAttachedTo(DatePicker bindable, AView platformView)
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
            _mauiDatePicker.ShowPicker += new Action(() =>
            {
                OnFocus();
            });
            ClearText();
        }

        protected override void OnDetachedFrom(DatePicker bindable, AView platformView)
        {
            bindable.DateSelected -= OnDateSelected;
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

        private void OnFocus()
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

