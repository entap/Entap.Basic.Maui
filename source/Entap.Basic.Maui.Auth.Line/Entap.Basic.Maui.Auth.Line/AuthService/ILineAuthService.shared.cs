using System;
using System.Threading.Tasks;

namespace Entap.Basic.Auth.Line
{
    internal interface ILineAuthService
    {
        Task<LoginResult> PlatformLoginAsync(params LoginScope[] scopes);
    }
}
