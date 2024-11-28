using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Networking;

namespace UTS_72210454.Data
{
    public static class ConnectivityHelper
    {
        public static bool IsConnected()
        {
            return Connectivity.NetworkAccess == NetworkAccess.Internet;
        }
    }
}
