using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    }
}
