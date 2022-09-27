using System;
using System.Globalization;

namespace Entap.Basic.Maui.Chat
{
    [Preserve(AllMembers = true)]
    [Obsolete($"{nameof(IsGroupChat)}のバインディングが動作しません。グループチャット使用時は{nameof(GroupAlreadyReadConverter)}を使用してください。")]
    public class AlreadyReadConverter : BindableObject, IValueConverter
    {
        public static BindableProperty IsGroupChatProperty = BindableProperty.Create("IsGroupChat", typeof(bool), typeof(AlreadyReadConverter));
        public bool IsGroupChat
        {
            get => (bool)GetValue(IsGroupChatProperty);
            set => SetValue(IsGroupChatProperty, value);
        }

        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var count = (int)value;
            if (count > 1)
            {
                if (IsGroupChat)
                    return Settings.Current.AlreadyReadText + " " + (count - 1);
                return Settings.Current.AlreadyReadText;
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
