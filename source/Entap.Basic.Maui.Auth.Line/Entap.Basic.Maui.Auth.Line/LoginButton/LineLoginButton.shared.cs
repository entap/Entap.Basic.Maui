using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Entap.Basic.Maui.Auth.Line
{
    public class LineLoginButton : View
    {
        public LineLoginButton()
        {
        }

        #region LoginScopes BindableProperty
        public static readonly BindableProperty LoginScopesProperty = BindableProperty.Create(
            nameof(LoginScopes),
            typeof(IList<string>),
            typeof(LineLoginButton),
            new string[] { Enum.GetName(typeof(LoginScope), LoginScope.Profile) ?? throw new ArgumentException(nameof(LoginScope.Profile))},
            defaultBindingMode: BindingMode.Default
        );

        [TypeConverter(typeof(ListStringTypeConverter))]
        public IList<string> LoginScopes
        {
            get { return (IList<string>)GetValue(LoginScopesProperty); }
            set { SetValue(LoginScopesProperty, value); }
        }
        #endregion

        public LoginScope[] Scopes =>
            LoginScopes
                .Select((arg) => (LoginScope)Enum.Parse(typeof(LoginScope), arg))
                .ToArray();

        #region Command BindableProperty
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(LineLoginButton),
            null,
            defaultBindingMode: BindingMode.Default
        );

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        #endregion

        #region CommandParameter BindableProperty
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter),
            typeof(LoginResult),
            typeof(LineLoginButton),
            null,
            defaultBindingMode: BindingMode.Default
        );

        public LoginResult CommandParameter
        {
            get { return (LoginResult)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        #endregion

        public event EventHandler<LoginResult>? Clicked;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendClicked(LoginResult result)
        {
            if (!IsEnabled) return;

            Clicked?.Invoke(this, result);
            CommandParameter = result;
            if (Command?.CanExecute(CommandParameter) == true)
                Command?.Execute(CommandParameter);
        }

    }
}
