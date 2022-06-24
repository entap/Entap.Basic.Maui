using System;
using System.ComponentModel;

namespace Entap.Basic.Maui.Core
{
    /// <summary>
    /// </summary>
    public class DisplaySizeManager
    {
        static IDisplaySizeService? _displaySizeService;
        static IDisplaySizeService _displaySizeServiceInstance => _displaySizeService ??=
#if ANDROID
            new Entap.Basic.Maui.Core.Android.DisplaySizeService();
#elif IOS
            new Entap.Basic.Maui.Core.iOS.DisplaySizeService();
#else
        new DisplaySizeService();
#endif

        /// <summary>
        /// 画面のサイズ(ナビゲーションバーやステータスバーの高さを含む)
        /// </summary>
        public static Size ScreenSize =>
            new (
                DeviceDisplay.Current.MainDisplayInfo.Width / DeviceDisplay.Current.MainDisplayInfo.Density,
                DeviceDisplay.Current.MainDisplayInfo.Height / DeviceDisplay.Current.MainDisplayInfo.Density
            );

        public static double StatusBarHeight =>
            _displaySizeServiceInstance.GetStatusBarHeight();

        public static double Density => DeviceDisplay.Current.MainDisplayInfo.Density;


        /// <summary>
        /// Androidのナビゲーションバー（画面下部の 戻る/ホーム/タスク管理 ）の高さ
        /// https://www.android.com/intl/ja_jp/articles/51/
        /// </summary>
        public static double AndroidBottomNavigationBarHeight =>
#if ANDROID
            _displaySizeServiceInstance.GetAndroidNavigationBarHeight();
#else
            0;
#endif


        /// <summary>
        /// <para>注意</para>
        /// <para>iOSの場合、各ページのコードビハインドやViewModelのコンストラクタ内では使用せず、iOSDisplaySizeChangedの方を使う</para>
        /// </summary>
        public static double TopNavigationHeight =>
            _displaySizeServiceInstance.GetTopNavigationHeight();
    }
}
