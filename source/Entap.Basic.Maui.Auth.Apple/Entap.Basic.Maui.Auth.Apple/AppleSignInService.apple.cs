using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Linq;
using AuthenticationServices;
using Foundation;
using UIKit;

namespace Entap.Basic.Maui.Auth.Apple
{
    public class AppleSignInService : NSObject, IASAuthorizationControllerDelegate, IASAuthorizationControllerPresentationContextProviding
    {
        static ASAuthorizationScope[]? _scopes;
        static bool _isInitialized;

        TaskCompletionSource<ASAuthorizationAppleIdCredential>? _tcsCredential;

        public static bool IsSupported =>
            (OperatingSystem.IsIOS() &&
             OperatingSystem.IsIOSVersionAtLeast(13, 0)) ||
            (OperatingSystem.IsMacCatalyst() &&
             OperatingSystem.IsMacCatalystVersionAtLeast(13, 1));

        public static void Init(params AuthorizationScope[]? scopes)
        {
            PlatformInit(scopes?.ToASAuthorizationScopes());
        }

        public static void PlatformInit(params ASAuthorizationScope[]? scopes)
        {
            _scopes = scopes;
            _isInitialized = true;
        }

        public AppleSignInService()
        {
        }


        public async Task<AppleIdCredential> SignInAsync()
        {
            var credential = await GetCredential();
            return credential.ToAppleIdCredential();
        }

        public async Task<ASAuthorizationAppleIdCredential> GetCredential()
        {
            if (IsSupported)
                throw new NotSupportedException();

            if (!_isInitialized)
                throw new InvalidOperationException($"Please call {nameof(AppleSignInService.Init)} method.");

            var appleIdProvider = new ASAuthorizationAppleIdProvider();
            var request = appleIdProvider.CreateRequest();
            request.RequestedScopes = _scopes;

            var authorizationController = new ASAuthorizationController(new[] { request })
            {
                Delegate = this,
                PresentationContextProvider = this
            };
            authorizationController.Delegate = this;
            authorizationController.PerformRequests();

            _tcsCredential = new TaskCompletionSource<ASAuthorizationAppleIdCredential>();
            var creds = await _tcsCredential.Task;

            return creds;
        }

        #region IASAuthorizationController Delegate

        [Export("authorizationController:didCompleteWithAuthorization:")]
        public void DidComplete(ASAuthorizationController controller, ASAuthorization authorization)
        {
            var creds = authorization.GetCredential<ASAuthorizationAppleIdCredential>();
            if (creds is null)
            {
                _tcsCredential?.SetException(new FormatException("Credential is not ASAuthorizationAppleIdCredential"));
                return;
            }

            if (creds.IdentityToken is not null &&
                (_scopes?.Any((scope) => scope == ASAuthorizationScope.Email) == true) &&
                creds.Email is null)
            {
                var jwt = NSString.FromData(creds.IdentityToken, NSStringEncoding.UTF8);
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);
                if (token.Payload.TryGetValue("email", out var email))
                    creds.SetValueForKey(new NSString(email.ToString()), new NSString(nameof(creds.Email)));
            }
            _tcsCredential?.TrySetResult(creds);
        }

        [Export("authorizationController:didCompleteWithError:")]
        public void DidComplete(ASAuthorizationController controller, NSError error)
        {
            if (error.Code == (int)ASAuthorizationError.Canceled)
                _tcsCredential?.SetException(new OperationCanceledException());
            else
                _tcsCredential?.SetException(new NSErrorException(error));
        }

        #endregion

        #region IASAuthorizationControllerPresentation Context Providing
        public UIWindow GetPresentationAnchor(ASAuthorizationController controller)
        {
            return UIApplication.SharedApplication.KeyWindow ?? new UIWindow();
        }
        #endregion

        public static async Task<ASAuthorizationAppleIdProviderCredentialState> GetCredentialStateAsync(string userId)
        {
            if (IsSupported)
                throw new NotSupportedException();

            var appleIdProvider = new ASAuthorizationAppleIdProvider();
            var credentialState = await appleIdProvider.GetCredentialStateAsync(userId);
            return credentialState;
        }

        /// <summary>
        /// AppleID使用停止時の処理を登録
        /// </summary>
        public static async Task RegisterCredentialRevokedActionAsync(string? userId, Action action)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var status = await GetCredentialStateAsync(userId);
                if (status == ASAuthorizationAppleIdProviderCredentialState.Revoked)
                    action.Invoke();
            }
            AddCredentialRevokedObserver(action);
        }

        /// <summary>
        /// アプリ起動中のAppleID使用停止時の処理を登録
        /// </summary>
        static void AddCredentialRevokedObserver(Action action)
        {
            var center = NSNotificationCenter.DefaultCenter;
            center.AddObserver(ASAuthorizationAppleIdProvider.CredentialRevokedNotification, (_) =>
            {
                action.Invoke();
            });
        }
    }
}