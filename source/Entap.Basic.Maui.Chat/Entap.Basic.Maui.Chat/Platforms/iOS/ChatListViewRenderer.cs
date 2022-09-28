using System;
using Foundation;
using System.ComponentModel;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Platform;
using UIKit;
using Microsoft.Maui.Controls.Handlers.Compatibility;

namespace Entap.Basic.Maui.Chat.Platforms.iOS
{
    public class ChatListViewRenderer : ListViewRenderer
    {
        bool _isDisposed;
        object? lastItem;

        // KeyboardObserver
        NSObject? _keyboardShownObserver;

        public ChatListViewRenderer()
        {

        }

        protected override void Dispose(bool disposing)
        {
            if (Element is not null)
            {
                Element.Scrolled -= OnScrolled;
                var _ChatListView = Element as ChatListView;
                _ChatListView?.Dispose();
            }
            base.Dispose(disposing);
            _isDisposed = true;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                DisableHighlight();
                SubscribeKeyboardObserver();
                if (Element is null)
                    return;
                Element.Scrolled += OnScrolled;   
            }

            if (e.OldElement != null)
            {
                UnsubscribeKeyboardObserver();
                if (Element is null)
                    return;
                Element.Scrolled -= OnScrolled;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == ChatListView.ItemsSourceProperty.PropertyName)
                UpdateVisibleItem();
        }

        private void DisableHighlight()
        {
            if (Control is null) return;
            Control.AllowsSelection = false;
        }

        void OnScrolled(object? sender, ScrolledEventArgs e)
        {
            UpdateVisibleItem();
        }

        void UpdateVisibleItem()
        {
            var _chatListView = Element as ChatListView;
            if (_chatListView is null) return;
            if (Control is null) return;

            if (Control.VisibleCells?.Any() != true)
                return;

            var firstVisibleCell = Control.VisibleCells.First();
            var lastVisibleCell = Control.VisibleCells.Last();

            _chatListView.VisibleItemUpdateForiOS(
                GetCellIndex(Control, firstVisibleCell),
                GetCellIndex(Control, lastVisibleCell));
        }

        int GetCellIndex(UITableView control, UITableViewCell cell)
        {
            var indexPath = control.IndexPathForCell(cell);
            return (int)indexPath.Row;
        }

        public override void WillMoveToWindow(UIWindow? window)
        {
            base.WillMoveToWindow(window);
            var _ChatListView = Element as ChatListView;
            if (window is null)
                lastItem = _ChatListView?.LastVisibleItem;
            // チャットのページでモーダルのページが表示され、そのページが閉じられると初回のみリストの表示がモーダル表示前と変わってしまう現象が発生(前のアイテムが表示されてしまっていた)
            // 対策としてページが戻る際、モーダル表示前の状態にScrollTo使い戻しておく
            if (_ChatListView?.LastVisibleItem != null)
                _ChatListView.ScrollTo(lastItem, ScrollToPosition.End, false);
        }

        void SubscribeKeyboardObserver()
        {
            _keyboardShownObserver = UIKeyboard.Notifications.ObserveWillShow(OnKeyboardShown);
        }

        void UnsubscribeKeyboardObserver()
        {
            _keyboardShownObserver?.Dispose();
        }

        void OnKeyboardShown(object? sender, UIKeyboardEventArgs e)
        {
            if (Control is null) return;
            if (!(Element is ChatListView chatListPage)) return;

            var lastItem = chatListPage.Messages?.LastOrDefault();
            if (lastItem is null) return;

            var lastVisibleCell = Control.VisibleCells?.LastOrDefault();
            if (lastVisibleCell is null) return;

            var lastItemIndex = chatListPage?.Messages?.IndexOf(lastItem);

            var indexPath = Control.IndexPathForCell(lastVisibleCell);
            var lastVisibleIndex = (int)indexPath.Row;

            if (lastItemIndex != lastVisibleIndex) return;

            chatListPage?.Dispatcher.Dispatch(async () =>
            {
                // キーボード表示時のUI調整があるため待機
                await Task.Delay(100);
                if (Control is null) return;
                if (_isDisposed) return;
                chatListPage?.ScrollTo(lastItem, ScrollToPosition.End, false);
            });
        }
    }
}

