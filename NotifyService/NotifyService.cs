using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace NotifyService
{
    class NotifyService
    {
        private readonly Timer _timer;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public NotifyService()
        {
            _timer = new Timer();
            _timer.Interval = 60 * 1000;
            _timer.Elapsed += new ElapsedEventHandler(SixtySecondsEvent);
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private static void SixtySecondsEvent(object sender, ElapsedEventArgs e)
        {
            
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
