using System;
namespace Entap.Basic.Maui.Core
{
    /// <summary>
    /// ダイアログアイテム
    /// </summary>
	public class DialogItem
	{
        /// <param name="text">テキスト</param>
        /// <param name="itemStyle">スタイル</param>
        public DialogItem(string text, DialogItemStyle itemStyle = DialogItemStyle.Default)
        {
            Text = text;
            Style = itemStyle;
        }

        /// <summary>
        /// テキスト
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// スタイル
        /// </summary>
        public DialogItemStyle Style { get; set; }
    }
}

