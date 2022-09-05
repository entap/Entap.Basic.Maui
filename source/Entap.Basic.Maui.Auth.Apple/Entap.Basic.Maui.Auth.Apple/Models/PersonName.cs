using System;
namespace Entap.Basic.Maui.Auth.Apple
{
    /// <summary>
    /// 個人名
    /// https://developer.apple.com/documentation/foundation/personnamecomponents
    /// </summary>
    public class PersonName
    {
        public PersonName(string? fullName)
        {
            FullName = fullName;
        }

        public string? FullName { get; }

        public string? GivenName { get; set; }

        public string? FamilyName { get; set; }

        public string? MiddleName { get; set; }

        public string? NamePrefix { get; set; }

        public string? NameSuffix { get; set; }

        public string? Nickname { get; set; }
    }
}
