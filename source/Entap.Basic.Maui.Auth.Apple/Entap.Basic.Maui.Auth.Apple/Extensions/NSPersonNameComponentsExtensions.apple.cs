using System;
using Foundation;

namespace Entap.Basic.Maui.Auth.Apple
{
    public static class NSPersonNameComponentsExtensions
    {
        public static PersonName ToPersonName(this NSPersonNameComponents components)
        {
            return new PersonName(components.FullName())
            {
                FamilyName = components.FamilyName,
                GivenName = components.GivenName,
                MiddleName = components.MiddleName,
                NamePrefix = components.NamePrefix,
                NameSuffix = components.NameSuffix,
                Nickname = components.Nickname,
            };
        }

        public static string? FullName(this NSPersonNameComponents? personNameComponents)
        {
            if (personNameComponents is null) return null;

            var nameComponentsFormatter = new NSPersonNameComponentsFormatter();
            return nameComponentsFormatter.GetString(personNameComponents);
        }
    }
}
