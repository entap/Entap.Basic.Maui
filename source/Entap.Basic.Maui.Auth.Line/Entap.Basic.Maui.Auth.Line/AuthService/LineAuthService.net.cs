using System;
using System.Threading.Tasks;

namespace Entap.Basic.Maui.Auth.Line
{
    public partial class LineAuthService : ILineAuthService
    {
        public Task<LoginResult> PlatformLoginAsync(params LoginScope[] scopes)
        {
            throw new NotSupportedException();
        }
    }
}
