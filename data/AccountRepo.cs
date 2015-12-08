using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using ts.domain;

namespace ts.data
{
    public class AccountRepo : IAccountRepo
    {
        private readonly IAccountContextProvider _accountContextProvider;

        public AccountRepo(IAccountContextProvider accountContextProvider)
        {
            _accountContextProvider = accountContextProvider;
        }

        public async Task<string> RegisterUser(string email, string password)
        {
            using (var db = _accountContextProvider.Context)
            {
                var exists = await db.Users.SingleOrDefaultAsync(u => u.Email == email);
                if (exists != null)
                    throw new UserException(strings.AccountAlreadyExists);

                var n = new User()
                {
                    Email = email,
                    HashedPassword = PasswordHash.CreateHash(password)
                };

                db.Users.Add(n);
                await db.SaveChangesAsync();
            }

            return await Login(email, password);
        }

        public async Task<string> Login(string email, string password)
        {
            using (var db = _accountContextProvider.Context)
            {
                var exists = await db.Users.SingleOrDefaultAsync(u => u.Email == email);
                if (exists == null)
                    throw new UserException(strings.InvalidUserPassword);

                var b = PasswordHash.ValidatePassword(password, exists.HashedPassword);
                if (!b)
                    throw new UserException(strings.InvalidUserPassword);

                // generate session key
                var provider = new RNGCryptoServiceProvider();
                byte[] skey = new byte[32];
                provider.GetBytes(skey);
                var session = new Session()
                {
                    UserId = exists.UserId,
                    SessionStart = DateTime.UtcNow,
                    SessionId = Convert.ToBase64String(skey),
                    SessionAccess = DateTime.UtcNow
                };

                db.Sessions.Add(session);
                await db.SaveChangesAsync();

                return session.SessionId;
            }
        }

        public async Task Logout(string skey)
        {
            using (var db = _accountContextProvider.Context)
            {
                var ses = await db.Sessions.SingleOrDefaultAsync(s => s.SessionId == skey);
                if (ses == null)
                    return;

                var archiveSession = new ArchiveSession()
                {
                    SessionId = ses.SessionId,
                    SessionStart = ses.SessionStart,
                    SessionEnd = DateTime.UtcNow,
                    SessionAccess = ses.SessionAccess,
                    UserId = ses.UserId
                };

                db.ArchiveSessions.Add(archiveSession);
                db.Sessions.Remove(ses);

                await db.SaveChangesAsync();
            }
        }

        public async Task<long> CheckSession(string skey)
        {
            using (var db = _accountContextProvider.Context)
            {
                var ses = await db.Sessions.SingleOrDefaultAsync(s => s.SessionId == skey);
                if (ses == null)
                    throw new UserException(strings.InvalidSessionKey);

                var diff = DateTime.UtcNow - ses.SessionAccess;
                if (diff.TotalMinutes < 20)
                {
                    ses.SessionAccess = DateTime.UtcNow;
                    db.Sessions.Update(ses);
                    await db.SaveChangesAsync();
                    return ses.UserId;
                }
            }

            await Logout(skey);
            throw new UserException(strings.InvalidSessionKey);
        }
    }
}
