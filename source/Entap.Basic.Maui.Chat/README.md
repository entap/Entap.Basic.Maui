# Entap.Basic.Maui.Chat

## 使用方法
* Entap.Basic.Maui.Chatをインストールする

### 初期化
* [IChatService](Entap.Basic.Maui.Chat/Interfaces/IChatService.cs)の実装クラス（メッセージ送受信用の処理）を定義する  
* [IChatControlService](Entap.Basic.Maui.Chat/Interfaces/IChatControlService.cs)の実装クラス（写真・動画等の付随処理）を定義する(任意)
* [Settings](Entap.Basic.Maui.Chat/Models/Settings.cs)の初期化処理を実装する  
実装例）
```csharp
Entap.Basic.Maui.Chat.Settings.Current.Init(new ChatService(), new ChatControlServic());
```
> **Note**  
> [Settings](Entap.Basic.Maui.Chat/Models/Settings.cs)では、日付等のフォーマットや文言の指定が可能。  

### チャット画面実装
以下のコントロール等を利用し、レイアウトする  
詳細は[サンプル](Sample/UI/Pages/ChatPage.xaml)参照  

* [ChatListView](Entap.Basic.Maui.Chat/Controls/ChatListView.cs)  
チャットのリスト部分のコントロール。
RoomIdとIsGroupChatとLastReadMessageIdはバインド必須

* [DynamicResizedEditor](Entap.Basic.Maui.Chat/Controls/DynamicResizedEditor.cs)  
送信するテキストメッセージを入力するEditor  
MaxDisplayLineCountが指定でき、MaxDisplayLineCountに達するまでは入力行数に合わせてリサイズする。  

### チャットメッセージ表示
メッセージの種類や、送信者等により異なるレイアウトとを採用するため[DataTemplateSelector](https://learn.microsoft.com/ja-jp/dotnet/maui/fundamentals/datatemplate#create-a-datatemplateselector)の利用を推奨する
* [MessageDataTemplateSelector](Entap.Basic.Maui.Chat/Controls/MessageDataTemplateSelector.cs)  
  メッセージの種類、送信者IDにより採用するDataTemplateを判定する。  
  定義されていないDataTemplate利用時や、判定を変更が必要な場合には任意の[DataTemplateSelector](https://learn.microsoft.com/ja-jp/dotnet/maui/fundamentals/datatemplate#create-a-datatemplateselector)を定義する  
* [MyMessageTemplate](Entap.Basic.Maui.Chat/Controls/MyMessageTemplate.xaml), [OthersMessageTemplate](Entap.Basic.Maui.Chat/Controls/OthersMessageTemplate.xaml)  
自分が送信したメッセージと、他者から受信したメッセージの共通部を定義する（日付ヘッダー、送信者、送受信日時、既読状態）  
それぞれ、MyMessageControlTemplateBase, OthersMessageControlTemplateBaseをキーとしたControlTemplateを定義することで
任意のControlTemplateの指定が可能  

詳細は[サンプル](Sample/Resources/Styles/ChatResources.xaml)参照
