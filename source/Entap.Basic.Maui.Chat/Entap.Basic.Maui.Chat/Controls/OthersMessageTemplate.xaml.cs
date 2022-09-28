namespace Entap.Basic.Maui.Chat;

public partial class OthersMessageTemplate : ContentView
{
	public OthersMessageTemplate()
	{
		InitializeComponent();
	}

    #region Message BindableProperty
    public static readonly BindableProperty MessageProperty = BindableProperty.Create(
        nameof(Message),
        typeof(MessageBase),
        typeof(OthersMessageTemplate),
        default(MessageBase),
        defaultBindingMode: BindingMode.Default);

    public MessageBase Message
    {
        get { return (MessageBase)GetValue(MessageProperty); }
        set { SetValue(MessageProperty, value); }
    }
    #endregion
}
