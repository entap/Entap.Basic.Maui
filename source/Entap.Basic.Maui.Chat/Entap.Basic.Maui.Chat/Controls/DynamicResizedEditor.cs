using System;
namespace Entap.Basic.Maui.Chat
{
	public class DynamicResizedEditor : Editor
	{
		public DynamicResizedEditor()
		{
            AutoSize = EditorAutoSizeOption.TextChanges;
		}

        public event EventHandler Resized = delegate { };

        public void Resize(int lineCount)
        {
            if (OperatingSystem.IsIOS())
                UpdateAutoSize(lineCount);

            //// リサイズ通知
            Resized?.Invoke(this, EventArgs.Empty);
        }

        void UpdateAutoSize(int lineCount)
        {
            if (lineCount <= MaxDisplayLineCount)
                AutoSize = EditorAutoSizeOption.TextChanges;
            else
                AutoSize = EditorAutoSizeOption.Disabled;
        }

        #region CornerRadius BindableProperty
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(DynamicResizedEditor), 0);

        public int CornerRadius
        {
            get { return (int)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        #endregion

        #region MaxDisplayLineCount BindableProperty
        public static readonly BindableProperty MaxDisplayLineCountProperty =
            BindableProperty.Create(nameof(MaxDisplayLineCount), typeof(int), typeof(DynamicResizedEditor), 4);

        public int MaxDisplayLineCount
        {
            get { return (int)GetValue(MaxDisplayLineCountProperty); }
            set { SetValue(MaxDisplayLineCountProperty, value); }
        }
        #endregion
    }
}

