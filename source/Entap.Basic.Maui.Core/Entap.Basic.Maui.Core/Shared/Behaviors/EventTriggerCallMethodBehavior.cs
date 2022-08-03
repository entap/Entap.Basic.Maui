using System;
using System.Reflection;

namespace Entap.Basic.Maui.Core
{
    /// <summary>
    /// 任意のイベント発火時に指定したオブジェクトのメソッドを実行する
    /// </summary>
    public class EventTriggerCallMethodBehavior : BindableBehavior<VisualElement>
    {
        #region MethodName BindableProperty
        public static readonly BindableProperty MethodNameProperty = BindableProperty.Create(
            nameof(MethodName),
            typeof(string),
            typeof(EventTriggerCallMethodBehavior),
            null);

        public string MethodName
        {
            get { return (string)GetValue(MethodNameProperty); }
            set { SetValue(MethodNameProperty, value); }
        }
        #endregion

        #region TargetObject BindableProperty
        public static readonly BindableProperty TargetObjectProperty = BindableProperty.Create(
            nameof(TargetObject),
            typeof(object),
            typeof(EventTriggerCallMethodBehavior),
            null);

        public object TargetObject
        {
            get { return (object)GetValue(TargetObjectProperty); }
            set { SetValue(TargetObjectProperty, value); }
        }
        #endregion

        #region EventName BindableProperty
        public static readonly BindableProperty EventNameProperty = BindableProperty.Create(
            nameof(EventName),
            typeof(string),
            typeof(EventTriggerCallMethodBehavior),
            null);

        public string EventName
        {
            get { return (string)GetValue(EventNameProperty); }
            set { SetValue(EventNameProperty, value); }
        }
        #endregion

        protected override void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);
            SubscribeEvent(bindable);
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            UnsubscribeEvent(bindable);
            base.OnDetachingFrom(bindable);
        }

        protected void CallMethod()
        {
            if (TargetObject == null ||
                string.IsNullOrEmpty(MethodName))
                return;

            var methodInfo = TargetObject.GetType().GetMethod(MethodName);
            if (methodInfo == null)
                return;

            methodInfo.Invoke(TargetObject, null);
        }

        private EventInfo? subscribingEvent;

        private void Invoke(object sender, EventArgs e)
        {
            CallMethod();
        }

        private Delegate? _Handler;
        private Delegate? Handler
        {
            get
            {
                if (_Handler == null)
                {
                    var handlerMethodInfo =
                        typeof(EventTriggerCallMethodBehavior)
                            .GetMethod("Invoke", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (handlerMethodInfo is null) return null;
                    _Handler = Delegate.CreateDelegate(typeof(EventHandler), this, handlerMethodInfo);
                }

                return _Handler;
            }
        }

        private void SubscribeEvent(object associatedObject)
        {
            subscribingEvent = associatedObject.GetType().GetEvent(EventName);
            if (subscribingEvent == null)
                return;

            subscribingEvent?.GetAddMethod()?.Invoke(associatedObject, new[] { Handler });
        }

        private void UnsubscribeEvent(object associatedObject)
        {
            if (subscribingEvent == null)
                return;

            subscribingEvent?.GetRemoveMethod()?.Invoke(associatedObject, new[] { Handler });
        }
    }
}