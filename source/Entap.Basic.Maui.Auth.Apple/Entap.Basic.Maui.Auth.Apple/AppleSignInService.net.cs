using System;
using System.Threading.Tasks;

namespace Entap.Basic.Maui.Auth.Apple
{
	public class AppleSignInService
	{
        public static bool IsSupported => false;

        internal static void Init(params AuthorizationScope[]? scopes)
        {
            throw new NotImplementedException();
        }

        public Task<AppleIdCredential> SignInAsync()
        {
            throw new NotImplementedException();
        }

        public static Task RegisterCredentialRevokedActionAsync(Action action, string? userId = null)
        {
            throw new NotImplementedException();
        }
    }
}

