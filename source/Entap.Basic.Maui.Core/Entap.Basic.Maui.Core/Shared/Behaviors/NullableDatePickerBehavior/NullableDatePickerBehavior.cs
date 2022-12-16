using System;

namespace Entap.Basic.Maui.Core
{
	public partial class NullableDatePickerBehavior : PlatformBehavior<DatePicker>
	{
        #region NullableDate BindableProperty
        public static readonly BindableProperty NullableDateProperty = BindableProperty.Create(
            nameof(NullableDate),
            typeof(DateTime?),
            typeof(NullableDatePickerBehavior),
            null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (bindable is NullableDatePickerBehavior behavior)
                {
#if IOS || ANDROID
                    if (newValue is null)
                        behavior.ClearText();
#endif
                }
            });

        public DateTime? NullableDate
        {
            get { return (DateTime?)GetValue(NullableDateProperty); }
            set { SetValue(NullableDateProperty, value); }
        }
        #endregion
    }
}

