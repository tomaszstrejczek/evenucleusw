using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ts.domain
{
    public class User
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }

        public virtual ICollection<KeyInfo> KeyInfos { get; set; }
        public virtual ICollection<Corporation> Corporations { get; set; }
        public virtual ICollection<Pilot> Pilots { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
