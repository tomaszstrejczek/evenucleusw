using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Data.Entity;
using Serilog;

using ts.domain;

namespace ts.data
{
    public class SkillRepo : ISkillRepo
    {
        private readonly ILogger _logger;
        private readonly INotificationRepo _notificationRepo;
        private readonly IAccountContextProvider _accountContextProvider;

        public SkillRepo(IAccountContextProvider accountContextProvider, ILogger logger, INotificationRepo notificationRepo)
        {
            _logger = logger;
            _accountContextProvider = accountContextProvider;
            _notificationRepo = notificationRepo;
        }

        public async Task Update(long userId, UserDataDto data)
        {
            _logger.Debug("{method} {userid}", "SkillRepo::Update", userId);
            using (var ctx = _accountContextProvider.Context)
            {
                var pilots = await ctx.Pilots.Include(c => c.Skills).Where(x => x.UserId == userId).ToListAsync();

                foreach (var p in pilots)
                {
                    var pd = data.Pilots.FirstOrDefault(x => x.Name == p.Name);
                    Debug.Assert(pd != null);

                    var storedSkills = p.Skills;
                    bool suspendNotification = storedSkills.Count == 0;   // suspend notification if the pilot is seen for the first time

                    var toremove = storedSkills.Where(x => !pd.TrainedSkills.Contains(x.SkillName)).ToList();
                    // it is not expected that we need to remove a skill. Probably it could happen if skills are renamed or skills are lost due to clone kill
                    // however if a skill is leveled up than we technically removed old level and replaced with a new level. No notification should be sent in such case
                    foreach (var r in toremove)
                    {
                        var leveledUp =
                            pd.TrainedSkills.FirstOrDefault(
                                x =>
                                    x.Length == r.SkillName.Length &&
                                    x.Substring(0, x.Length - 1) == r.SkillName.Substring(0, x.Length - 1));

                        if (leveledUp == null)
                        {
                            _logger.Debug("{method} removing skill {skill} for {pilot}", "SkillRepo::Update", r.SkillName, p.Name);
                            await _notificationRepo.IssueNew(userId, p.Name, $"{r.SkillName} removed");
                        }
                    }
                    ctx.Skills.RemoveRange(toremove);

                    var toadd = pd.TrainedSkills.Where(x => !storedSkills.Select(y => y.SkillName).Contains(x));
                    foreach (var a in toadd)
                    {
                        _logger.Debug("{method} adding skill {skill} for {pilot}", "SkillRepo::Update", a, p.Name);
                        var skill = new Skill()
                        {
                            PilotId = p.PilotId,
                            SkillName = a,
                        };
                        p.Skills.Add(skill);
                        ctx.Skills.Add(skill);

                        if (!suspendNotification)
                        {
                            await _notificationRepo.IssueNew(userId, p.Name, $"{a} trained");
                        }
                    }
                }

                await ctx.SaveChangesAsync();
            }
        }

        public async Task<List<string>> GetForPilot(long pilotId)
        {
            _logger.Debug("{method} {userid}", "SkillRepo::GetForPilot", pilotId);

            using (var ctx = _accountContextProvider.Context)
            {
                var pilot = await ctx.Pilots.Include(c => c.Skills).SingleOrDefaultAsync(p => p.PilotId == pilotId);
                if (pilot == null)
                    throw new UserException(strings.SecurityException);

                return pilot.Skills.Select(s => s.SkillName).ToList();
            }
        }
    }
}
