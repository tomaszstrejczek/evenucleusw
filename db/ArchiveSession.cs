using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ts.db
{
    public class ArchiveSession
    {
        public long ArchiveSessionId { get; set; }
        public string SessionId { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime SessionEnd { get; set; }
        public DateTime SessionAccess { get; set; }

        public long UserId { get; set; }
    }
}
