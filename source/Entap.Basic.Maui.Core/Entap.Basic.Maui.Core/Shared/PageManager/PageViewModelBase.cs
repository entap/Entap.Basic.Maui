using System;

namespace Entap.Basic.Maui.Core
{
    /// <summary>
    /// Page用ViewModel
    /// </summary>
    public class PageViewModelBase : BindableBase
    {
        IPageLifeCycle? _pageLifeCycle;
        public PageViewModelBase()
        {
        }

        /// <summary>
        /// IPageLifeCycleのセット
        /// </summary>
        /// <param name="pageLifeCycle">PageLifeCycle</param>
        public void SetPageLifeCycle(IPageLifeCycle pageLifeCycle)
        {
            _pageLifeCycle = pageLifeCycle;
            _pageLifeCycle?.OnCreate();
        }

        #region PageNavigationStatus制御

        /// <summary>
        /// ページ侵入時（MainPage設定, Push先, Pop先）処理
        /// </summary>
        public virtual void OnEntry()
        {
            System.Diagnostics.Debug.WriteLine($"{this} : {nameof(OnEntry)}");
            _pageLifeCycle?.OnEntry();
        }

        /// <summary>
        /// ページ退出時（Push元）処理
        /// </summary>
        public virtual void OnExit()
        {
            System.Diagnostics.Debug.WriteLine($"{this} : {nameof(OnExit)}");
            _pageLifeCycle?.OnExit();
        }

        /// <summary>
        /// ページ破棄時（Pop元）処理
        /// </summary>
        public virtual void OnDestroy()
        {
            System.Diagnostics.Debug.WriteLine($"{this} : {nameof(OnDestroy)}");
            _pageLifeCycle?.OnDestroy();
            HasDestryoed = true;
        }

        /// <summary>
        /// ViewModelが破棄されたか
        /// </summary>
        public bool HasDestryoed
        {
            get => _hasDestryoed;
            set => SetProperty(ref _hasDestryoed, value);
        }
        bool _hasDestryoed;
        #endregion

        #region PushCommand
        public ProcessCommand PushCommand<T>(PageViewModelBase viewModel) where T : Page => new ProcessCommand(async () =>
        {
            await PageManager.Navigation.PushAsync<T>(viewModel);
        });

        public ProcessCommand PushModalCommand<T>(PageViewModelBase viewModel) where T : Page => new ProcessCommand(async () =>
        {
            await PageManager.Navigation.PushModalAsync<T>(viewModel);
        });

        public ProcessCommand PushNavigationModalCommand<T>(PageViewModelBase viewModel, bool animated, bool hasNavigationCloseButton = true) where T : Page => new ProcessCommand(async () =>
        {
            await PageManager.Navigation.PushNavigationModalAsync<T>(viewModel, animated, hasNavigationCloseButton);
        });
        #endregion

        #region PopCommand
        public ProcessCommand PopCommand => new ProcessCommand(async () =>
        {
            await PageManager.Navigation.PopAsync(true);
        });

        public ProcessCommand PopModalCommand => new ProcessCommand(async () =>
        {
            await PageManager.Navigation.PopModalAsync(true);
        });
        #endregion
    }
}