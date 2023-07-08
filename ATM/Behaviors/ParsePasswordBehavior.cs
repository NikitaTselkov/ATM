using Core;
using Microsoft.Xaml.Behaviors;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ATM.Behaviors
{
    public class ParsePasswordBehavior : Behavior<PasswordBox>
    {
        #region Events

        public static readonly RoutedEvent SendPasswordEvent =
         EventManager.RegisterRoutedEvent("SendPassword",
             RoutingStrategy.Bubble,
         typeof(EventHandler<SendPasswordEventArgs>),
         typeof(ParsePasswordBehavior));

        public static void AddSendPasswordHandler(DependencyObject obj, EventHandler<SendPasswordEventArgs> handler)
        {
            ((UIElement)obj).AddHandler(SendPasswordEvent, handler);
        }

        public static void RemoveSendPasswordHandler(DependencyObject obj, EventHandler<SendPasswordEventArgs> handler)
        {
            ((UIElement)obj).RemoveHandler(SendPasswordEvent, handler);
        }

        #endregion

        protected override void OnAttached()
        {
            AssociatedObject.PasswordChanged += PasswordChanged;
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PasswordChanged -= PasswordChanged;
            base.OnDetaching();
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!char.IsDigit(AssociatedObject.Password.LastOrDefault()) && AssociatedObject.Password.Length > 0)
            {
                AssociatedObject.Password = AssociatedObject.Password[..^1];
            }

            if (AssociatedObject.SecurePassword.Length == 4)
            {
                ((UIElement)sender)?.RaiseEvent(new SendPasswordEventArgs(AssociatedObject.SecurePassword, SendPasswordEvent));
            }
        }
    }
}
