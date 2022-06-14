using System;
namespace Entap.Basic.Maui.Core
{
	/// <summary>
    /// アプリケーション
    /// </summary>
	public class MainApplication
	{
		public MainApplication()
		{
		}

		/// <summary>
        /// 実行元アプリケーション
        /// </summary>
		public static Application Current => Application.Current ?? throw new InvalidOperationException();
	}
}

