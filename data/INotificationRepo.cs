using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ts.domain;

namespace ts.data
{
    public interface INotificationRepo
    {
        Task<long> IssueNew(long userid, string message, string message2);
        Task<ICollection<Notification>> GetAll(long userid);
        Task Remove(long notificationId);
    }
}
