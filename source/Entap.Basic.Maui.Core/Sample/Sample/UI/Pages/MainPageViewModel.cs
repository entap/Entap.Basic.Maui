﻿using System;
using Entap.Basic.Maui.Core;

namespace Sample
{
	public class MainPageViewModel : PageViewModelBase
	{
		public MainPageViewModel()
		{
		}

        public ProcessCommand ProcessCommand => new(async () =>
        {
            await PageManager.Navigation.PushAsync<ProcessPage>(new ProcessPageViewModel());
        });

        public ProcessCommand PageManagerCommand => new(async () =>
		{
			await PageManager.Navigation.PushAsync<PageManagerPage>(new PageManagerPageViewModel());
		});

        public ProcessCommand DisplaySizeCommand => new(async () =>
        {
            await PageManager.Navigation.PushAsync<DisplaySizePage>(new DisplaySizePageViewModel());
        });

        public ProcessCommand SafeAreaCommand => new(async () =>
        {
            await PageManager.Navigation.PushAsync<SafeAreaPage>(new SafeAreaPageViewModel());
        });
        
    }
}

