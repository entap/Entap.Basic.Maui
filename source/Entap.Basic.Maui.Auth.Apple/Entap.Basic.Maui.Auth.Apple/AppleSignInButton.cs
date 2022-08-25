using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Entap.Basic.Maui.Auth.Apple
{
    public interface IAppleSignInButton : IView
    {
        ButtonType ButtonType { get; set; }
        ButtonStyle ButtonStyle { get; set; }
        double CornerRadius { get; set; }
        ICommand? Command { get; set; }
        object? CommandParameter { get; set; }

        void SendClicked();
    }

    public class AppleSignInButton : View, IAppleSignInButton
    {
        public AppleSignInButton() : this(ButtonType.Default, ButtonStyle.Black)
        {
        }

        public AppleSignInButton(ButtonType buttonType, ButtonStyle buttonStyle)
        {
            ButtonType = buttonType;
            ButtonStyle = buttonStyle;

            if (!OperatingSystem.IsIOS())
            {
                IsVisible = false;
                return;
            }
        }

        public ButtonType ButtonType { get; set; }
        public ButtonStyle ButtonStyle { get; set; }

#region CornerRadius BindableProperty
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
            nameof(CornerRadius),
            typeof(double),
            typeof(AppleSignInButton),
            -1d,
            defaultBindingMode: BindingMode.Default);

        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
#endregion

#region Command BindableProperty
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(AppleSignInButton),
            null,
            defaultBindingMode: BindingMode.Default);

        public ICommand? Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
#endregion

#region CommandParameter BindableProperty
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(AppleSignInButton),
            null,
            defaultBindingMode: BindingMode.Default);

        public object? CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
#endregion

        public event EventHandler? Clicked;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendClicked()
        {
            if (!IsEnabled) return;

            Clicked?.Invoke(this, EventArgs.Empty);
            if (Command?.CanExecute(CommandParameter) == true)
                Command?.Execute(CommandParameter);
        }
    }
}

