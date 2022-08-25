using System;
using Microsoft.Maui.Handlers;

#if __IOS__ || MACCATALYST
using PlatformView = UIKit.UIView;
#elif __ANDROID__
using PlatformView = Android.Views.View;
#elif WINDOWS
using PlatformView = Microsoft.UI.Xaml.FrameworkElement;
#elif TIZEN
using PlatformView = ElmSharp.EvasObject;
#elif (NETSTANDARD || !PLATFORM)
using PlatformView = System.Object;
#endif

namespace Entap.Basic.Maui.Core
{
    /// <summary>
    /// ハンドラーを使用してカスタムコントロール作成時、実装不要なプラットフォームに指定するダミーハンドラー
    /// ※実装不要なプラットフォームにもハンドラーを指定しないと、以下の例外が発生する。
    /// Microsoft.Maui.Platfrom.HandlerNotFoundException(Handler not found for view)
    /// </summary>
    public class DummyViewHandler : ViewHandler<IView, PlatformView>
    {
        public static IPropertyMapper<IView, DummyViewHandler> Mapper =
            new PropertyMapper<IView, DummyViewHandler>(ViewMapper)
            {
            };

        public DummyViewHandler() : base(Mapper, null)
        {
        }

        protected override PlatformView CreatePlatformView()
        {
#if ANDROID
            return new (Context);
#else
            return new();
#endif
        }
    }
}

