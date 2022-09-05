# Entap.Basic.Maui.Auth.Apple
[Sign in with Apple](https://developer.apple.com/jp/sign-in-with-apple/)をMAUIで使用可能にするライブラリ
※対象：iOS13以降、MacCatalyst13.1以降

## 事前準備
「Sign In with Apple」機能を有効にする  
[https://help.apple.com/developer-account/?lang=ja#/devde676e696](https://help.apple.com/developer-account/?lang=ja#/devde676e696)

## 導入方法  
* 「Entap.Basic.Maui.Auth.Apple」をインストールする

## 使用方法
### 初期化処理
必要に応じてスコープを指定する
```csharp
	public App()
	{
		InitializeComponent();

        Entap.Basic.Maui.Auth.Apple.AppleSignInService.Init(
            Entap.Basic.Maui.Auth.Apple.AuthorizationScope.Email,
            Entap.Basic.Maui.Auth.Apple.AuthorizationScope.FullName
        );

        MainPage = new AppShell();
	}
```
### 認証ボタン
ボタンのタイプ・スタイルは変更可能。
詳細は、[サンプル](https://github.com/entap/Entap.Basic.Maui/tree/main/source/Entap.Basic.Maui.Auth.Apple/Sample/Sample)参照
```xml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:basic="http://entap.co.jp/schemas/basic"
             x:Class="Sample.MainPage"
             BackgroundColor="LightGray"
>
    <StackLayout>
        <basic:AppleSignInButton
            Command="{Binding AuthCommand}"
        />
    </StackLayout>
</ContentPage>
```

### 認証処理
必要に応じて取得結果をKeyChain等に保存すること。  
* 同一アカウントでの2回目の認証以降、Appleの使用上FullNameが取得できない  
(Emailもはライブラリ内でトークンから補完する)  
* 後述の[AppleId使用停止時処理](#appleid使用停止時処理)を使用時、AppleIdCredential.UserIdが必要になる
```csharp
    async Task AppleSignInAsync()
	{
        try
        {
            var service = new Entap.Basic.Maui.Auth.Apple.AppleSignInService();
            var result = await service.SignInAsync();
            // ToDo : 認証処理
        }
        catch (InvalidOperationException ex)
        {
            // 初期化未済
        }
        catch (NotSupportedException ex)
        {
            // サポート外
        }
        catch (OperationCanceledException ex)
        {
            // ユーザによるキャンセル
        }
        catch (Exception ex)
        {
            // 認証エラー
            System.Diagnostics.Debug.WriteLine(ex);
        }
    }
```

### AppleId使用停止時処理
AppleId使用停止時に再認証等を行う必要がある場合に、使用停止時処理を登録する。
action:AppleId使用停止時に実行したい処理を指定。
userId:認証済みのユーザIDを任意で指定（認証ステータスを取得し、無効化時はactionが実行される）
```csharp
    if  (Entap.Basic.Maui.Auth.Apple.AppleSignInService.IsSupported)
    {
        Entap.Basic.Maui.Auth.Apple.AppleSignInService.RegisterCredentialRevokedActionAsync(() =>
        {
            System.Diagnostics.Debug.WriteLine("CredentialRevoked");
        });
    }
```