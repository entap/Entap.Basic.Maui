using System;
using Foundation;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Controls.Platform;

namespace Entap.Basic.Maui.Chat.Platforms.iOS
{
    [Obsolete]
    public class DynamicResizedEditorRenderer : EditorRenderer
    {
        DynamicResizedEditor? _dynamicResizedEditor;
        int _lineCount = 1;

        public DynamicResizedEditorRenderer()
		{
		}

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null && e.NewElement != null)
            {
                _dynamicResizedEditor = e.NewElement as DynamicResizedEditor;
                if (_dynamicResizedEditor is null) return;

                this.Control.Layer.CornerRadius = _dynamicResizedEditor.CornerRadius;
                this.Control.Layer.MasksToBounds = true;
                this.Control.BackgroundColor = _dynamicResizedEditor.BackgroundColor?.ToUIColor() ?? UIKit.UIColor.Clear;

                // 初期はスクロールを封じる。
                Control.ScrollEnabled = false;
                Control.Changed += OnChanged;
                _dynamicResizedEditor.Focused += OnFocused;
                _dynamicResizedEditor.Unfocused += OnUnFocused;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (_dynamicResizedEditor is null) return;
            if (e.PropertyName == DynamicResizedEditor.TextProperty.PropertyName)
            {
                ResizeIfNeeded();
            }
            if (e.PropertyName == DynamicResizedEditor.HeightRequestProperty.PropertyName)
            {
                if (_dynamicResizedEditor.HeightRequest <= _dynamicResizedEditor.MinimumHeightRequest)
                {
                    // エディターが入力状態でない時
                    Control.ScrollsToTop = true;
                    Control.ScrollEnabled = false;
                }
                else
                {
                    EditorCursorMoveEnd();
                }
            }
        }
        void EditorCursorMoveEnd()
        {
            if (Control is null) return;
            Task.Run(async () =>
            {
                await Task.Delay(250);
                // エディターのカーソルを末尾に
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (Control.Text?.Length > 0)
                    {
                        var loc = Control.Text.Length - 1;
                        NSRange range = new NSRange(0, Control.Text.Length);
                        Control.ScrollRangeToVisible(range);
                    }
                    Control.SelectedTextRange = Control.GetTextRange(Control.EndOfDocument, Control.EndOfDocument);
                });
            });
        }

        void OnChanged(object? sender, EventArgs e)
        {
            ResizeIfNeeded();
        }

        void OnFocused(object? sender, FocusEventArgs e)
        {
            if (_dynamicResizedEditor is null) return;

            _dynamicResizedEditor.HeightRequest = -1;
            Control.ScrollEnabled = true;
            ResizeIfNeeded();
            EditorCursorMoveEnd();
        }

        void OnUnFocused(object? sender, FocusEventArgs e)
        {
            if (_dynamicResizedEditor is null) return;

            _dynamicResizedEditor.HeightRequest = _dynamicResizedEditor.MinimumHeightRequest;
            Control.ScrollEnabled = false;
        }

        void ResizeIfNeeded()
        {
            if (_dynamicResizedEditor is null) return;

            var fitHeight = Control.SizeThatFits(new CoreGraphics.CGSize(Control.Bounds.Width, NFloat.MaxValue)).Height;
            var lineCount = CalculateLineCount(fitHeight);
            if (lineCount != _lineCount)
            {
                _dynamicResizedEditor.Resize(lineCount);
                Control.ScrollEnabled = (lineCount > _dynamicResizedEditor.MaxDisplayLineCount);
                _lineCount = lineCount;
            }
        }

        int CalculateLineCount(NFloat fitHeight)
        {
            if (Control is null) return 0;
            if (Control.Font is null) return 0;

            var fontHeight = Control.Font.LineHeight;
            return (int)Math.Round(fitHeight / fontHeight);
        }

    }
}

