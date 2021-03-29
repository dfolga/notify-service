using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;

namespace NotifyService
{
    class NotifyService
    {
        private static Timer _timer = new Timer();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private List<string> hours;
        
        

        public NotifyService(double interval,List<string> hours)
        {
            this.hours = hours;
            _timer.Elapsed += new ElapsedEventHandler(NotifyEvent);
            _timer.AutoReset = true;
            _timer.Interval = interval;
        }
        
        public void NotifyEvent(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
            Console.WriteLine("Example Notification..." + "Counted interval = " + (IntervalCounter.CountInterval(hours) / 3600000));
            logger.Info("Successfully notified");
            _timer.Interval = IntervalCounter.CountInterval(hours);
        }
        public void Start()
        {
            _timer.Start();
            logger.Info("Service started!");
        }
        public void Stop()
        {
            _timer.Stop();
            logger.Info("Service stopped!");
        }
    }
}