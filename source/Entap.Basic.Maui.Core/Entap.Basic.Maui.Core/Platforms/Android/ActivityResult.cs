using System;
using Android.App;
using Android.Content;

namespace Entap.Basic.Maui.Core.Android
{
    public class ActivityResult
    {
        public ActivityResult(Result resultCode, Intent? data)
        {
            ResultCode = resultCode;
            Data = data;
        }

        public Result ResultCode { get; private set; }

        public Intent? Data { get; private set; }
    }
}

