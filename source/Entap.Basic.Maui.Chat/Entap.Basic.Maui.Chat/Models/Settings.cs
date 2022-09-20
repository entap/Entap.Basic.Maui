﻿using System;
namespace Entap.Basic.Maui.Chat
{
    [Preserve(AllMembers = true)]
    /// <summary>
    /// 設定
    /// </summary>
	public class Settings
	{
        static readonly Lazy<Settings> _settings = new Lazy<Settings>(() => new Settings());

        /// <summary>
        /// Current plugin implementation to use
        /// </summary>
        public static Settings Current => _settings.Value;

        public IChatService? ChatService;
        public IChatControlService? ChatControlService;

        /// <summary>
        /// チャットの各メッセージの送信時間の表示フォーマット
        /// </summary>
        public string? TimeFormat;
        /// <summary>
        /// チャット内の既読のテキスト
        /// </summary>
        public string? AlreadyReadText;
        /// <summary>
        /// チャット内の日付の今日のテキスト
        /// </summary>
        public string? TodayText;
        /// <summary>
        /// チャット内の日付のテキストフォーマット
        /// </summary>
        public string? DateFormat;
        /// <summary>
        /// メンバー追加時の文言指定
        /// </summary>
        public string? MemberAddRoomText;
        /// <summary>
        /// メンバー退出時の文言指定
        /// </summary>
        public string? MemberLeaveRoomText;

        public void Init(IChatService chatService, IChatControlService? chatControlService = null)
        {
            ChatService = chatService;
            if (chatControlService != null)
                ChatControlService = chatControlService;
            TimeFormat = "H:mm";
            AlreadyReadText = "既読";
            TodayText = "今日";
            DateFormat = "MM/dd";
            MemberAddRoomText = " が追加されました";
            MemberLeaveRoomText = " が退出しました";
        }
    }
}