using System;
using System.ComponentModel;
using System.Linq;
using Microsoft.Maui.Controls;

namespace Entap.Basic.Maui.Core
{
    public partial class AndroidAutofillHintsMapper
    {
        public static readonly BindableProperty AutofillHintsProperty =
            BindableProperty.CreateAttached("AutofillHints", typeof(IEnumerable<string>), typeof(AndroidAutofillHintsMapper), default(IEnumerable<string>), propertyChanged: OnAutofillHintsPropertyChanged);

        public static IEnumerable<string>? GetAutofillHints(BindableObject view) =>
            view?.GetValue(AutofillHintsProperty) as IEnumerable<string>;

        public static void SetAutofillHints(BindableObject element, IEnumerable<string> value)
        {
            element.SetValue(AutofillHintsProperty, value);
        }

        public static void OnAutofillHintsPropertyChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            if (bindableObject is not Entry entry) { return; }
#if ANDROID
            UpdateAutofillHints(entry);
#endif
        }

        public static void Apply()
        {
#if ANDROID
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(AndroidAutofillHintsMapper.AutofillHintsProperty.PropertyName, (handler, view) =>
            {
                if (view is not Entry entry) return;
                System.Diagnostics.Debug.WriteLine("--------------------------------------------------------------------------------");
                UpdateAutofillHints(entry);
            });
#endif
        }
    }
}

