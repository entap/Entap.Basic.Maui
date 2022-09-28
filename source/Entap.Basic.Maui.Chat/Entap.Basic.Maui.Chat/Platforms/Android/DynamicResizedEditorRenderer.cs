using System;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using System.ComponentModel;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Platform;
using Window = Android.Views.Window;
using Android.Runtime;
using Android.Graphics;

namespace Entap.Basic.Maui.Chat.Platforms.Android
{
    [Obsolete]
    public class DynamicResizedEditorRenderer : EditorRenderer
    {
        const int HorizontalPadding = 12;
        DynamicResizedEditor? _dynamicResizedEditor;
        SoftInput? _startingMode;

        public DynamicResizedEditorRenderer(Context context) : base(context)
        {

            var window = Context?.GetActivity()?.Window;
            if (window is null) return;
            _startingMode = window.Attributes?.SoftInputMode;
            window.SetSoftInputMode(SoftInput.AdjustResize);
        }

        protected override void OnFocusChanged(bool gainFocus, [GeneratedEnum] FocusSearchDirection direction, global::Android.Graphics.Rect? previouslyFocusedRect)
        {
            var window = Context?.GetActivity()?.Window;
            if (window is null) return;

            if (gainFocus)
            {
                _startingMode = window.Attributes?.SoftInputMode;
                window.SetSoftInputMode(SoftInput.AdjustResize);
            }
            else
            {
                if (_startingMode is not null)
                    window.SetSoftInputMode(_startingMode.Value);
            }

            base.OnFocusChanged(gainFocus, direction, previouslyFocusedRect);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null && e.NewElement != null)
            {
                _dynamicResizedEditor = e.NewElement as DynamicResizedEditor;
                if (_dynamicResizedEditor is null) return;

                Control.TextChanged += OnTextChanged;

                var border = new GradientDrawable();
                border.SetCornerRadius(_dynamicResizedEditor.CornerRadius * DensityF); //角丸
                border.SetColor(_dynamicResizedEditor.BackgroundColor.ToAndroid());
                this.Control.SetBackground(border);

                // SetColorで背景色指定後BackgroundColorをTransparentにしておかないと角丸が見えなくなる
                _dynamicResizedEditor.BackgroundColor = Colors.Transparent;

                _dynamicResizedEditor.Focused += OnFocused;
                _dynamicResizedEditor.Unfocused += OnUnFocused;
                SetOneLineSize();
                
                SetMaxLines();
            }
        }

        protected override void OnDraw(Canvas? canvas)
        {
            base.OnDraw(canvas);
            SetPadding();
        }

        float DensityF => (float)DeviceDisplay.Current.MainDisplayInfo.Density;

        private void OnTextChanged(object? sender, global::Android.Text.TextChangedEventArgs e)
        {
            var lineCount = Control.LineCount;
            if (lineCount <= 1)
            {
                SetOneLineSize();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (_dynamicResizedEditor is null) return;

            // 下記処理ないと文字色変わらなかった
            if (e.PropertyName == DynamicResizedEditor.TextColorProperty.PropertyName)
            {
                Control.SetTextColor(_dynamicResizedEditor.TextColor.ToAndroid());
            }
            else if (e.PropertyName == DynamicResizedEditor.HeightRequestProperty.PropertyName)
            {
                // エディターのカーソルを末尾に
                Control.SetSelection(Control?.Text?.Length ?? 0);
            }
            else if (e.PropertyName == DynamicResizedEditor.MaxDisplayLineCountProperty.PropertyName)
                SetMaxLines();
        }

        void OnFocused(object? sender, FocusEventArgs e)
        {
            // エディターのカーソルを末尾に
            Device.BeginInvokeOnMainThread(() =>
            {
                Control.SetSelection(Control?.Text?.Length ?? 0);

                if (Control?.LineCount <= 1)
                {
                    SetOneLineSize();
                }
            });
        }

        void OnUnFocused(object? sender, FocusEventArgs e)
        {
            SetOneLineSize();
        }

        void SetOneLineSize()
        {
            if (_dynamicResizedEditor is null) return;
            _dynamicResizedEditor.HeightRequest = _dynamicResizedEditor.MinimumHeightRequest;
        }

        bool isSetPadding;
        void SetPadding()
        {
            if (isSetPadding) return;
            if (Control.Height <= 0) return;

            var verticalPadding = (Control.Height - Control.LineHeight * Math.Min(Control.MaxLines, Control.LineCount)) / 2;
            Control.SetPadding(HorizontalPadding, verticalPadding, HorizontalPadding, verticalPadding);
            isSetPadding = true;
        }

        void SetMaxLines()
        {
            if (Control is null) return;
            if (_dynamicResizedEditor is null) return;

            Control.SetMaxLines(_dynamicResizedEditor.MaxDisplayLineCount);
        }
    }
}