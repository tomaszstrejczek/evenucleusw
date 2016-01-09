using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ts.data;
using ts.domain;

namespace ring0
{
    [TestClass]
    public class AccountContextT: TestBase
    {
        [TestMethod]
        public async Task Register()
        {
            var repo = AccountRepo;

            var skey1 = await repo.RegisterUser("ala@kot.pies", "123");
            var skey2 = await repo.Login("ala@kot.pies", "123");

            Assert.AreNotEqual(skey1, skey2);
        }

        [TestMethod]
        public async Task InvalidUserPassword()
        {
            var repo = AccountRepo;

            string msg1 = "", msg2 = "", msg3 = "";

            await repo.RegisterUser("ala@kot2.pies", "123");

            // invalid user and password
            try
            {
                await repo.Login("ala@wrong1", "12345");
            }
            catch (UserException ex)
            {
                msg1 = ex.Message;
            }

            // invalid user, valid password
            try
            {
                await repo.Login("ala@wrong1", "123");
            }
            catch (UserException ex)
            {
                msg2 = ex.Message;
            }

            // valid user, invalid password
            try
            {
                await repo.Login("ala@kot2.pies", "12345");
            }
            catch (UserException ex)
            {
                msg3 = ex.Message;
            }

            Assert.AreEqual(strings.InvalidUserPassword, msg1);
            Assert.AreEqual(msg1, msg2);
            Assert.AreEqual(msg1, msg3);

            await repo.Login("ala@kot2.pies", "123");
        }

        [TestMethod]
        public async Task RegisterTheSameTwice()
        {
            var repo = AccountRepo;

            await repo.RegisterUser("ala@kot3.pies", "123");
            try
            {
                await repo.RegisterUser("ala@kot3.pies", "123");
                Assert.Fail("Unreachable");
            }
            catch (UserException ex)
            {
                Assert.AreEqual(strings.AccountAlreadyExists, ex.Message);
            }
        }

        [TestMethod]
        public async Task RegisterLongPassword()
        {
            var repo = AccountRepo;
            var csprng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[16*1024];
            csprng.GetBytes(salt);
            var password = Convert.ToBase64String(salt);

            var skey1 = await repo.RegisterUser("ala@kot4.pies", password);
            var skey2 = await repo.Login("ala@kot4.pies", password);

            Assert.AreNotEqual(skey1, skey2);

            try
            {
                await repo.Login("ala@kot4.pies", password + "ala");
                Assert.Fail("Unreachable");
            }
            catch (UserException ex)
            {
                Assert.AreEqual(strings.InvalidUserPassword, ex.Message);
            }

            try
            {
                await repo.Login("ala@kot4.pies", password.Substring(0, password.Length - 2));
                Assert.Fail("Unreachable");
            }
            catch (UserException ex)
            {
                Assert.AreEqual(strings.InvalidUserPassword, ex.Message);
            }
        }

        [TestMethod]
        public async Task CheckSession()
        {
            var repo = AccountRepo;
            var skey = await repo.RegisterUser("ala@kot5.pies", "123");

            var userid = await repo.CheckSession(skey);
            Assert.AreNotEqual(0, userid);
            await repo.Logout(skey);

            try
            {
                await repo.CheckSession(skey);
                Assert.Fail("Unreachable");
            }
            catch (UserException ex)
            {
                Assert.AreEqual(strings.InvalidSessionKey, ex.Message);
            }

            // let's check if it is in ArchiveSession now
            using (var db = new AccountContext())
            {
                var found = await db.ArchiveSessions.SingleOrDefaultAsync(s => s.SessionId == skey);
                Assert.IsNotNull(found);
            }

            // let's simulate elapsed time
            skey = await repo.Login("ala@kot5.pies", "123");
            using (var db = new AccountContext())
            {
                var found = await db.Sessions.SingleOrDefaultAsync(s => s.SessionId == skey);
                Assert.IsNotNull(found);
                found.SessionAccess = DateTime.UtcNow.AddMinutes(-21);
                db.Sessions.Update(found);
                await db.SaveChangesAsync();
            }

            try
            {
                await repo.CheckSession(skey);
                Assert.Fail("Unreachable");
            }
            catch (UserException ex)
            {
                Assert.AreEqual(strings.InvalidSessionKey, ex.Message);
            }

        }
    }
}