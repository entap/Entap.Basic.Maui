using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Entap.Basic.Maui.Core
{
	/// <summary>
	/// プロセスの多重実行を制御するため実行状態を管理する
	/// </summary>
    public class ProcessManager
    {
        /// <summary>
        /// 現在のProcessManagerを取得
        /// </summary>
        public static ProcessManager Current => instance;
        private static readonly ProcessManager instance = new ProcessManager();

        /// <summary>
        /// プロセスの実行状態
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// 実行中プロセス名
        /// </summary>
        public string? RunningProcessName { get; private set; }

        /// <summary>
        /// プロセスを実行する
        /// </summary>
        /// <param name="processName">プロセス名</param>
        /// <param name="action">同期処理</param>
        public void Invoke(string processName, Action action)
        {
            bool started = false;
            try
            {
                started = OnStart(processName);
                if (started)
                    action();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Warning]Exception occured.{Environment.NewLine}  ProcessName : {RunningProcessName}{Environment.NewLine} Exception : {ex.Message}");
                throw;
            }
            finally
            {
                if (started)
                    OnComplete();
            }
        }

        /// <summary>
        /// プロセスを実行する
        /// </summary>
        /// <param name="action">同期処理</param>
        /// <param name="memberName">メンバー名</param>
        /// <param name="sourceFilePath">実行元ファイルパス</param>
        /// <param name="sourceLineNumber">行数</param>
        public void Invoke(Action action, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            Invoke(
                GetProcessName(memberName, sourceFilePath, sourceLineNumber),
                action);
        }

        /// <summary>
        /// プロセスを実行する
        /// </summary>
        /// <param name="processName">プロセス名</param>
        /// <param name="funcTask">非同期処理</param>
        public async Task Invoke(string processName, Func<Task> funcTask)
        {
            bool started = false;
            try
            {
                started = OnStart(processName);
                if (started)
                    await funcTask()
                        .ContinueWith((arg) =>
                        {
                            if (arg.IsFaulted)
                                Debug.WriteLine($"[Warning]Exception occured.{Environment.NewLine}  ProcessName : {processName}{Environment.NewLine} Exception : {arg.Exception}");
                        })
                        .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Warning]Exception occured.{Environment.NewLine}  ProcessName : {RunningProcessName}{Environment.NewLine} Exception : {ex.Message}");
                throw;
            }
            finally
            {
                if (started)
                    OnComplete();
            }
        }

        /// <summary>
        /// プロセスを実行する
        /// </summary>
        /// <param name="funcTask">非同期処理</param>
        /// <param name="memberName">メンバー名</param>
        /// <param name="sourceFilePath">実行元ファイルパス</param>
        /// <param name="sourceLineNumber">行数</param>
        public Task Invoke(Func<Task> funcTask, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            return Invoke(
                GetProcessName(memberName, sourceFilePath, sourceLineNumber),
                funcTask);
        }

        /// <summary>
        /// プロセス実行時処理
        /// </summary>
        /// <param name="processName">プロセス名</param>
        bool OnStart(string processName)
        {
            if (IsRunning)
            {
                Debug.WriteLine($"[Warning]Process is Running : {RunningProcessName}");
                return false;
            }

            IsRunning = true;
            RunningProcessName = processName;
            Debug.WriteLine($"[Trace]Start Process : {RunningProcessName}");
            return true;
        }

        /// <summary>
        /// プロセス完了時処理
        /// </summary>
        public void OnComplete()
        {
            Debug.WriteLine($"[Trace]Process Completed : {RunningProcessName}");
            IsRunning = false;
            RunningProcessName = null;
        }

        /// <summary>
        /// プロセス名取得
        /// </summary>
        /// <param name="memberName">メンバー名</param>
        /// <param name="sourceFilePath">実行元ファイルパス</param>
        /// <param name="sourceLineNumber">行数</param>
        /// <returns>プロセス名</returns>
        public static string GetProcessName(string memberName, string sourceFilePath, int sourceLineNumber)
            => $"{sourceFilePath} L:{sourceLineNumber} {memberName}";
    }
}

