﻿using System;

namespace Entap.Basic.Maui.Auth.Apple
{
	public static class Core
	{
		public static void Init(params AuthorizationScope[]? scopes)
		{
			if (!AppleSignInService.IsSupported)
				return;

			AppleSignInService.Init(scopes);
        }
    }
}

