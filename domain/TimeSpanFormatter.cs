using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ts.domain
{
    public static class TimeSpanFormatter
    {
        public static string Format(TimeSpan val)
        {
            if (val.Days > 0)
                return string.Format("{0}d {1}h {2}m", val.Days, val.Hours, val.Minutes);

            if (val.Hours > 0)
                return string.Format("{0}h {1}m", val.Hours, val.Minutes);

            return string.Format("{0}m", val.Minutes);
        }
    }
}
