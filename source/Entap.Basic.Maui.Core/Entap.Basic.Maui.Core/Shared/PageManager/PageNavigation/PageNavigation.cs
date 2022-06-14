namespace Entap.Basic.Maui.Core
{
    public class PageNavigation : IPageNavigation
    {
        public PageNavigation()
        {
            Init();
        }

        void Init()
        {
            // Androidデバイスの戻るボタンを検出するため、App.Current.ModalPoppedを購読
            Application.Current.ModalPopped += OnModalPopped;
        }

        void OnModalPopped(object? sender, ModalPoppedEventArgs e)
        {
            if (IsNavigationPage(e.Modal))
                RemoveNavigationStack(e.Modal.Navigation);
            else
                OnPagePopped(e.Modal);

            var currentPage = GetCurrentPage();
            if (currentPage is null) return;

            GetViewModelBase(currentPage)?.OnEntry();
            if (currentPage is TabbedPage tabbedPage)
                GetViewModelBase(tabbedPage.CurrentPage)?.OnEntry();
        }

        public virtual Page CreateNavigationPage(Page page)
        {
            try
            {
                var navigationPage = new NavigationPage(page);
                navigationPage.Behaviors.Add(new NavigationBehavior());
                return navigationPage;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("CreateNavigationPage : " + ex.Message);
                throw;
            }
        }

        public virtual Page CreateClosableNavigationPage(Page page)
        {
            var navigationPage = CreateNavigationPage(page);
            page.ToolbarItems.Add(new ToolbarItem()
            {
                Text = "閉じる",
                Command = new ProcessCommand(async (obj) =>
                {
                    await PageManager.Navigation.PopModalAsync(true);
                })
            });
            return navigationPage;
        }

        #region IPageNavigation

        #region CurrentPage制御
        /// <summary>
        /// ページ遷移順を管理するため、NavigationStack, ModalStack共通のStackを保有
        /// </summary>
        List<Guid> pageIdStack = new List<Guid>();

        public Page? GetCurrentPage()
        {
            var lastItem = pageIdStack.LastOrDefault();
            // NavigationStackからページ取得
            var lastNavigationPage = currentNavigation.NavigationStack?.Where((arg) => arg.Id == lastItem).LastOrDefault();
            if (lastNavigationPage != null)
                return GetCurrentPage(lastNavigationPage);

            var lastModalPage = currentNavigation.ModalStack?.Where((arg) => arg.Id == lastItem).LastOrDefault();
            if (lastModalPage != null)
                return GetCurrentPage(lastModalPage);

            if (Application.Current.MainPage is null)
                return null;

            return GetCurrentPage(Application.Current.MainPage);
        }

        Page GetCurrentPage(Page page)
        {
            if (page is null)
                throw new ArgumentNullException(nameof(page));

            if (IsNavigationPage(page))
            {
                var navigationPage = (NavigationPage)page;
                return navigationPage.CurrentPage;
            }
            else if (page is TabbedPage tabbedPage)
                return tabbedPage.CurrentPage;
            return page;
        }

        public PageViewModelBase? GetViewModelBase(Page page, object? viewModel = null)
        {
            if (viewModel == null)
            {
                if (page == null)
                    return null;

                if (page is NavigationPage navigationPage)
                {
                    viewModel = navigationPage.CurrentPage?.BindingContext;
                }
                else
                {
                    viewModel = page.BindingContext;
                }
            }

            return viewModel as PageViewModelBase;
        }

        /// <summary>
        /// モーダルナビゲーションをスタックする。
        /// </summary>
        List<INavigation>? modalNavigationStack;

        /// <summary>
        /// 現在のNavigationを取得する。
        /// </summary>
        /// <value>The current navigation.</value>
        INavigation currentNavigation =>
            modalNavigationStack?.Last() ??
            Application.Current.MainPage?.Navigation ??
            throw new InvalidOperationException();

        void ClearNavigationModalStack()
        {
            if (modalNavigationStack is null) return;
            Array.ForEach(modalNavigationStack.AsParallel().AsOrdered().ToArray(), RemoveNavigationStack);
            modalNavigationStack = null;
        }

        void RemoveNavigationStack(INavigation? navigation)
        {
            if (navigation == null)
                return;

            Array.ForEach(navigation.NavigationStack.AsParallel().AsOrdered().ToArray(), (obj) => OnPagePopped(obj));
            Array.ForEach(navigation.ModalStack.AsParallel().AsOrdered().ToArray(), (obj) => OnPagePopped(obj));

            modalNavigationStack?.Remove(navigation);
        }
        #endregion

        #region メインページ設定
        /// <summary>
        /// 指定されたページをメインページに設定
        /// </summary>
        /// <param name="viewModel">ViewModelをバインドしたい場合に指定</param>
        /// /// <typeparam name="T">ページタイプ</typeparam>
        public Task<Task> SetMainPage<T>(PageViewModelBase? viewModel = null) where T : Page
        {
            return SetMainPage<T>(false, viewModel);
        }

        /// <summary>
        /// NavigationPageをメインページに設定
        /// </summary>
        /// <param name="viewModel">ViewModelをバインドしたい場合に指定</param>
        /// <typeparam name="T">ページタイプ</typeparam>
        public Task<Task> SetNavigationMainPage<T>(PageViewModelBase? viewModel = null) where T : Page
        {
            return SetMainPage<T>(true, viewModel);
        }

        Task<Task> SetMainPage<T>(bool isNavigationPage, PageViewModelBase? viewModel) where T : Page
        {
            var page = (isNavigationPage) ? CreateNavigationPage<T>(viewModel) : CreatePage<T>(viewModel);

            if (Application.Current?.MainPage == null)
            {
                SetMainPageBase(page, viewModel);
                return Task.FromResult(Task.CompletedTask);
            }
            else
            {
                var completionSource = new TaskCompletionSource<Task>();
                try
                {
                    Application.Current.Dispatcher.Dispatch(() =>
                    {
                        SetMainPageBase(page, viewModel);
                        completionSource.SetResult(completionSource.Task);
                    });
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    completionSource.SetResult(completionSource.Task);
                }
                return completionSource.Task;
            }
        }
        void SetMainPageBase(Page page, PageViewModelBase? viewModel)
        {
            if (Application.Current is null) return;

            RemoveMainPageNavigationStack();
            Application.Current.MainPage = page;

            var currentPage = GetCurrentPage(page);
            pageIdStack.Add(currentPage.Id);

            GetViewModelBase(page, viewModel)?.OnEntry();
            ClearNavigationModalStack();
        }

        void RemoveMainPageNavigationStack()
        {
            var mainPage = (Application.Current.MainPage);
            if (mainPage is null) return;
            RemoveNavigationStack(mainPage.Navigation);
            OnPagePopped(mainPage);
        }
        #endregion

        #region Page遷移
        /// <summary>
        /// PagePush
        /// </summary>
        /// <param name="viewModel">ViewModelをバインドしたい場合に指定</param>
        /// <param name="animated"><c>true</c>:ページ遷移後に、アニメーションを有効にする</param>
        /// <param name="removePages"><c>false</c>Pushするページ以外を削除する</param>
        /// <typeparam name="T">ページタイプ</typeparam>
        public Task PushAsync<T>(PageViewModelBase? viewModel = null, bool animated = true, bool removePages = false) where T : Page
        {
            var page = CreatePage<T>(viewModel);
            return PushAsync(page, viewModel, animated, removePages);
        }
        public Task PushAsync(Page page, PageViewModelBase? viewModel, bool animated = true, bool removePages = false)
        {
            var completionSource = new TaskCompletionSource<Task>();

            Application.Current.Dispatcher.Dispatch(async () =>
            {
                var oldPage = GetCurrentPage();
                await currentNavigation.PushAsync(page, animated);

                if (removePages)
                {
                    // スタックされているPushされたページ以外のすべてのページを削除する
                    var removingPages = currentNavigation.NavigationStack.Where(p => pageIdStack.Contains(p.Id)).ToList();
                    foreach (var rp in removingPages)
                    {
                        currentNavigation.RemovePage(rp);
                        pageIdStack.Remove(rp.Id);
                    }
                }

                pageIdStack.Add(page.Id);

                GetViewModelBase(page, viewModel)?.OnEntry();
                if (oldPage is not null)
                    GetViewModelBase(oldPage, null)?.OnExit();

                completionSource.SetResult(completionSource.Task);
            });
            return completionSource.Task;
        }

        /// <summary>
        /// PopPage
        /// </summary>
        /// <param name="animated"><c>true</c>:ページ遷移後に、アニメーションを有効にする</param>
        public Task PopAsync(bool animated = true)
        {
            var completionSource = new TaskCompletionSource<Task>();
            Application.Current.Dispatcher.Dispatch(async () =>
            {
                await currentNavigation.PopAsync(animated);

                completionSource.SetResult(completionSource.Task);
            });
            return completionSource.Task;
        }

        /// <summary>
        /// PushModalPage
        /// </summary>
        /// <param name="viewModel">ViewModelをバインドしたい場合に指定</param>
        /// <param name="animated"><c>true</c>:ページ遷移後に、アニメーションを有効にする</param>
        /// <typeparam name="T">ページタイプ</typeparam>
        public Task PushModalAsync<T>(PageViewModelBase? viewModel = null, bool animated = true) where T : Page
        {
            var page = CreatePage<T>(viewModel);
            return PushModalAsync(page, viewModel, animated);
        }

        public Task PushModalAsync(Page page, PageViewModelBase? viewModel = null, bool animated = true)
        {
            var completionSource = new TaskCompletionSource<Task>();
            Application.Current.Dispatcher.Dispatch(async () =>
            {
                var oldPage = GetCurrentPage();
                await currentNavigation.PushModalAsync(page, animated);

                var currentPage = GetCurrentPage(page);
                pageIdStack.Add(currentPage.Id);

                GetViewModelBase(page, viewModel)?.OnEntry();
                if (oldPage is not null)
                    GetViewModelBase(oldPage, null)?.OnExit();

                if (IsNavigationPage(page))
                {
                    if (modalNavigationStack == null)
                        modalNavigationStack = new List<INavigation>();

                    modalNavigationStack.Add(page.Navigation);
                }

                completionSource.SetResult(completionSource.Task);
            });
            return completionSource.Task;
        }

        /// <summary>
        /// NavigationPageをPushModalする
        /// </summary>
        /// <typeparam name="T">ページタイプ</typeparam>
        /// <param name="viewModel">ViewModelをバインドしたい場合に指定</param>
        /// <param name="animated"><c>true</c>:ページ遷移後に、アニメーションを有効にする</param>
        /// <param name="hasNavigationCloseButton"><c>true</c>:閉じるボタンを表示する</param>
        public Task PushNavigationModalAsync<T>(PageViewModelBase? viewModel = null, bool animated = true, bool hasNavigationCloseButton = false) where T : Page
        {
            var page = CreatePage<T>(viewModel);
            return PushNavigationModalAsync(page, viewModel, animated, hasNavigationCloseButton);
        }

        /// <summary>
        /// NavigationPageをPushModalする
        /// </summary>
        /// <param name="page">ページ</param>
        /// <param name="viewModel">ViewModelをバインドしたい場合に指定</param>
        /// <param name="animated"><c>true</c>:ページ遷移後に、アニメーションを有効にする</param>
        /// <param name="hasNavigationCloseButton"><c>true</c>:閉じるボタンを表示する</param>
        public Task PushNavigationModalAsync(Page page, PageViewModelBase? viewModel = null, bool animated = true, bool hasNavigationCloseButton = false)
        {
            var navigationPage = hasNavigationCloseButton ?
                CreateClosableNavigationPage(page) :
                CreateNavigationPage(page);
            return PushModalAsync(navigationPage, viewModel, animated);
        }

        /// <summary>
        /// PopModalPage
        /// </summary>
        /// <param name="animated"><c>true</c>:ページ遷移後に、アニメーションを有効にする</param>
        public Task PopModalAsync(bool animated = true)
        {
            var completionSource = new TaskCompletionSource<Task>();
            Application.Current.Dispatcher.Dispatch(async () =>
            {
                try
                {
                    // LastPage退避
                    var lastPage = currentNavigation.ModalStack.LastOrDefault();
                    await currentNavigation.PopModalAsync(animated);
                    if (lastPage is null) return;
                    if (IsNavigationPage(lastPage))
                        RemoveNavigationStack(lastPage.Navigation);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"PopModalAsync : {ex.Message} _ {GetCurrentPage()?.GetType()}");
                    throw;
                }

                completionSource.SetResult(completionSource.Task);
            });
            return completionSource.Task;
        }

        public void OnPagePopped(Page page, PageViewModelBase? viewModelBase = null)
        {
            // pageIdStackから削除
            pageIdStack.Remove(page.Id);
            // OnExit実行
            GetViewModelBase(page, viewModelBase)?.OnExit();
            // OnDestroy実行
            GetViewModelBase(page, viewModelBase)?.OnDestroy();
        }

        /// <summary>
        /// PopToRootPage
        /// </summary>
        /// <param name="animated"><c>true</c>:ページ遷移後に、アニメーションを有効にする</param>
        public Task PopToRootAsync(bool animated = true)
        {
            var completionSource = new TaskCompletionSource<Task>();
            Application.Current.Dispatcher.Dispatch(async () =>
            {
                OnPopToRoot(currentNavigation);
                await currentNavigation.PopToRootAsync(animated);

                completionSource.SetResult(completionSource.Task);
            });
            return completionSource.Task;
        }
        public void OnPopToRoot(INavigation navigation)
        {
            if (navigation == null)
                return;
            if (navigation.NavigationStack is null)
                return;
            if (navigation.NavigationStack.Count <= 1)
                return;

            var firstItem = navigation.NavigationStack.First();
            if (firstItem is not null)
            {
                Array.ForEach(navigation.NavigationStack
                          .Where((arg) => arg != firstItem)
                          .AsParallel()
                          .AsOrdered()
                          .ToArray(),
                          (obj) => OnPagePopped(obj));
            }

            var currentPage = GetCurrentPage();
            if (currentPage is null) return;
            GetViewModelBase(currentPage)?.OnEntry();
        }

        /// <summary>
        /// 現在のページの直前に、指定したページを挿入する
        /// </summary>
        /// <typeparam name="T">挿入するページ</typeparam>
        /// <param name="insertPageViewModel">挿入するページのViewModel</param>
        public void InsertPageBefore<T>(PageViewModelBase insertPageViewModel, Page? beforePage = null) where T : Page
        {
            var before = beforePage ?? GetCurrentPage();
            currentNavigation.InsertPageBefore(CreatePage<T>(insertPageViewModel), before);
        }
        #endregion

        #endregion

        #region ページ生成
        Page CreateNavigationPage<T>(PageViewModelBase? viewModel = null) where T : Page
        {
            var page = CreatePage<T>(viewModel);
            return CreateNavigationPage(page);
        }

        /// <summary>
        /// 指定されたページを生成する。
        /// </summary>
        /// <returns>生成したページを返す</returns>
        /// <param name="viewModel">ViewModelをバインドしたい場合に指定</param>
        /// <typeparam name="T">ページのタイプ</typeparam>
        Page CreatePage<T>(PageViewModelBase? viewModel = null) where T : Page
        {
            try
            {
                // ページのインスタンスを生成
                if (Activator.CreateInstance(typeof(T)) is not Page page)
                    throw new NullReferenceException();

                // ViewModelの指定がある場合、BindingContextに設定する。
                if (viewModel != null)
                    page.BindingContext = viewModel;

                return page;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// 指定したページがNavigationPageかどうか判定する。
        /// </summary>
        /// <returns><c>true</c>NavigationPage, <c>false</c> otherwise.</returns>
        /// <param name="page">Page.</param>
        bool IsNavigationPage(Page page)
        {
            return (page?.GetType() == typeof(NavigationPage) ||
                    page?.GetType()?.BaseType == typeof(NavigationPage)) ?
                true : false;
        }
        #endregion
    }
}
