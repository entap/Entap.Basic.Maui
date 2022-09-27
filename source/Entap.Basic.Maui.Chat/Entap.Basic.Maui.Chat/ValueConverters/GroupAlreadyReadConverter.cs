using System;
using System.Globalization;

namespace Entap.Basic.Maui.Chat
{
	public class GroupAlreadyReadConverter : IMultiValueConverter
	{
        public object? Convert(object[]? values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values is null) return null;
            if (!values.Any()) return null;

            if (values.Length < 0 ||
                values[0] is not int alreadyReadText) return null;
            if (alreadyReadText < 1) return null;

            var isGroup = (values.Length > 1 &&
                values[1] is bool boolValue) ?
                boolValue :
                false;

            if (isGroup)
                return Settings.Current.AlreadyReadText + " " + (alreadyReadText - 1);
            return Settings.Current.AlreadyReadText;
        }

        public object[]? ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

