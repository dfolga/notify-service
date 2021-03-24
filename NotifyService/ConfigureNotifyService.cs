
using Newtonsoft.Json.Linq;
using NLog;
using System;
using Topshelf;

namespace NotifyService
{
    class ConfigureNotifyService
    {
        static void Main(string[] args)
        {
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "logs/file.txt" };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

            // Apply config           
            NLog.LogManager.Configuration = config;

            string data;
            JObject jsonObject = JObject.Parse(data);

            var rc = HostFactory.Run(x =>
            {
                x.Service<NotifyService>(s =>
                {
                    s.ConstructUsing(timestampservice => new NotifyService());
                    s.WhenStarted(timestampservice => timestampservice.Start());
                    s.WhenStopped(timestampservice => timestampservice.Stop());
                });
                x.UseNLog();

                x.RunAsLocalSystem();

                x.SetServiceName("NotifyService");
                x.SetDisplayName("NotifyService");
                x.SetDescription("This is a service which is notify and is logging timestamp using NLog");
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
