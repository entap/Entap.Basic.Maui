using System;
using Android.Content;
using Android.Graphics.Drawables;
using AColor = Android.Graphics.Color;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform;

namespace Entap.Basic.Maui.Chat.Platforms.Android
{
	public class ChatListViewRenderer : ListViewRenderer
    {
		public ChatListViewRenderer(Context context) : base(context)
		{
		}
        protected override void Dispose(bool disposing)
        {
            var _ChatListView = Element as ChatListView;
            _ChatListView?.Dispose();
            if (Control is not null)
                Control.Scroll -= Control_Scroll;

            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (Control != null && e.NewElement != null)
            {
                DisableHighlight();
                Control.Scroll += Control_Scroll;
            }
        }

        private void DisableHighlight()
        {
            if (Control is null) return;
            Control.Selector = new ColorDrawable(AColor.Transparent);
        }

        private void Control_Scroll(object? sender, global::Android.Widget.AbsListView.ScrollEventArgs e)
        {
            if (Control is null) return;

            var _ChatListView = Element as ChatListView;
            if (_ChatListView?.Messages is null || _ChatListView.Messages.Count < 1)
                return;

            var firstIndex = e.FirstVisibleItem;

            if (_ChatListView.Messages.Count >= firstIndex)
            {
                int firstVisibleIndex;
                object firstVisibleItem;
                int lastVisibleIndex = 0;
                if (firstIndex == 0)
                {
                    firstVisibleIndex = firstIndex;
                    firstVisibleItem = _ChatListView.Messages[firstIndex];
                }
                else
                {
                    firstVisibleIndex = firstIndex - 1;
                    firstVisibleItem = _ChatListView.Messages[firstIndex - 1];
                }

                var lastVisibleItem = firstVisibleItem;
                var lastIndex = firstIndex - 1 + e.VisibleItemCount - 1;
                for (var i = lastIndex; i >= 0; i--)
                {
                    if (_ChatListView.Messages.Count - 1 >= i)
                    {
                        lastVisibleIndex = i;
                        lastVisibleItem = _ChatListView.Messages[i];
                        break;
                    }
                }

                _ChatListView.VisibleItemUpdateForAndroid(firstVisibleIndex, firstVisibleItem, lastVisibleIndex, lastVisibleItem);
            }
            var controlY = Control.GetChildAt(0)?.GetY() ?? 0;
            _ChatListView.OnScrolled(null, new ScrolledEventArgs(0, controlY * -1));
        }
    }
}