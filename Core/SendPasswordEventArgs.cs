using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Core
{
    public class SendPasswordEventArgs : RoutedEventArgs
    {
        public SecureString Password { get; private set; }

        public SendPasswordEventArgs() { }

        public SendPasswordEventArgs(SecureString password)
        {
            Password = password;
        }
        
        public SendPasswordEventArgs(SecureString password, RoutedEvent routedEvent) : base(routedEvent)
        {
            Password = password;
        }
    }
}
