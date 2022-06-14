using System;
namespace Entap.Basic.Maui.Core
{
    /// <summary>
    /// ページ制御を管理する
    /// </summary>
    public class PageManager
    {
        public PageManager()
        {
        }

        #region Navigation
        /// <summary>
        /// Navigation
        /// </summary>
        public static IPageNavigation Navigation => _pageNavigation ??= new PageNavigation();
        static IPageNavigation? _pageNavigation;

        /// <summary>
        /// PageNavigationの設定処理
        /// </summary>
        /// <param name="pageNavigation"></param>
        public static void SetNavigation(IPageNavigation pageNavigation)
        {
            _pageNavigation = pageNavigation;
        }
        #endregion
    }
}
