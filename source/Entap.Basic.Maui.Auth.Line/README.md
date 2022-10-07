# Entap.Basic.Maui.Auth.Line
[LINEログイン](https://developers.line.biz/ja/docs/line-login/)を行うライブラリです。
iOS:[LINE SDK for iOS](https://developers.line.biz/ja/docs/ios-sdk/)をバインディングした[LineSDK.NET.iOS](https://github.com/entap/Xamarin.LineSDK/tree/main/Xamarin.LineSDK/Xamarin.LineSDK.iOS)を利用します。  
Android:[LINE SDK for Android](https://developers.line.biz/ja/docs/android-sdk/)をバインディングした[LineSDK.NET.Android](https://github.com/entap/Xamarin.LineSDK/tree/main/Xamarin.LineSDK/Xamarin.LineSDK.Android)を利用します。

## 事前準備
[LINE Developers](https://developers.line.biz/ja/)のアカウントを作成し、Provider・Channelを作成し  
LINE Login settingsを登録してください。

## 導入方法

**初期化処理**
* App.cs
```csharp
	public App()
	{
		InitializeComponent();

		// 初期化処理（universalLinkURL：Universal Linkを利用しない場合は省略可能）
		Entap.Basic.Maui.Auth.Line.LineAuthService.Init([channelID], [universalLinkURL]);

		PageManager.Navigation.SetNavigationMainPage<MainPage>(new MainPageViewModel());
	}
```

* MauiProgram.cs : (LineLoginButton使用時)
```csharp
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureMauiHandlers((handlers) =>
			{
				handlers.AddHandler(typeof(LineLoginButton), typeof(LineLoginButtonHandler));
			});

		return builder.Build();
	}
```
**iOS**  
 ・[アプリをチャネルにリンクする](htt	ps://developers.line.biz/ja/docs/ios-sdk/swift/setting-up-project/#linking-app-to-channel)  
　※ユニバーサルリンクの[アプリとサーバーを関連づけ](https://developers.line.biz/ja/docs/ios-sdk/swift/universal-links-support/#ul-s1)については 、[Firebase Dynamic Links](https://firebase.google.com/docs/dynamic-links?hl=ja)の使用も可能です  
(シミュレータはサポート対象外)。    
・[Info.plistファイルを設定する](https://developers.line.biz/ja/docs/ios-sdk/swift/setting-up-project/#config-infoplist-file)  
・「Entap.Basic.Auth.Maui.Line」を追加してください。
・[アプリデリゲートを変更する](https://developers.line.biz/ja/docs/ios-sdk/swift/integrate-line-login/#modify-app-delegate)
```csharp
public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
{
  if (Entap.Basic.Maui.Auth.Line.LineAuthService.OpenUrl(application, url, options))
      return true;

  return base.OpenUrl(application, url, options);
}
```

・[アプリデリゲートを変更する](https://developers.line.biz/ja/docs/ios-sdk/swift/universal-links-support/#modify-app-delegate)（ユニバーサルリンクを利用時）
```csharp
public override bool ContinueUserActivity(UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
{
    if (Entap.Basic.Maui.Auth.Line.LineAuthService.ContinueUserActivity(application, userActivity, completionHandler))
        return true;

    return base.ContinueUserActivity(applicat	ion, userActivity, completionHandler);
}
```

・[シーンデリゲートを変更する](https://developers.line.biz/ja/docs/ios-sdk/swift/integrate-line-login/#modify-scene-delegates)（マルチウィンドウをサポートする場合）
```csharp
public override void OpenUrlContexts(UIScene scene, NSSet<UIOpenUrlContext> urlContexts)
{
    Entap.Basic.Maui.Auth.Line.OpenUrlContexts.OpenUrl(scene, urlContexts);
}
```

**Android**  
・[Androidマニフェストファイルを設定する](https://developers.line.biz/ja/docs/android-sdk/integrate-line-login/#setting-android-manifest-file])

・LineLoginButton使用時
```csharp
protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
{
    base.OnActivityResult(requestCode, resultCode, data);

    LineLoginButtonHandler.OnActivityResult(requestCode, resultCode, data);
}
```

## 使用方法
詳細は[サンプル](Sample)を参照してください。

### LineLoginButton使用時
以下いづれかの名前空間を指定してください。
```xml
xmlns:basic="http://entap.co.jp/schemas/basic"
xmlns:line="clr-namespace:Entap.Basic.Maui.Auth.Line;assembly=Entap.Basic.Maui.Auth.Line"
```
LoginScopesを指定してください。
```xml
<basic:LineLoginButton
    LoginScopes="OpenID, Profile, Email"
    Command="{Binding LineLoginCommand}"
/>
```
```csharp
public ProcessCommand<LoginResult> LineLoginCommand => new (async (loginResult) =>
{
    // ToDo : 認証処理
});
```

### LineLoginButton非使用時
LineAuthServiceを使用しログイン処理をしてください。
```csharp
var authService = new LineAuthService();
var loginResult = await authService.LoginAsync([LoginScope[]]);
```


## 補足説明
### iOS
・LineLoginButtonRendererでWeakLoginDelegateを設定してもDelegateが実行されない
→既存のクリック時処理を無効化し、独自にログイン処理を実行して回避。

### Android
・LineLoginButtonRendererでLoginDelegate、LoginListenerを設定してもLoginListenerが実行されない。
→MainActivity.OnActivityResult時に、ログイン結果を取得するよう変更して回避。
