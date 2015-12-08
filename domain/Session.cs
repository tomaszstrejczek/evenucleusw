using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ts.domain
{
    public class Session
    {
        public string SessionId { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime SessionEnd { get; set; }
        public DateTime SessionAccess { get; set; }

        public long UserId { get; set; }
    }
}
