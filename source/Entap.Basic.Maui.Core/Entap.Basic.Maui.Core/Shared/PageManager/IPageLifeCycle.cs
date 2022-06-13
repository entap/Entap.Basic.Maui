using System;
namespace Entap.Basic.Maui.Core
{
    /// <summary>
    /// ページのライフサイクルインターフェース
    /// </summary>
    public interface IPageLifeCycle
    {
        /// <summary>
        /// 生成時処理
        /// </summary>
        void OnCreate();

        /// <summary>
        /// 進入時処理
        /// </summary>
        void OnEntry();

        /// <summary>
        /// 退出時処理
        /// </summary>
        void OnExit();

        /// <summary>
        /// 破棄時処理
        /// </summary>
        void OnDestroy();
    }
}

