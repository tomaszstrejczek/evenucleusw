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
    public class CorporationRepo : ICorporationRepo
    {
        private readonly IAccountContextProvider _accountContextProvider;
        private readonly ILogger _logger;
        private readonly IEveApi _eveApi;

        public CorporationRepo(IAccountContextProvider accountContextProvider, ILogger logger, IEveApi eveApi)
        {
            _accountContextProvider = accountContextProvider;
            _logger = logger;
            _eveApi = eveApi;
        }

        public async Task Update(long userid, UserDataDto data)
        {
            _logger.Debug("{method} {userid}", "CorporationRepo::Update", userid);
            using (var ctx = _accountContextProvider.Context)
            {
                var user = await ctx.Users.Include(c => c.Corporations).SingleOrDefaultAsync(u => u.UserId == userid);
                var validCorpoNames = data.Pilots.Select(x => x.Name);

                // remove inactive corporations
                var storedCorpos = user.Corporations.ToList();
                var inactive = storedCorpos.Where(x => !validCorpoNames.Contains(x.Name)).ToList();
                ctx.RemoveRange(inactive);
                foreach (var r in inactive)
                    _logger.Debug("{method} removing {pilot}", "CorporationRepo::Update", r.Name);

                // add newly revealed pilots
                var toadd = data.Corporations;
                foreach (var a in toadd)
                {
                    a.UserId = userid;
                    if (storedCorpos.All(x => x.EveId != a.EveId))
                    {
                        user.Corporations.Add(a);
                        ctx.Corporations.Add(a);
                    }
                }

                await ctx.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Corporation>> GetAll(long userid)
        {
            using (var ctx = _accountContextProvider.Context)
            {
                var user = await ctx.Users.Include(c => c.Corporations).SingleOrDefaultAsync(u => u.UserId == userid);
                return user.Corporations;
            }
        }

        public async Task SimpleUpdateFromKey(long userid, long keyid, string vcode)
        {
            _logger.Debug("{method} {userid} {keyid}", "CorporationRepo::SimpleUpdateFromKey", userid, keyid);

            using (var ctx = _accountContextProvider.Context)
            {
                var corpos = (await GetAll(userid)).Select(x => x.EveId);
                var corpo = _eveApi.GetCorporations(keyid, vcode);
                if (corpos.Contains(corpo.CorporationId))
                    return;

                var toadd = new Corporation()
                {
                    EveId = corpo.CorporationId,
                    KeyInfoId = keyid,
                    Name = corpo.CorporationName
                };

                ctx.Corporations.Add(toadd);

                await ctx.SaveChangesAsync();
            }
        }

        public async Task DeleteByKey(long keyinfoid)
        {
            _logger.Debug("{method} {keyid}", "CorporationRepo::DeleteByKey", keyinfoid);
            using (var ctx = _accountContextProvider.Context)
            {
                var keyinfo = await ctx.KeyInfos.SingleOrDefaultAsync(k => k.KeyInfoId == keyinfoid);
                if (keyinfo == null)
                    return;
                ctx.Corporations.RemoveRange(await ctx.Corporations.Where(p => p.KeyInfoId == keyinfo.KeyInfoId).ToListAsync());
                ctx.KeyInfos.Remove(keyinfo);

                await ctx.SaveChangesAsync();
            }

            //_localdb.Execute("delete from wallettransaction where CorporationId=?", p.CorporationId);
            //_localdb.Execute("delete from journalentry where CorporationId=?", p.CorporationId);
            //_localdb.Execute("delete from corporation where CorporationId=?", p.CorporationId);
        }
    }
}
