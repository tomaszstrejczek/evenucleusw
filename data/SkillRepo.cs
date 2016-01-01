using System;
using System.CodeDom;
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

                    var toremove = storedSkills.Where(x => pd.Skills.All(z => z.SkillName != x.SkillName));
                    // it is not expected that we need to remove a skill. Probably it could happen if skills are renamed or skills are lost due to clone kill
                    foreach (var r in toremove)
                    {
                        _logger.Debug("{method} removing skill {skill} for {pilot}", "SkillRepo::Update", r.SkillName, p.Name);
                        await _notificationRepo.IssueNew(userId, p.Name, $"{r.SkillName} {r.Level} removed");
                    }
                    ctx.Skills.RemoveRange(toremove);

                    var toadd = pd.Skills.Where(x => storedSkills.All(y => y.SkillName!=x.SkillName));
                    foreach (var a in toadd)
                    {
                        _logger.Debug("{method} adding skill {skill} for {pilot}", "SkillRepo::Update", a, p.Name);
                        var skill = new Skill()
                        {
                            PilotId = p.PilotId,
                            SkillName = a.SkillName,
                            Level = a.Level
                        };
                        p.Skills.Add(skill);
                        ctx.Skills.Add(skill);

                        if (!suspendNotification)
                        {
                            await _notificationRepo.IssueNew(userId, p.Name, $"{a.SkillName} {a.Level} trained");
                        }
                    }

                    // Changed level
                    foreach (var s in pd.Skills)
                    {
                        var found = storedSkills.FirstOrDefault(x => x.SkillName == s.SkillName && x.Level != s.Level);
                        if (found != null)
                        {
                            found.Level = s.Level;
                            if (!suspendNotification)
                            {
                                await _notificationRepo.IssueNew(userId, p.Name, $"{s.SkillName} {s.Level} trained");
                            }
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
