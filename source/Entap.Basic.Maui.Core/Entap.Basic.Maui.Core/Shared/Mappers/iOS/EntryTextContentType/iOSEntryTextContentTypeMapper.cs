using System;

namespace Entap.Basic.Maui.Core
{
	public partial class iOSEntryTextContentTypeMapper
    {
		public iOSEntryTextContentTypeMapper()
		{
		}

        public static readonly BindableProperty TextContentTypeProperty =
            BindableProperty.CreateAttached("TextContentType", typeof(TextContentType), typeof(iOSEntryTextContentTypeMapper), TextContentType.Default, propertyChanged: OnTextContentTypePropertyChanged);

        public static TextContentType? GetTextContentType(BindableObject view) =>
            (TextContentType?)view?.GetValue(TextContentTypeProperty);

        public static void SetTextContentType(BindableObject element, TextContentType? value)
        {
            element.SetValue(TextContentTypeProperty, value);
        }

        public static void OnTextContentTypePropertyChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            if (bindableObject is not Entry entry) { return; }
#if IOS
            UpdateTextContentType(entry);
#endif
        }

        public static void Apply()
        {
#if IOS
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(iOSEntryTextContentTypeMapper.TextContentTypeProperty.PropertyName, (handler, view) =>
            {
                if (view is not Entry entry) return;
                UpdateTextContentType(entry);
            });
#endif
        }
    }
}

