using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleBuster
{
    public enum IdleTimeoutAction
    {
        Lock,
        Logoff,
        Standby,
        Hibernate,
        Shutdown,
        Restart
    }
}
