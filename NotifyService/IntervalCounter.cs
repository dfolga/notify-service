using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotifyService
{
    public abstract class IntervalCounter
    {

        public static double CountInterval(List<string> hours)
        {

            foreach (var x in hours)
            {
                DateTime scheduleTime = DateTime.Parse(x);
                if (scheduleTime > DateTime.Now)
                {
                    return scheduleTime.Subtract(DateTime.Now).TotalMilliseconds;
                }
                
            }
            return DateTime.Parse(hours.ElementAt(0)).AddDays(1).Subtract(DateTime.Now).TotalMilliseconds;
        }
    }
}
