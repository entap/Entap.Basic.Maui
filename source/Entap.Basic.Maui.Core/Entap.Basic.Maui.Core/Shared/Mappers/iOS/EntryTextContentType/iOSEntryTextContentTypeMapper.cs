using System;
#if IOS
using UIKit;
using Foundation;
#endif

namespace Entap.Basic.Maui.Core
{
	public partial class iOSEntryTextContentTypeMapper
    {
		public iOSEntryTextContentTypeMapper()
		{
		}

        public static readonly BindableProperty TextContentTypeProperty =
            BindableProperty.CreateAttached("TextContentType", typeof(TextContentType), typeof(Entry), TextContentType.Default, propertyChanged: OnTextContentTypePropertyChanged);

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

#if IOS
        static void UpdateTextContentType(Entry entry)
        {
            if (entry is null) { return; }
            if (entry.Handler?.PlatformView is not UITextField textField) { return; }

            var textContentType = iOSEntryTextContentTypeMapper.GetTextContentType(entry);
            if (textContentType is null) return;

            textField.AutocorrectionType = UITextAutocorrectionType.Yes;
            var uiTextConetntType = GetUITextContentType(textContentType.Value);
            textField.TextContentType = uiTextConetntType;

            EnableAutoFillTelephoneNumber(entry, textField);
        }

        // https://developer.apple.com/forums/thread/120703
        static void EnableAutoFillTelephoneNumber(Entry entry, UITextField textField)
        {
            if (textField.TextContentType != UITextContentType.TelephoneNumber) return;

            entry.Keyboard = Keyboard.Default;
            textField.KeyboardType = UIKeyboardType.AsciiCapableNumberPad;
        }


        static NSString GetUITextContentType(TextContentType textContentType)
        {
            if (OperatingSystem.IsIOSVersionAtLeast(15, 0))
            {
                switch (textContentType)
                {
                    case TextContentType.DateTime:
                        return UITextContentType.DateTime;
                    case TextContentType.FlightNumber:
                        return UITextContentType.FlightNumber;
                    case TextContentType.ShipmentTrackingNumber:
                        return UITextContentType.ShipmentTrackingNumber;
                    default:
                        break;
                }
            }

            switch (textContentType)
            {
                case TextContentType.Default:
                    return new NSString();
                case TextContentType.Url:
                    return UITextContentType.Url;
                case TextContentType.NamePrefix:
                    return UITextContentType.NamePrefix;
                case TextContentType.Name:
                    return UITextContentType.Name;
                case TextContentType.NameSuffix:
                    return UITextContentType.NameSuffix;
                case TextContentType.GivenName:
                    return UITextContentType.GivenName;
                case TextContentType.MiddleName:
                    return UITextContentType.MiddleName;
                case TextContentType.FamilyName:
                    return UITextContentType.FamilyName;
                case TextContentType.Nickname:
                    return UITextContentType.Nickname;
                case TextContentType.OrganizationName:
                    return UITextContentType.OrganizationName;
                case TextContentType.JobTitle:
                    return UITextContentType.JobTitle;
                case TextContentType.Location:
                    return UITextContentType.Location;
                case TextContentType.FullStreetAddress:
                    return UITextContentType.FullStreetAddress;
                case TextContentType.StreetAddressLine1:
                    return UITextContentType.StreetAddressLine1;
                case TextContentType.StreetAddressLine2:
                    return UITextContentType.StreetAddressLine2;
                case TextContentType.AddressCity:
                    return UITextContentType.AddressCity;
                case TextContentType.AddressCityAndState:
                    return UITextContentType.AddressCityAndState;
                case TextContentType.AddressState:
                    return UITextContentType.AddressState;
                case TextContentType.PostalCode:
                    return UITextContentType.PostalCode;
                case TextContentType.Sublocality:
                    return UITextContentType.Sublocality;
                case TextContentType.CountryName:
                    return UITextContentType.CountryName;
                case TextContentType.Username:
                    return UITextContentType.Username;
                case TextContentType.Password:
                    return UITextContentType.Password;
                case TextContentType.NewPassword:
                    return UITextContentType.NewPassword;
                case TextContentType.OneTimeCode:
                    return UITextContentType.OneTimeCode;
                case TextContentType.EmailAddress:
                    return UITextContentType.EmailAddress;
                case TextContentType.TelephoneNumber:
                    return UITextContentType.TelephoneNumber;
                case TextContentType.CreditCardNumber:
                    return UITextContentType.CreditCardNumber;
                default:
#if DEBUG
                    throw new ArgumentOutOfRangeException(nameof(textContentType));
#else
                    return new NSString(textContentType.ToString());
#endif
            }
        }
#endif
    }
}

