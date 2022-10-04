using System;
using System.Threading.Tasks;

namespace Entap.Basic.Maui.Auth.Line
{
    internal interface ILineAuthService
    {
        Task<LoginResult> PlatformLoginAsync(params LoginScope[] scopes);
    }
}
