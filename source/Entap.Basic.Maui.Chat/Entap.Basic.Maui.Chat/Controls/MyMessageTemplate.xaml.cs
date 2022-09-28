namespace Entap.Basic.Maui.Chat;

public partial class MyMessageTemplate : ContentView
{
	public MyMessageTemplate()
	{
		InitializeComponent();
	}

    #region IsGroupChat BindableProperty
    public static readonly BindableProperty IsGroupChatProperty = BindableProperty.Create(
        nameof(IsGroupChat),
        typeof(bool),
        typeof(MyMessageTemplate),
        false,
        defaultBindingMode: BindingMode.Default);

    public bool IsGroupChat
    {
        get { return (bool)GetValue(IsGroupChatProperty); }
        set { SetValue(IsGroupChatProperty, value); }
    }
    #endregion

    #region Message BindableProperty
    public static readonly BindableProperty MessageProperty = BindableProperty.Create(
        nameof(Message),
        typeof(MessageBase),
        typeof(MyMessageTemplate),
        default(MessageBase),
        defaultBindingMode: BindingMode.Default);

    public MessageBase Message
    {
        get { return (MessageBase)GetValue(MessageProperty); }
        set { SetValue(MessageProperty, value); }
    }
    #endregion
}
