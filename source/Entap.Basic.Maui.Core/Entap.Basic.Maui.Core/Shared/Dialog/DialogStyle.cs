using System;
namespace Entap.Basic.Maui.Core
{
    /// <summary>
    /// ダイアログのスタイル
    /// </summary>
	public class DialogStyle
	{
        /// <summary>
        /// DialogItemStyle.Default 時の文字色
        /// </summary>
        public Color? DefaultTextColor { get; set; }

        /// <summary>
        /// DialogItemStyle.Cancel 時の文字色
        /// </summary>
        public Color? CancelTextColor { get; set; }

        /// <summary>
        /// DialogItemStyle.Destructive 時の文字色
        /// </summary>
        public Color? DestructiveTextColor { get; set; }
    }
}

