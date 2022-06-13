using System;
namespace Entap.Basic.Maui.Core
{
	/// <summary>
    /// アプリケーションに登録した依存性を保有する
    /// </summary>
	public static class BasicStartup
	{
		static MauiApp? _mauiApp;
		public static void Init(MauiApp mauiApp)
		{
			if (mauiApp is null)
				throw new ArgumentNullException(nameof(mauiApp));

			_mauiApp = mauiApp;
		}

		public static MauiApp MauiApp
		{
			get
			{
				if (_mauiApp is null)
					throw new InvalidOperationException($"You MUST call {typeof(BasicStartup)}.{nameof(Init)}; prior to using it.");

				return _mauiApp;
			}
		}
	}
}

