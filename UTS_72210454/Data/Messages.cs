using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace UTS_72210454.Data
{
    class RefreshMessage : ValueChangedMessage<bool>
    {
        public RefreshMessage(bool value) : base(value)
        {
        }
    }
}
