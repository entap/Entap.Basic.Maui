using System;
using Entap.Basic.Maui.Core;

namespace Sample
{
	public class DisplaySizePageViewModel : PageViewModelBase
    {
		public DisplaySizePageViewModel()
		{
            UpdateDeviceSize();
        }

        public double PageWidth
        {
            get => _pageWidth;
            set
            {
                if (SetProperty(ref _pageWidth, value))
                {
                    System.Diagnostics.Debug.WriteLine($"PageWidth : {PageWidth}");
                }
            }
        }
        double _pageWidth;

        public double PageHeight
        {
            get => _pageHeight;
            set
            {
                if (SetProperty(ref _pageHeight, value))
                {
                    System.Diagnostics.Debug.WriteLine($"PageHeight : {PageHeight}");
                }
            }
        }
        double _pageHeight;

       public Command<Size> PageSizeChangedCommand => new((size) =>
       {
           System.Diagnostics.Debug.WriteLine($"PageSizeChangedCommand PageHeight : {size.Width}  PageHeight : {size.Height}");
       });

        public bool HasNavigationBar
        {
            get => _hasNavigationBar;
            set
            {
                if (SetProperty(ref _hasNavigationBar, value))
                    UpdateDeviceSize();
            }
        }
        bool _hasNavigationBar = true;

        public Command SwitchNavigationBarVisibleCommand => new (() => HasNavigationBar = !HasNavigationBar);


        public string DisplaySizeInfo
        {
            get => _displaySizeInfo;
            set => SetProperty(ref _displaySizeInfo, value);
        }
        string _displaySizeInfo;

        void UpdateDeviceSize()
        {
            DisplaySizeInfo =@$"
<Essentials>
  Density   : {DeviceDisplay.Current.MainDisplayInfo.Density}

  Width     : {DeviceDisplay.Current.MainDisplayInfo.Width}
  Height    : {DeviceDisplay.Current.MainDisplayInfo.Height}

<DisplaySizeManager>
  ScreenSize.Width  : {DisplaySizeManager.ScreenSize.Width}
  ScreenSize.Height : {DisplaySizeManager.ScreenSize.Height}

  StatusBarHeight       : {DisplaySizeManager.StatusBarHeight}
  TopNavigationHeight   : {DisplaySizeManager.TopNavigationHeight}
  AndroidBottomNavigationBarHeight  : {DisplaySizeManager.AndroidBottomNavigationBarHeight}
";
        }
    }
}

