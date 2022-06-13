using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Entap.Basic.Maui.Core
{
    /// <summary>
    /// 処理の多重実行を制御するコマンド
    /// </summary>
    public sealed class ProcessCommand<T> : Command
    {
        #region ProcessManagerによる制御
        /// <summary>
        /// ProcessCommand クラスの新しいインスタンスを初期化する（canExecute指定なし）
        /// </summary>
        /// <param name="processManager">プロセスマネージャ</param>
        /// <param name="execute">コマンドの実行時に実行する関数</param>
        /// <param name="memberName">コマンド呼び出し元のメンバ名</param>
        /// <param name="sourceFilePath">コマンド呼び出し元のファイルパス</param>
        /// <param name="sourceLineNumber">コマンド呼び出し元の行数</param>
        public ProcessCommand(
            ProcessManager processManager,
            Func<T, Task> execute,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            : base(arg =>
            {
                processManager.Invoke(
                    ProcessManager.GetProcessName(memberName, sourceFilePath, sourceLineNumber),
                    () => execute((T)arg))
                .ConfigureAwait(false);
            })
        {
        }

        /// <summary>
        /// ProcessCommand クラスの新しいインスタンスを初期化する（canExecute指定あり）
        /// </summary>
        /// <param name="processManager">プロセスマネージャ</param>
        /// <param name="execute">コマンドの実行時に実行する関数</param>
        /// <param name="canExecute">コマンドが実行できるかどうかを示す</param>
        /// <param name="memberName">コマンド呼び出し元のメンバ名</param>
        /// <param name="sourceFilePath">コマンド呼び出し元のファイルパス</param>
        /// <param name="sourceLineNumber">コマンド呼び出し元の行数</param>
        public ProcessCommand(
            ProcessManager processManager,
            Func<T, Task> execute,
            Func<T, bool> canExecute,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            : base(arg =>
            {
                processManager.Invoke(
                    ProcessManager.GetProcessName(memberName, sourceFilePath, sourceLineNumber),
                    () => execute((T)arg))
                .ConfigureAwait(false);
            }, arg => canExecute((T)arg))
        {
        }
        #endregion

        #region ProcessManager.Currentによる制御
        /// <summary>
        /// ProcessCommand クラスの新しいインスタンスを初期化する（canExecute指定なし）
        /// </summary>
        /// <param name="execute">コマンドの実行時に実行する関数</param>
        /// <param name="memberName">コマンド呼び出し元のメンバ名</param>
        /// <param name="sourceFilePath">コマンド呼び出し元のファイルパス</param>
        /// <param name="sourceLineNumber">コマンド呼び出し元の行数</param>
        public ProcessCommand(
            Func<T, Task> execute,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            : this(ProcessManager.Current, execute, memberName, sourceFilePath, sourceLineNumber)
        {
        }

        /// <summary>
        /// ProcessCommand クラスの新しいインスタンスを初期化する（canExecute指定あり）
        /// </summary>
        /// <param name="execute">コマンドの実行時に実行する関数</param>
        /// <param name="canExecute">コマンドが実行できるかどうかを示す</param>
        /// <param name="memberName">コマンド呼び出し元のメンバ名</param>
        /// <param name="sourceFilePath">コマンド呼び出し元のファイルパス</param>
        /// <param name="sourceLineNumber">コマンド呼び出し元の行数</param>
        public ProcessCommand(
            Func<T, Task> execute,
            Func<T, bool> canExecute,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            : this(ProcessManager.Current, execute, canExecute, memberName, sourceFilePath, sourceLineNumber)
        {
        }
        #endregion

        static bool IsValidParameter(object o)
        {
            if (o != null)
                return o is T;

            var t = typeof(T);
            if (Nullable.GetUnderlyingType(t) != null)
                return true;

            return !t.GetTypeInfo().IsValueType;
        }
    }

    /// <summary>
    /// 処理の多重実行を制御するコマンド
    /// </summary>
    public class ProcessCommand : Command
    {
        #region ProcessManagerによる制御
        /// <summary>
        /// ProcessCommand クラスの新しいインスタンスを初期化する（executeの引数あり、canExecute指定なし）
        /// </summary>
        /// <param name="processManager">プロセスマネージャ</param>
        /// <param name="execute">コマンドの実行時に実行する関数</param>
        /// <param name="memberName">コマンド呼び出し元のメンバ名</param>
        /// <param name="sourceFilePath">コマンド呼び出し元のファイルパス</param>
        /// <param name="sourceLineNumber">コマンド呼び出し元の行数</param>
        public ProcessCommand(
            ProcessManager processManager,
            Func<object, Task> execute,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            : base(arg =>
                processManager.Invoke(ProcessManager.GetProcessName(memberName, sourceFilePath, sourceLineNumber),
                () => execute(arg))
                .ConfigureAwait(false))
        {
        }

        /// <summary>
        /// ProcessCommand クラスの新しいインスタンスを初期化する（executeの引数なし、canExecute指定なし）
        /// </summary>
        /// <param name="processManager">プロセスマネージャ</param>
        /// <param name="execute">コマンドの実行時に実行する関数</param>
        /// <param name="memberName">コマンド呼び出し元のメンバ名</param>
        /// <param name="sourceFilePath">コマンド呼び出し元のファイルパス</param>
        /// <param name="sourceLineNumber">コマンド呼び出し元の行数</param>
        public ProcessCommand(
            ProcessManager processManager,
            Func<Task> execute,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            : base(arg =>
                processManager.Invoke($"{sourceFilePath} L:{sourceLineNumber} {memberName}", execute)
                .ConfigureAwait(false))
        {
        }

        /// <summary>
        /// ProcessCommand クラスの新しいインスタンスを初期化する（executeの引数あり、canExecute指定あり）
        /// </summary>
        /// <param name="processManager">プロセスマネージャ</param>
        /// <param name="execute">コマンドの実行時に実行する関数</param>
        /// <param name="canExecute">コマンドが実行できるかどうかを示す</param>
        /// <param name="memberName">コマンド呼び出し元のメンバ名</param>
        /// <param name="sourceFilePath">コマンド呼び出し元のファイルパス</param>
        /// <param name="sourceLineNumber">コマンド呼び出し元の行数</param>
        public ProcessCommand(
            ProcessManager processManager,
            Func<object, Task> execute,
            Func<object, bool> canExecute,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            : base(arg =>
                processManager.Invoke(ProcessManager.GetProcessName(memberName, sourceFilePath, sourceLineNumber),
                () => execute(arg)
                .ConfigureAwait(false)),
                canExecute)
        {
        }

        /// <summary>
        /// ProcessCommand クラスの新しいインスタンスを初期化する（executeの引数なし、canExecute指定あり）
        /// </summary>
        /// <param name="processManager">プロセスマネージャ</param>
        /// <param name="execute">コマンドの実行時に実行する関数</param>
        /// <param name="canExecute">コマンドが実行できるかどうかを示す</param>
        /// <param name="memberName">コマンド呼び出し元のメンバ名</param>
        /// <param name="sourceFilePath">コマンド呼び出し元のファイルパス</param>
        /// <param name="sourceLineNumber">コマンド呼び出し元の行数</param>
        public ProcessCommand(
            ProcessManager processManager,
            Func<Task> execute,
            Func<bool> canExecute,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            : base(
                arg => processManager.Invoke(
                    $"{sourceFilePath} L:{sourceLineNumber} {memberName}",
                    execute)
                    .ConfigureAwait(false),
                arg => canExecute())
        {
        }
        #endregion

        #region ProcessManager.Currentによる制御
        /// <summary>
        /// ProcessCommand クラスの新しいインスタンスを初期化する（executeの引数あり、canExecute指定なし）
        /// </summary>
        /// <param name="execute">コマンドの実行時に実行する関数</param>
        /// <param name="memberName">コマンド呼び出し元のメンバ名</param>
        /// <param name="sourceFilePath">コマンド呼び出し元のファイルパス</param>
        /// <param name="sourceLineNumber">コマンド呼び出し元の行数</param>
        public ProcessCommand(
            Func<object, Task> execute,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            : this(ProcessManager.Current, execute, memberName, sourceFilePath, sourceLineNumber)
        {
        }

        /// <summary>
        /// ProcessCommand クラスの新しいインスタンスを初期化する（executeの引数なし、canExecute指定なし）
        /// </summary>
        /// <param name="execute">コマンドの実行時に実行する関数</param>
        /// <param name="memberName">コマンド呼び出し元のメンバ名</param>
        /// <param name="sourceFilePath">コマンド呼び出し元のファイルパス</param>
        /// <param name="sourceLineNumber">コマンド呼び出し元の行数</param>
        public ProcessCommand(
            Func<Task> execute,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            : this(ProcessManager.Current, execute, memberName, sourceFilePath, sourceLineNumber)
        {
        }

        /// <summary>
        /// ProcessCommand クラスの新しいインスタンスを初期化する（executeの引数あり、canExecute指定あり）
        /// </summary>
        /// <param name="execute">コマンドの実行時に実行する関数</param>
        /// <param name="canExecute">コマンドが実行できるかどうかを示す</param>
        /// <param name="memberName">コマンド呼び出し元のメンバ名</param>
        /// <param name="sourceFilePath">コマンド呼び出し元のファイルパス</param>
        /// <param name="sourceLineNumber">コマンド呼び出し元の行数</param>
        public ProcessCommand(
            Func<object, Task> execute,
            Func<object, bool> canExecute,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            : this(ProcessManager.Current, execute, canExecute, memberName, sourceFilePath, sourceLineNumber)
        {
        }

        /// <summary>
        /// ProcessCommand クラスの新しいインスタンスを初期化する（executeの引数なし、canExecute指定あり）
        /// </summary>
        /// <param name="execute">コマンドの実行時に実行する関数</param>
        /// <param name="canExecute">コマンドが実行できるかどうかを示す</param>
        /// <param name="memberName">コマンド呼び出し元のメンバ名</param>
        /// <param name="sourceFilePath">コマンド呼び出し元のファイルパス</param>
        /// <param name="sourceLineNumber">コマンド呼び出し元の行数</param>
        public ProcessCommand(
            Func<Task> execute,
            Func<bool> canExecute,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            : this(ProcessManager.Current, execute, canExecute, memberName, sourceFilePath, sourceLineNumber)
        {
        }
        #endregion
    }
}

