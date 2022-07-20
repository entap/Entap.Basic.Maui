using System;
namespace Entap.Basic.Maui.Core
{
    /// <summary>
    /// TextContentType
    /// https://developer.apple.com/documentation/uikit/uitextcontenttype
    /// </summary>
	public enum TextContentType
	{
        Default,

        // Web

        /// <summary>
        /// URL
        /// </summary>
        Url,

        // Identifying Contacts

        /// <summary>
        /// Prefix or title, such as Dr
        /// </summary>
        NamePrefix,
        /// <summary>
        /// Name
        /// </summary>
        Name,
        /// <summary>
        /// Suffix, such as Jr
        /// </summary>
        NameSuffix,
        /// <summary>
        /// First name
        /// </summary>
        GivenName,
        /// <summary>
        /// Middle name
        /// </summary>
        MiddleName,
        /// <summary>
        /// Family name, or last name
        /// </summary>
        FamilyName,
        /// <summary>
        /// Nickname
        /// </summary>
        Nickname,
        /// <summary>
        /// Organization name
        /// </summary>
        OrganizationName,
        /// <summary>
        /// Job title
        /// </summary>
        JobTitle,

        // Setting Location Data

        /// <summary>
        /// Location, such as a point of interest, an address, or another identifier for a location
        /// </summary>
        Location,
        /// <summary>
        /// Street address that fully identifies a location
        /// </summary>
        FullStreetAddress,
        /// <summary>
        /// The first line of a street address
        /// </summary>
        StreetAddressLine1,
        /// <summary>
        /// The second line of a street address
        /// </summary>
        StreetAddressLine2,
        /// <summary>
        /// City name
        /// </summary>
        AddressCity,
        /// <summary>
        /// City name with a state name
        /// </summary>
        AddressCityAndState,
        /// <summary>
        /// State name
        /// </summary>
        AddressState,
        /// <summary>
        /// Postal code
        /// </summary>
        PostalCode,
        /// <summary>
        /// Sublocality
        /// </summary>
        Sublocality,
        /// <summary>
        /// Country or region name
        /// </summary>
        CountryName,

        // Managing Accounts

        /// <summary>
        /// Account or login name
        /// </summary>
        Username,
        /// <summary>
        /// Password
        /// </summary>
        Password,
        /// <summary>
        /// New password
        /// </summary>
        NewPassword,

        // Securing Accounts

        /// <summary>
        /// One-time code
        /// </summary>
        OneTimeCode,
        // Setting Communication Details

        /// <summary>
        /// Email address
        /// </summary>
        EmailAddress,
        /// <summary>
        /// Telephone number
        /// </summary>
        TelephoneNumber,

        // Accepting Payment

        /// <summary>
        /// Credit card number
        /// </summary>
        CreditCardNumber,

        // Scheduling Events

        /// <summary>
        /// Date, time, or duration
        /// </summary>
        DateTime,

        // Tracking Events

        /// <summary>
        /// Airline flight number
        /// </summary>
        FlightNumber,
        /// <summary>
        /// Parcel tracking number
        /// </summary>
        ShipmentTrackingNumber,
    }
}

