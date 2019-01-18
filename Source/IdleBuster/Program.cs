using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IdleBuster
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath;

            if (args.Contains("install"))
            {
                if (!IsAdministrator())
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo(Path.Combine(path, "IdleBuster.exe"))
                    {
                        Verb = "runas",
                        Arguments = "install"
                    };
                    Process.Start(startInfo);
                    return;
                }
                else
                {
                    RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    registryKey.SetValue("IdleBuster", Path.Combine(path, "IdleBuster.exe"));
                    return;
                }
            }

            if (args.Contains("uninstall"))
            {
                if (!IsAdministrator())
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo(Path.Combine(path, "IdleBuster.exe"))
                    {
                        Verb = "runas",
                        Arguments = "uninstall"
                    };
                    Process.Start(startInfo);
                    return;
                }
                else
                {
                    RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    registryKey.DeleteValue("IdleBuster");
                    return;
               }
            }
          
            var settingsFile = Path.Combine(path, "IdleBuster.json");
            if (!File.Exists(settingsFile))
            {
                var defaultSettings = new Settings();
                defaultSettings.SetDefaults();
                File.WriteAllText(settingsFile, JsonConvert.SerializeObject(defaultSettings, new Newtonsoft.Json.Converters.StringEnumConverter()));
            }
            var settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(settingsFile));

            var cts = new CancellationTokenSource();
            var monitor = new UserInputMonitor(settings.Timeout, settings.Action);
            monitor.Start();
            WaitHandle.WaitAny(new[] { cts.Token.WaitHandle });
            monitor.Stop();
        }

        private static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
