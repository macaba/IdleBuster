using System;

namespace IdleBuster
{
    public class Settings
    {
        public TimeSpan Timeout { get; set; }
        public IdleTimeoutAction Action { get; set; }

        public void SetDefaults()
        {
            Timeout = new TimeSpan(0, 10, 0);
            Action = IdleTimeoutAction.Lock;
        }
    }
}
