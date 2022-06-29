using System;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace Entap.Basic.Maui.Core
{
    /// <summary>
    /// Xamarin.Forms.Thicknessの拡張メソッド
    /// </summary>
    public static class ThicknessExtentions
    {
        /// <summary>
        /// SafeAreaを適用したThicknessを取得する
        /// </summary>
        /// <param name="thickness">SafeAreaを適用するThickness</param>
        /// <param name="positionFlags">SafeAreaを適用する位置</param>
        /// <returns>SafeAreaを適用したThickness</returns>
        public static Thickness? GetSafeAreaAppliedThickness(this Thickness thickness, Thickness safeArea, ThicknessPositionFlags positionFlags)
        {
            if (!OperatingSystem.IsIOS()) return null;
            if (positionFlags == 0) return null;
            if (safeArea.IsEmpty) return null;

            var result = new Thickness(thickness.Left, thickness.Top, thickness.Right, thickness.Bottom);
            if (positionFlags.HasFlag(ThicknessPositionFlags.Left))
                result.Left += safeArea.Left;

            if (positionFlags.HasFlag(ThicknessPositionFlags.Top))
                result.Top += safeArea.Top;

            if (positionFlags.HasFlag(ThicknessPositionFlags.Right))
                result.Right += safeArea.Right;

            if (positionFlags.HasFlag(ThicknessPositionFlags.Bottom))
                result.Bottom += safeArea.Bottom;

            return result;
        }
    }
}
