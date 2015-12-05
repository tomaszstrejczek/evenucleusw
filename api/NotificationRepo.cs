using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Serilog;
using ts.db;

namespace ts.api
{
    public class NotificationRepo : INotificationRepo
    {
        readonly ILogger _logger;
        readonly IAccountContextProvider _accountContextProvider;

        public NotificationRepo(IAccountContextProvider accountContextProvider, ILogger logger)
        {
            _logger = logger;
            _accountContextProvider = accountContextProvider;
        }

        public async Task<long> IssueNew(long userid, string message, string message2)
        {
            _logger.Debug("{method} {userid}", "NotificationRepo::IssueNew", userid);

            using (var ctx = _accountContextProvider.Context)
            {
                var n = new Notification()
                {
                    Status = NotificationStatus.NotSeen,
                    CreatedOn = DateTime.UtcNow,
                    Message = message,
                    Message2 = message2,
                    UserId = userid
                };

                ctx.Notifications.Add(n);
                await ctx.SaveChangesAsync();

                return n.NotificationId;
            }
        }

        public async Task<ICollection<Notification>>  GetAll(long userid)
        {
            _logger.Debug("{method} {userid}", "NotificationRepo::GetAll", userid);

            using (var ctx = _accountContextProvider.Context)
            {
                var user = await ctx.Users.Include(c => c.Notifications).SingleOrDefaultAsync(x => x.UserId == userid);
                if (user == null)
                    throw new UserException(strings.SecurityException);

                return user.Notifications;
            }
        }

        public async Task Remove(long notificationId)
        {
            _logger.Debug("{method} {notificationid}", "NotificationRepo::Remove", notificationId);

            using (var ctx = _accountContextProvider.Context)
            {
                var todel = new Notification() {NotificationId = notificationId};
                ctx.Notifications.Attach(todel);
                ctx.Notifications.Remove(todel);
                await ctx.SaveChangesAsync();
            }
        }
    }
}
