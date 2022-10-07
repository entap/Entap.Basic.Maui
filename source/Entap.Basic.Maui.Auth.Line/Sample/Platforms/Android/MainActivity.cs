using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Entap.Basic.Maui.Auth.Line;

namespace Sample;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{

    protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
    {
        base.OnActivityResult(requestCode, resultCode, data);
        LineLoginButtonHandler.OnActivityResult(requestCode, resultCode, data);
    }
}

