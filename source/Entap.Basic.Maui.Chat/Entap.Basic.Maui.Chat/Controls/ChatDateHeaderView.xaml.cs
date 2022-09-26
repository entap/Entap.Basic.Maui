namespace Entap.Basic.Maui.Chat;

public partial class ChatDateHeaderView : ContentView
{
	public ChatDateHeaderView()
	{
		InitializeComponent();
	}

    //#region DateVisible BindableProperty
    //public static readonly BindableProperty DateVisibleProperty = BindableProperty.Create(
    //    nameof(DateVisible),
    //    typeof(bool),
    //    typeof(ChatDateHeaderView),
    //    false,
    //    defaultBindingMode: BindingMode.Default);

    //public bool DateVisible
    //{
    //    get { return (bool)GetValue(DateVisibleProperty); }
    //    set { SetValue(DateVisibleProperty, value); }
    //}
    //#endregion

    //#region SendDateTime BindableProperty
    //public static readonly BindableProperty SendDateTimeProperty = BindableProperty.Create(
    //    nameof(SendDateTime),
    //    typeof(DateTime),
    //    typeof(ChatDateHeaderView),
    //    DateTime.MinValue,
    //    defaultBindingMode: BindingMode.Default);

    //public DateTime SendDateTime
    //{
    //    get { return (DateTime)GetValue(SendDateTimeProperty); }
    //    set { SetValue(SendDateTimeProperty, value); }
    //}
    //#endregion
}
