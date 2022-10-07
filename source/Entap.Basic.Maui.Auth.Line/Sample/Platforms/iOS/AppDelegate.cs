using Foundation;
using UIKit;

namespace Sample;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override bool OpenUrl(UIApplication application, NSUrl url, NSDictionary options)
    {
        if (Entap.Basic.Maui.Auth.Line.LineAuthService.OpenUrl(application, url, options))
            return true;

        return base.OpenUrl(application, url, options);
    }
}

