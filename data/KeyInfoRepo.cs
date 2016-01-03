using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Serilog;
using ts.domain;

namespace ts.data
{
    public class KeyInfoRepo : IKeyInfoRepo
    {
        private readonly ILogger _logger;
        private readonly IAccountContextProvider _accountContextProvider;

        public KeyInfoRepo(ILogger logger, IAccountContextProvider accountContextProvider)
        {
            _logger = logger;
            _accountContextProvider = accountContextProvider;
        }

        public async Task<long> AddKey(long userid, long keyid, string vcode)
        {
            _logger.Debug("{method} {userid} {keyid}", "KeyRepo::AddKey", userid, keyid);

            using (var ctx = _accountContextProvider.Context)
            {
                var user = await ctx.Users.Include(c => c.KeyInfos).SingleOrDefaultAsync(x => x.UserId == userid);

                if (user.KeyInfos.Any(x => x.KeyId == keyid))
                {
                    _logger.Debug("{method} strings.ErrorKeyAlreadyDefined", "UserRepo::AddKey");
                    throw new UserException(strings.ErrorKeyAlreadyDefined);
                }

                var k = new KeyInfo()
                {
                    KeyId = keyid,
                    VCode = vcode,
                    UserId = userid
                };
                ctx.KeyInfos.Add(k);
                user.KeyInfos.Add(k);
                await ctx.SaveChangesAsync();

                return k.KeyInfoId;
            }
        }

        public async Task DeleteKey(long keyinfoid)
        {
            _logger.Debug("{method} {keyid}", "KeyRepo::DeleteKey", keyinfoid);
            using (var ctx = _accountContextProvider.Context)
            {
                var keyInfo = ctx.KeyInfos.First(k => k.KeyInfoId == keyinfoid);
                var user =
                    ctx.Users.Include(u => u.KeyInfos)
                        .Include(u => u.Pilots)
                        .Include(u => u.Corporations)
                        .First(u => u.UserId == keyInfo.UserId);
                var pilotsToRemove = user.Pilots.Where(p => p.KeyInfoId == keyInfo.KeyInfoId).ToList();
                var corposToRemove = user.Corporations.Where(c => c.KeyInfoId == keyInfo.KeyInfoId).ToList();

                foreach (var p in pilotsToRemove)
                {
                    user.Pilots.Remove(p);
                    ctx.Pilots.Remove(p);
                }
                foreach (var c in corposToRemove)
                {
                    user.Corporations.Remove(c);
                    ctx.Corporations.Remove(c);
                }
                ctx.KeyInfos.Remove(keyInfo);

                await ctx.SaveChangesAsync();
            }
        }

        public async Task<ICollection<KeyInfo>> GetKeys(long userid)
        {
            _logger.Debug("{method} {userid}", "KeyRepo::GetKeys", userid);
            using (var ctx = _accountContextProvider.Context)
            {
                var found = await ctx.Users.Include(a => a.KeyInfos).SingleOrDefaultAsync(x => x.UserId == userid);
                if (found == null)
                    throw new UserException(strings.SecurityException);

                return found.KeyInfos;
            }
        }

        public async Task<KeyInfo> GetById(long keyinfoid)
        {
            _logger.Debug("{method} {userid}", "KeyRepo::GetById", keyinfoid);
            using (var ctx = _accountContextProvider.Context)
            {
                var found = await ctx.KeyInfos.SingleOrDefaultAsync(x => x.KeyInfoId == keyinfoid);

                return found;
            }
        }
    }
}
