using System;
using System.Collections.Concurrent;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace Entap.Basic.Maui.Core.Platforms.Android
{
    [Activity(ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class StarterActivity : Activity
    {
        const string launchedExtra = "launched";
        const string actualIntentExtra = "actual_intent";
        const string guidExtra = "guid";
        const string requestCodeExtra = "request_code";

        static readonly ConcurrentDictionary<string, IntentTask> pendingTasks =
            new ConcurrentDictionary<string, IntentTask>();

        bool launched;
        Intent? actualIntent;
        string? guid;
        int requestCode;

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var extras = savedInstanceState ?? Intent?.Extras;
            if (extras is null) return;

#pragma warning disable CS8602 // null 参照の可能性があるものの逆参照です。
            launched = extras.GetBoolean(launchedExtra, false);
            actualIntent = extras.GetParcelable(actualIntentExtra) as Intent;
            guid = extras.GetString(guidExtra);
            requestCode = extras.GetInt(requestCodeExtra, -1);
#pragma warning restore CS8602 // null 参照の可能性があるものの逆参照です。

            if (!launched)
                StartActivityForResult(actualIntent, requestCode);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutBoolean(launchedExtra, true);

            outState.PutParcelable(actualIntentExtra, actualIntent);
            outState.PutString(guidExtra, guid);
            outState.PutInt(requestCodeExtra, requestCode);

            base.OnSaveInstanceState(outState);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent? data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            var task = GetIntentTask(guid, true);
            if (task is null)
            {
                Finish();
                return;
            }

            task.TaskCompletionSource.TrySetResult(new ActivityResult(resultCode, data));
            Finish();
        }

        /// <summary>
        /// 指定したIntentを起動し、結果を返す
        /// キャンセル時はTaskCanceledExceptionとする
        /// </summary>
        /// <returns>Intent</returns>
        public static async Task<Intent?> StartAsync(Activity activity, Intent intent, int requestCode)
        {
            var result = await StartActivityForResult(activity, intent, requestCode);
            if (result.ResultCode == Result.Canceled)
                throw new TaskCanceledException();

            return result.Data;
        }

        /// <summary>
        /// 指定したIntentを起動し、結果を返す
        /// </summary>
        /// <returns>ActivityResult</returns>
        public static Task<ActivityResult> StartForResultAsync(Activity activity, Intent intent, int requestCode)
        {
            return StartActivityForResult(activity, intent, requestCode);
        }

        static Task<ActivityResult> StartActivityForResult(Activity activity, Intent intent, int requestCode)
        {
            var data = new IntentTask();
            pendingTasks[data.Id] = data;

            var intermediateIntent = new Intent(activity, typeof(StarterActivity));
            intermediateIntent.PutExtra(actualIntentExtra, intent);
            intermediateIntent.PutExtra(guidExtra, data.Id);
            intermediateIntent.PutExtra(requestCodeExtra, requestCode);

            activity.StartActivityForResult(intermediateIntent, requestCode);

            return data.TaskCompletionSource.Task;
        }

        static IntentTask? GetIntentTask(string? guid, bool remove = false)
        {
            if (string.IsNullOrEmpty(guid))
                return null;

            if (remove)
            {
                pendingTasks.TryRemove(guid, out var removedTask);
                return removedTask;
            }

            pendingTasks.TryGetValue(guid, out var task);
            return task;
        }

        class IntentTask
        {
            public IntentTask()
            {
                Id = Guid.NewGuid().ToString();
                TaskCompletionSource = new TaskCompletionSource<ActivityResult>();
            }

            public string Id { get; }

            public TaskCompletionSource<ActivityResult> TaskCompletionSource { get; }
        }
    }
}