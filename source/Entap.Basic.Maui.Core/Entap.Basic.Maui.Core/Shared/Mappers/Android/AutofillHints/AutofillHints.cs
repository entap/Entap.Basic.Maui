using System;
#if ANDROID
using AndroidX.AutoFill;
#endif

namespace Entap.Basic.Maui.Core
{
    public struct AutofillHints
    {
#if ANDROID
        public static readonly string? AutofillHintBirthDateDay = HintConstants.AutofillHintBirthDateDay;
        public static readonly string? AutofillHintBirthDateFull = HintConstants.AutofillHintBirthDateFull;
        public static readonly string? AutofillHintBirthDateMonth = HintConstants.AutofillHintBirthDateMonth;
        public static readonly string? AutofillHintBirthDateYear = HintConstants.AutofillHintBirthDateYear;
        public static readonly string? AutofillHintCreditCardExpirationDate = HintConstants.AutofillHintCreditCardExpirationDate;
        public static readonly string? AutofillHintCreditCardExpirationDay = HintConstants.AutofillHintCreditCardExpirationDay;
        public static readonly string? AutofillHintCreditCardExpirationMonth = HintConstants.AutofillHintCreditCardExpirationMonth;
        public static readonly string? AutofillHintCreditCardExpirationYear = HintConstants.AutofillHintCreditCardExpirationYear;
        public static readonly string? AutofillHintCreditCardNumber = HintConstants.AutofillHintCreditCardNumber;
        public static readonly string? AutofillHintCreditCardSecurityCode = HintConstants.AutofillHintCreditCardSecurityCode;
        public static readonly string? AutofillHintEmailAddress = HintConstants.AutofillHintEmailAddress;
        public static readonly string? AutofillHintGender = HintConstants.AutofillHintGender;
        public static readonly string? AutofillHintNewPassword = HintConstants.AutofillHintNewPassword;
        public static readonly string? AutofillHintNewUsername = HintConstants.AutofillHintNewUsername;
        public static readonly string? AutofillHintPassword = HintConstants.AutofillHintPassword;
        public static readonly string? AutofillHintPersonName = HintConstants.AutofillHintPersonName;
        public static readonly string? AutofillHintPersonNameFamily = HintConstants.AutofillHintPersonNameFamily;
        public static readonly string? AutofillHintPersonNameGiven = HintConstants.AutofillHintPersonNameGiven;
        public static readonly string? AutofillHintPersonNameMiddle = HintConstants.AutofillHintPersonNameMiddle;
        public static readonly string? AutofillHintPersonNameMiddleInitial = HintConstants.AutofillHintPersonNameMiddleInitial;
        public static readonly string? AutofillHintPersonNamePrefix = HintConstants.AutofillHintPersonNamePrefix;
        public static readonly string? AutofillHintPersonNameSuffix = HintConstants.AutofillHintPersonNameSuffix;
        public static readonly string? AutofillHintPhoneCountryCode = HintConstants.AutofillHintPhoneCountryCode;
        public static readonly string? AutofillHintPhoneNational = HintConstants.AutofillHintPhoneNational;
        public static readonly string? AutofillHintPhoneNumber = HintConstants.AutofillHintPhoneNumber;
        public static readonly string? AutofillHintPhoneNumberDevice = HintConstants.AutofillHintPhoneNumberDevice;
        public static readonly string? AutofillHintPostalAddress = HintConstants.AutofillHintPostalAddress;
        public static readonly string? AutofillHintPostalAddressCountry = HintConstants.AutofillHintPostalAddressCountry;
        public static readonly string? AutofillHintPostalAddressExtendedAddress = HintConstants.AutofillHintPostalAddressExtendedAddress;
        public static readonly string? AutofillHintPostalAddressExtendedPostalCode = HintConstants.AutofillHintPostalAddressExtendedPostalCode;
        public static readonly string? AutofillHintPostalAddressLocality = HintConstants.AutofillHintPostalAddressLocality;
        public static readonly string? AutofillHintPostalAddressRegion = HintConstants.AutofillHintPostalAddressRegion;
        public static readonly string? AutofillHintPostalAddressStreetAddress = HintConstants.AutofillHintPostalAddressStreetAddress;
        public static readonly string? AutofillHintPostalCode = HintConstants.AutofillHintPostalCode;
        public static readonly string? AutofillHintSmsOtp = HintConstants.AutofillHintSmsOtp;
        public static readonly string? AutofillHintUsername = HintConstants.AutofillHintUsername;

