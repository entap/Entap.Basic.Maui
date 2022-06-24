using System;
namespace Entap.Basic.Maui.Core
{
    public interface IDisplaySizeService
    {
        double GetStatusBarHeight();
        double GetTopNavigationHeight();
        double GetAndroidNavigationBarHeight();
    }
}