using System;
using System.Timers;

namespace IdleBuster
{
    public class UserInputMonitor
    {
        readonly Timer _timer;
        readonly IdleTimeoutAction _action;
        readonly TimeSpan _timeout;

        public UserInputMonitor(TimeSpan timeout, IdleTimeoutAction action)
        {
            _timeout = timeout;
            _action = action;
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Win32.IdleTime > _timeout)
            {
                switch (_action)
                {
                    case IdleTimeoutAction.Lock:
                        Win32.LockWorkStation();
                        break;
                    case IdleTimeoutAction.Logoff:
                        Win32.Logoff();
                        break;
                    case IdleTimeoutAction.Standby:
                        Win32.Standby();
                        break;
                    case IdleTimeoutAction.Hibernate:
                        Win32.Hibernate();
                        break;
                    case IdleTimeoutAction.Shutdown:
                        Win32.Shutdown();
                        break;
                    case IdleTimeoutAction.Restart:
                        Win32.Restart();
                        break;
                }
            }
        }
        public void Start() { _timer.Start(); }
        public void Stop() { _timer.Stop(); }
    }
}
