using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ts.db
{
    public class CacheEntry
    {
        public string Key { get; set; }
        public DateTime CachedUntil { get; set; }
        public string Data { get; set; }
    }
}
