using System;
namespace Entap.Basic.Maui.Core
{
    /// <summary>
    /// Thicknessのポジションを指定するフラグ
    /// </summary>
    [System.Flags]
    public enum ThicknessPositionFlags
    {
        Left = 1,
        Top = 2,
        Right = 4,
        Bottom = 8,

        Horizontal = 5,
        Vertical = 10,

        All = 15,

    }
}
