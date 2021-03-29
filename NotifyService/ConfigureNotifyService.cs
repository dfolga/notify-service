
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using Topshelf;

namespace NotifyService
{
   
    class ConfigureNotifyService
    {

        
        static void Main(string[] args)
        {
            NotifyHours notifyHours = null;
            try
            {
                using (StreamReader r = new StreamReader("input.json"))
                {
                    string json = r.ReadToEnd();
                    notifyHours = JsonConvert.DeserializeObject<NotifyHours>(json);
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Error: "+ex);
            }

            var rc = HostFactory.Run(x =>
            {
                x.Service<NotifyService>(s =>
                {
                    s.ConstructUsing(notifyservice => new NotifyService(IntervalCounter.CountInterval(notifyHours.Hours),notifyHours.Hours));
                    s.WhenStarted(notifyservice => notifyservice.Start());
                    s.WhenStopped(notifyservice => notifyservice.Stop());
                });
                x.UseNLog();

                x.RunAsLocalSystem();

                x.SetServiceName("NotifyService");
                x.SetDisplayName("NotifyService");
                x.SetDescription("This is a service which is notify and is logging  using NLog");
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
