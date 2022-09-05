using System;
namespace Entap.Basic.Maui.Auth.Apple
{
    /// <summary>
    /// AppleIdCredential
    /// https://developer.apple.com/documentation/authenticationservices/asauthorizationappleidcredential
    /// </summary>
    public class AppleIdCredential
    {
        public AppleIdCredential(string userId, UserDetectionStatus userDetectionStatus)
        {
            UserId = userId;
            UserDetectionStatus = userDetectionStatus;
        }

        public string UserId { get; set; }

        public UserDetectionStatus UserDetectionStatus { get; set; }

        public string? AuthorizationCode { get; set; }

        public string? IdToken { get;set; }

        public string? Email { get;set; }

        public PersonName? PersonName { get; set; }        
    }
}
