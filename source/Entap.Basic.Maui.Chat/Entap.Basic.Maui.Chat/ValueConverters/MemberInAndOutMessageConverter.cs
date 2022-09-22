using System;
using System.Globalization;

namespace Entap.Basic.Maui.Chat
{
    [Preserve(AllMembers = true)]
    public class MemberInAndOutMessageConverter : BindableObject, IValueConverter
    {
        public static BindableProperty MessageTypeProperty = BindableProperty.Create("MessageType", typeof(int), typeof(MemberInAndOutMessageConverter));
        public int MessageType
        {
            get => (int)GetValue(MessageTypeProperty);
            set => SetValue(MessageTypeProperty, value);
        }

        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = value as string;
            if (MessageType == (int)Entap.Basic.Maui.Chat.MessageType.MemberAddRoom)
            {
                return val + Settings.Current.MemberAddRoomText;
            }
            else if (MessageType == (int)Entap.Basic.Maui.Chat.MessageType.MemberLeaveRoom)
            {
                return val + Settings.Current.MemberLeaveRoomText;
            }
            return val;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
