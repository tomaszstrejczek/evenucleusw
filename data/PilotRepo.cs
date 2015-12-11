using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Serilog;

using ts.domain;

namespace ts.data
{
    public class PilotRepo : IPilotRepo
    {
        private readonly IAccountContextProvider _accountContextProvider;
        private readonly ILogger _logger;
        private readonly IEveApi _eveApi;

        public PilotRepo(IAccountContextProvider accountContextProvider, ILogger logger, IEveApi eveApi)
        {
            _accountContextProvider = accountContextProvider;
            _logger = logger;
            _eveApi = eveApi;
        }

        public async Task Update(long userid, UserDataDto data)
        {
            _logger.Debug("{method} {userid}", "PilotRepo::Update", userid);
            using (var ctx = _accountContextProvider.Context)
            {
                var user = await ctx.Users.Include(c => c.Pilots).SingleOrDefaultAsync(u => u.UserId == userid);
                if (user == null)
                    throw new UserException(strings.SecurityException);
                var validPilotNames = data.Pilots.Select(x => x.Name);

                // remove inactive pilots
                var storedPilots = user.Pilots.ToList();
                var inactive = storedPilots.Where(x => !validPilotNames.Contains(x.Name)).ToList();
                foreach (var r in inactive)
                    _logger.Debug("{method} removing {pilot}", "pilotRepo::updatePilotData", r.Name);
                ctx.Pilots.RemoveRange(inactive);

                // add/update pilots
                var toadd = data.Pilots;
                foreach (var a in toadd)
                {
                    _logger.Debug("{method} adding {pilot}", "pilotRepo::updatePilotData", a.Name);

                    var stored = storedPilots.FirstOrDefault(x => x.EveId == a.EveId && x.UserId==userid);
                    var pilot = a;
                    pilot.FreeManufacturingJobsNofificationCount = stored == null ? 0 : stored.FreeManufacturingJobsNofificationCount;
                    pilot.FreeResearchJobsNofificationCount = stored == null ? 0 : stored.FreeResearchJobsNofificationCount;
                    // We deliberatly don't update children skills - so skillrepo may issue appropriate notifications
                    pilot.KeyInfoId= a.KeyInfoId;
                    pilot.UserId = userid;

                    if (stored == null)
                    {
                        user.Pilots.Add(pilot);
                        ctx.Pilots.Add(pilot);
                    }
                }

                await ctx.SaveChangesAsync();
            }                
        }

        public async Task<ICollection<Pilot>> GetAll(long userid)
        {
            _logger.Debug("{method} {userid}", "PilotRepo::GetAll", userid);
            using (var ctx = _accountContextProvider.Context)
            {
                var pilots = await ctx.Pilots.Include(p => p.Skills).Where(c => c.UserId == userid).ToListAsync();
                return pilots;
            }
        }

        public async Task<ICollection<Pilot>> GetByKeyInfoId(long keyinfoid)
        {
            _logger.Debug("{method} {keyinfoid}", "PilotRepo::GetByKeyInfoId", keyinfoid);
            using (var ctx = _accountContextProvider.Context)
            {
                var pilots = await ctx.Pilots.Include(p => p.Skills).Where(c => c.KeyInfoId== keyinfoid).ToListAsync();
                return pilots;
            }
        }

        public async Task SetFreeManufacturingJobsNofificationCount(long pilotid, int value)
        {
            _logger.Debug("{method} {pilotid} {value}", "PilotRepo::SetFreeManufacturingJobsNofificationCount", pilotid, value);
            using (var ctx = _accountContextProvider.Context)
            {
                var pilot = await ctx.Pilots.SingleOrDefaultAsync(p => p.PilotId == pilotid);
                if (pilot == null)
                    throw new UserException(strings.SecurityException);

                pilot.FreeManufacturingJobsNofificationCount = value;
                await ctx.SaveChangesAsync();
            }
        }

        public async Task SetFreeResearchJobsNofificationCount(long pilotid, int value)
        {
            _logger.Debug("{method} {pilotid} {value}", "PilotRepo::SetFreeResearchJobsNofificationCount", pilotid, value);
            using (var ctx = _accountContextProvider.Context)
            {
                var pilot = await ctx.Pilots.SingleOrDefaultAsync(p => p.PilotId == pilotid);
                if (pilot == null)
                    throw new UserException(strings.SecurityException);

                pilot.FreeResearchJobsNofificationCount = value;
                await ctx.SaveChangesAsync();
            }
        }

        public async Task SimpleUpdateFromKey(long userid, long keyinfoid, long keyid, string vcode)
        {
            _logger.Debug("{method} {userid} {keyid} {keyinfoid}", "pilotRepo::SimpleUpdateFromKey", userid, keyid, keyinfoid);

            var pilots = (await GetAll(userid)).Select(x => x.EveId);
            var characters = _eveApi.GetCharacters(keyid, vcode);
            var toadd = characters.Where(x => !pilots.Contains(x.CharacterId));
            var pilotsToAdd =
                toadd.Select(a => new Pilot() { UserId = userid, Name = a.CharacterName, KeyInfoId = keyinfoid, EveId = a.CharacterId }).ToList();
            using (var ctx = _accountContextProvider.Context)
            {
                ctx.Pilots.AddRange(pilotsToAdd);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task DeleteByKey(long keyinfoid)
        {
            _logger.Debug("{method} {keyid}", "pilotRepo::DeleteByKey", keyinfoid);
            using (var ctx = _accountContextProvider.Context)
            {
                var keyinfo = await ctx.KeyInfos.SingleOrDefaultAsync(k => k.KeyInfoId == keyinfoid);
                if (keyinfo == null)
                    return;
                ctx.Pilots.RemoveRange(await ctx.Pilots.Where(p => p.KeyInfoId == keyinfo.KeyInfoId).ToListAsync());
                ctx.KeyInfos.Remove(keyinfo);

                //_localdb.Execute("delete from skill where CharacterId=?", p.CharacterId);
                //_localdb.Execute("delete from wallettransaction where CharacterId=?", p.CharacterId);
                //_localdb.Execute("delete from journalentry where CharacterId=?", p.CharacterId);
                //_localdb.Execute("delete from pilot where CharacterId=?", p.CharacterId);

                await ctx.SaveChangesAsync();
            }
        }
    }
}
