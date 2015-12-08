using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ts.domain
{
    public enum NotificationStatus
    {
        NotSeen,
        Seen
    }
    public class Notification
    {
        public long NotificationId { get; set; }
        public long UserId { get; set; }

        public string Message { get; set; }
        public string Message2 { get; set; }
        public string Error { get; set; }

        public NotificationStatus Status { get; set; }

        public DateTime CreatedOn { get; set; }
    }

}
