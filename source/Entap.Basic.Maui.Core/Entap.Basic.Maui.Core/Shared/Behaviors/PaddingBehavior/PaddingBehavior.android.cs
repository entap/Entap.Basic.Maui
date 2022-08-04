using System;
namespace Entap.Basic.Maui.Core
{
    public partial class PaddingBehavior
    {
        global::Android.Views.View? _platformView;

        protected override void OnAttachedTo(View bindable, global::Android.Views.View platformView)
        {
            base.OnAttachedTo(bindable, platformView);
            _platformView = platformView;
            PlatformUpdatePadding(Padding);
        }

        protected override void OnDetachedFrom(View bindable, global::Android.Views.View platformView)
        {
            base.OnDetachedFrom(bindable, platformView);
        }

        void PlatformUpdatePadding(Thickness padding)
        {
            if (_platformView is null) return;

            var density = DisplaySizeManager.Density;
            _platformView.SetPadding(
                (int)(padding.Left * density),
                (int)(padding.Top * density),
                (int)(padding.Right * density),
                (int)(padding.Bottom * density));
        }
    }
}

