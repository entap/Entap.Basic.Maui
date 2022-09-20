using System;
namespace Entap.Basic.Maui.Chat
{
	public interface IChatControlService
	{
        Task<string> SelectImage();
        Task<string> TakePicture();
        Task ImageShare(string imagePath);
        Task VideoShare(string videoPath);
        void MoveImagePreviewPage(string imageUrl);
        void MoveVideoPreviewPage(string imageUrl);
        Task<IEnumerable<MessageBase>> BottomControllerMenuExecute(int notSendMessageId, int type, int roomId, ChatListView chatListView);
    }
}

