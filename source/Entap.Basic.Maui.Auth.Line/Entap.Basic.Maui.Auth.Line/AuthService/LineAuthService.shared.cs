using System;
using System.Threading.Tasks;

namespace Entap.Basic.Maui.Auth.Line
{
    public partial class LineAuthService
    {
        public Task<LoginResult> LoginAsync(params LoginScope[] scopes) => PlatformLoginAsync(scopes);
    }
}
