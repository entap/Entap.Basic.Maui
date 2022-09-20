using System;
namespace Entap.Basic.Maui.Chat
{
    [Preserve(AllMembers = true)]
    public class SendMessageResponseBase
    {
        public int MessageId { get; set; }
        public DateTime SendDateTime { get; set; }
        public string? MediaThumbnailUrl { get; set; }
    }
}

