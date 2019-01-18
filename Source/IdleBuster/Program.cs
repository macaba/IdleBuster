using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace IdleBuster
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath;
            var settingsFile = Path.Combine(path, "IdleBuster.json");
            if(!File.Exists(settingsFile))
            {
                var defaultSettings = new Settings();
                defaultSettings.SetDefaults();
                File.WriteAllText(settingsFile, JsonConvert.SerializeObject(defaultSettings, new Newtonsoft.Json.Converters.StringEnumConverter()));
            }
            var settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(settingsFile));

            var rc = HostFactory.Run(x =>
            {
                x.Service<UserInputMonitor>(s =>
                {
                    s.ConstructUsing(name => new UserInputMonitor(settings.Timeout, settings.Action));
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("IdleBuster");
                x.SetDisplayName("IdleBuster");
                x.SetServiceName("IdleBuster");
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
