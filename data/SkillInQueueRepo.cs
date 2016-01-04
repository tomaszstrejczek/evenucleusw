using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using AutoMapper;
using Microsoft.Data.Entity;
using Serilog;

using ts.domain;
using ts.dto;

namespace ts.data
{
    public class SkillInQueueRepo : ISkillInQueueRepo
    {
        private readonly ILogger _logger;
        private readonly IAccountContextProvider _accountContextProvider;

        public SkillInQueueRepo(IAccountContextProvider accountContextProvider, ILogger logger)
        {
            _logger = logger;
            _accountContextProvider = accountContextProvider;
        }

        public async Task Update(long userId, UserDataDto data)
        {
            _logger.Debug("{method} {userid}", "SkillInQueueRepo::Update", userId);
            using (var ctx = _accountContextProvider.Context)
            {
                var pilots = await ctx.Pilots.Include(c => c.SkillsInQueue).Where(x => x.UserId == userId).ToListAsync();

                foreach (var p in pilots)
                {
                    var pd = data.Pilots.FirstOrDefault(x => x.Name == p.Name);
                    Debug.Assert(pd != null);

                    var storedSkills = p.SkillsInQueue;

                    var toremove = storedSkills.Where(x => pd.SkillsInQueue.All(z => z.SkillName != x.SkillName || z.Level != x.Level));
                    ctx.SkillsInQueue.RemoveRange(toremove);

                    var toadd = pd.SkillsInQueue.Where(x => storedSkills.All(y => y.SkillName != x.SkillName || y.Level != x.Level))
                        .Select(x => new SkillInQueue() { PilotId = p.PilotId, Length = x.Length, SkillName = x.SkillName, Level = x.Level, Order = x.Order});
                    p.SkillsInQueue.Clear();
                    foreach(var a in toadd)
                        p.SkillsInQueue.Add(a);
                }

                await ctx.SaveChangesAsync();
            }
        }

        public async Task<List<SkillInQueueDto>> GetForPilot(long pilotId)
        {
            _logger.Debug("{method} {userid}", "SkillInQueue::GetForPilot", pilotId);

            using (var ctx = _accountContextProvider.Context)
            {
                var pilot = await ctx.Pilots.Include(c => c.SkillsInQueue).SingleOrDefaultAsync(p => p.PilotId == pilotId);
                if (pilot == null)
                    throw new UserException(strings.SecurityException);

                return pilot.SkillsInQueue.Select(s => Mapper.Map<SkillInQueueDto>(s)).ToList();
            }
        }

    }
}
