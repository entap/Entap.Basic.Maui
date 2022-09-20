using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Entap.Basic.Maui.Chat
{
    [Preserve(AllMembers = true)]
    public class MessageBase : INotifyPropertyChanged
    {
        public MessageBase(string sendUserId)
        {
            SendUserId = sendUserId;
        }

        public MessageBase(MessageBase notSendMessage)
        {
            MessageId = notSendMessage.MessageId;
            SendDateTime = notSendMessage.SendDateTime;
            Text = notSendMessage.Text;
            MediaUrl = notSendMessage.MediaUrl;
            UserIcon = notSendMessage.UserIcon;
            MessageType = notSendMessage.MessageType;
            SendUserId = notSendMessage.SendUserId;
            AlreadyReadCount = 0;
            ResendVisible = true;
        }

        private int messageId;
        public int MessageId
        {
            get => messageId;
            set
            {
                if (SetProperty(ref messageId, value))
                {
                    if (messageId != ChatListView.NotSendMessageId)
                        ProgressVisible = false;
                    OnPropertyChanged(nameof(ResendVisible));
                }
            }
        }

        private DateTime sendDateTime;
        public DateTime SendDateTime
        {
            get => sendDateTime;
            set => SetProperty(ref sendDateTime, value);
        }

        public string? Text { get; set; }

        private string? mediaUrl;
        public string? MediaUrl
        {
            get => mediaUrl;
            set => SetProperty(ref mediaUrl, value);
        }

        private string? mediaThumbnailUrl;
        public string? MediaThumbnailUrl
        {
            get => mediaThumbnailUrl;
            set => SetProperty(ref mediaThumbnailUrl, value);
        }

        private string? userIcon;
        public string? UserIcon
        {
            get => userIcon;
            set => SetProperty(ref userIcon, value);
        }

        /// <summary>
        /// 1:テキスト, 2:画像, 3:動画
        /// </summary>
        public int MessageType { get; set; }
        public string SendUserId { get; set; }

        private int alreadyReadCount;
        public int AlreadyReadCount
        {
            get => alreadyReadCount;
            set => SetProperty(ref alreadyReadCount, value);
        }

        private bool resendVisible;
        public bool ResendVisible
        {
            get => resendVisible;
            set => SetProperty(ref resendVisible, value);
        }

        public int NotSendId { get; set; }

        private bool dateVisible;
        public bool DateVisible
        {
            get => dateVisible;
            set => SetProperty(ref dateVisible, value);
        }

        private double uploadProgress;
        public double UploadProgress
        {
            get => uploadProgress;
            set
            {
                if (SetProperty(ref uploadProgress, value))
                {
                    if (uploadProgress >= 1)
                        ProgressVisible = false;
                }
            }
        }
        public void HandleUploadProgress(long bytes, long totalBytes)
        {
            System.Diagnostics.Debug.WriteLine("Uploading {0}/{1}", bytes, totalBytes);
            UploadProgress = (double)bytes / (double)totalBytes;
        }
        private bool progressVisible;
        public bool ProgressVisible
        {
            get => progressVisible;
            set
            {
                if (SetProperty(ref progressVisible, value))
                {
                    if (!progressVisible)
                        UploadProgress = 0;
                }
            }
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        /// <summary>
        /// プロパティ値を変更し、クライアントに通知し、プロパティの変更有無を返す。
        /// </summary>
        /// <returns><c>true</c>, プロパティに変更あり<c>false</c>プロパティに変更なし</returns>
        /// <typeparam name="T">プロパティの型。</typeparam>
        /// <param name="storage">get アクセス操作子と set アクセス操作子両方を使用したプロパティへの参照。</param>
        /// <param name="value">プロパティに必要な値。</param>
        /// <param name="propertyName">リスナーに通知するために使用するプロパティの名前。省略可能。</param>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (object.Equals(storage, value))
            {
                return false;
            }
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}