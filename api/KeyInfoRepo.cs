using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Serilog;
using ts.db;

namespace ts.api
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
                var existing = await ctx.Users.SingleOrDefaultAsync(x => x.UserId == userid && x.KeyInfos.Any(z => z.KeyId==keyid));
                if (existing != null)
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
                await ctx.SaveChangesAsync();

                return k.KeyInfoId;
            }
        }

        public async Task DeleteKey(long keyinfoid)
        {
            _logger.Debug("{method} {keyid}", "KeyRepo::DeleteKey", keyinfoid);
            using (var ctx = _accountContextProvider.Context)
            {
                var toremove = new KeyInfo() {KeyInfoId = keyinfoid};
                ctx.KeyInfos.Attach(toremove);
                ctx.KeyInfos.Remove(toremove);
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
