using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Serilog;

using ts.db;


namespace ts.api
{
    public class JobRepo : IJobRepo
    {
        private readonly IAccountContextProvider _accountContextProvider;
        private readonly ILogger _logger;
        private readonly INotificationRepo _notificationRepo;
        private readonly IPilotRepo _pilotRepo;

        public JobRepo(IAccountContextProvider accountContextProvider, ILogger logger, INotificationRepo notificationRepo, IPilotRepo pilotRepo)
        {
            _accountContextProvider = accountContextProvider;
            _logger = logger;
            _notificationRepo = notificationRepo;
            _pilotRepo = pilotRepo;
        }

        public async Task Update(long userid, UserDataDto data)
        {
            await updateRepo(userid, data);
            await updateNotifications(userid, data);
        }

        private async Task updateRepo(long userid, UserDataDto data)
        {
            using (var ctx = _accountContextProvider.Context)
            {
                ctx.Jobs.RemoveRange(await ctx.Jobs.Where(x => x.UserId == userid).ToListAsync());
                foreach (var j in data.Jobs)
                    j.UserId = userid;

                ctx.Jobs.AddRange(data.Jobs);

                await ctx.SaveChangesAsync();
            }
        }

        private async Task updateNotifications(long userid, UserDataDto data)
        {
            var pilots = await _pilotRepo.GetAll(userid);

            foreach (var p in pilots)
            {
                var pd = data.Pilots.FirstOrDefault(x => x.Name == p.Name);
                Debug.Assert(pd != null);

                var actualManufacturingCount = data.Jobs.Count(x => x.Owner == p.Name && x.IsManufacturing);
                var actualResearchCount = data.Jobs.Count(x => x.Owner == p.Name && !x.IsManufacturing);

                if (p.FreeManufacturingJobsNofificationCount > 0)
                {
                    if (actualManufacturingCount >= pd.MaxManufacturingJobs)
                    {
                        _logger.Debug("{method} resetting notification", "JobRepo::updateNotifications");
                        await _pilotRepo.SetFreeManufacturingJobsNofificationCount(p.PilotId, 0);                  // reset notification - maximum number of jobs running                        
                    }
                }
                else
                {
                    if (actualManufacturingCount < pd.MaxManufacturingJobs)
                    {   // notify about free manufacturing slots
                        _logger.Debug("{method} scheduling man notification for {pilot}", "JobRepo::updateNotifications", p.Name);
                        await _notificationRepo.IssueNew(userid, p.Name,
                            $"{pd.MaxManufacturingJobs - actualManufacturingCount} free manufacturing slots");
                        await _pilotRepo.SetFreeManufacturingJobsNofificationCount(p.PilotId, 1);
                    }
                }

                if (p.FreeResearchJobsNofificationCount > 0)
                {
                    if (actualResearchCount >= pd.MaxResearchJobs)
                    {
                        _logger.Debug("{method} resetting notification", "JobRepo::updateNotifications");
                        await _pilotRepo.SetFreeResearchJobsNofificationCount(p.PilotId, 0);                  // reset notification - maximum number of jobs running                        
                    }
                }
                else
                {
                    if (actualResearchCount < pd.MaxResearchJobs)
                    {   // notify about free research slots
                        _logger.Debug("{method} scheduling research notification for {pilot}", "JobRepo::updateNotifications", p.Name);
                        await _notificationRepo.IssueNew(userid, p.Name,
                            $"{pd.MaxResearchJobs - actualResearchCount} free research slots");
                        await _pilotRepo.SetFreeResearchJobsNofificationCount(p.PilotId, 1);
                    }
                }
            }
        }

        public async Task<ICollection<Job>> GetAll(long userid)
        {
            using (var ctx = _accountContextProvider.Context)
            {
                var user = await ctx.Users.Include(c => c.Jobs).SingleOrDefaultAsync(u => u.UserId == userid);
                if (user == null)
                    throw new UserException(strings.SecurityException);

                return user.Jobs;
            }
        }
    }
}