        // 1.1.2-beta01
        // Xamarin.AndroidX.AutoFillでは未サポートのため独自定義
        // https://www.nuget.org/packages/Xamarin.AndroidX.AutoFill#versions-body-tab
        public static readonly string? AutofillHint2FaAppOtp = "2faAppOTPCode";
        public static readonly string? AutofillHintEmailOtp = "emailOTPCode";
        public static readonly string? AutofillHintNotApplicable = "notApplicable";
        public static readonly string? AutofillHintPostalAddressAptNumber = "aptNumber";
        public static readonly string? AutofillHintPostalAddressDependentLocality = "dependentLocality";
        public static readonly string? AutofillHintPromoCode = "promoCode";
        public static readonly string? AutofillHintUpiVpa = "upiVirtualPaymentAddress";
        public static readonly string? AutofillHintWifiPassword = "wifiPassword";
#else
        public static readonly string? AutofillHintBirthDateDay = null;
        public static readonly string? AutofillHintBirthDateFull = null;
        public static readonly string? AutofillHintBirthDateMonth = null;
        public static readonly string? AutofillHintBirthDateYear = null;
        public static readonly string? AutofillHintCreditCardExpirationDate = null;
        public static readonly string? AutofillHintCreditCardExpirationDay = null;
        public static readonly string? AutofillHintCreditCardExpirationMonth = null;
        public static readonly string? AutofillHintCreditCardExpirationYear = null;
        public static readonly string? AutofillHintCreditCardNumber = null;
        public static readonly string? AutofillHintCreditCardSecurityCode = null;
        public static readonly string? AutofillHintEmailAddress = null;
        public static readonly string? AutofillHintGender = null;
        public static readonly string? AutofillHintNewPassword = null;
        public static readonly string? AutofillHintNewUsername = null;
        public static readonly string? AutofillHintPassword = null;
        public static readonly string? AutofillHintPersonName = null;
        public static readonly string? AutofillHintPersonNameFamily = null;
        public static readonly string? AutofillHintPersonNameGiven = null;
        public static readonly string? AutofillHintPersonNameMiddle = null;
        public static readonly string? AutofillHintPersonNameMiddleInitial = null;
        public static readonly string? AutofillHintPersonNamePrefix = null;
        public static readonly string? AutofillHintPersonNameSuffix = null;
        public static readonly string? AutofillHintPhoneCountryCode = null;
        public static readonly string? AutofillHintPhoneNational = null;
        public static readonly string? AutofillHintPhoneNumber = null;
        public static readonly string? AutofillHintPhoneNumberDevice = null;
        public static readonly string? AutofillHintPostalAddress = null;
        public static readonly string? AutofillHintPostalAddressCountry = null;
        public static readonly string? AutofillHintPostalAddressExtendedAddress = null;
        public static readonly string? AutofillHintPostalAddressExtendedPostalCode = null;
        public static readonly string? AutofillHintPostalAddressLocality = null;
        public static readonly string? AutofillHintPostalAddressRegion = null;
        public static readonly string? AutofillHintPostalAddressStreetAddress = null;
        public static readonly string? AutofillHintPostalCode = null;
        public static readonly string? AutofillHintSmsOtp = null;
        public static readonly string? AutofillHintUsername = null;

        // 1.1.2-beta01
        public static readonly string? AutofillHint2FaAppOtp = null;
        public static readonly string? AutofillHintEmailOtp = null;
        public static readonly string? AutofillHintNotApplicable = null;
        public static readonly string? AutofillHintPostalAddressAptNumber = null;
        public static readonly string? AutofillHintPostalAddressDependentLocality = null;
        public static readonly string? AutofillHintPromoCode = null;
        public static readonly string? AutofillHintUpiVpa = null;
        public static readonly string? AutofillHintWifiPassword = null;
#endif
    }
}

