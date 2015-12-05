using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ts.api;
using ts.db;


namespace ring0
{
    [TestClass]
    public class KeyRepoT: TestBase
    {
        [TestMethod]
        public async Task AddKeyInfo()
        {
            var accrepo = AccountRepo;
            var skey = await accrepo.RegisterUser("alakeyinfo1@kot.pies", "123");
            var userid = await accrepo.CheckSession(skey);

            var repo = new KeyInfoRepo(Logger, AccountContextProvider);

            var keyinfoid1 = await repo.AddKey(userid, 1, "ala");
            var result = await repo.GetKeys(userid);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("ala", result.FirstOrDefault().VCode);

            var keyinfoid2 = await repo.AddKey(userid, 2, "kot");
            result = await repo.GetKeys(userid);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("ala", result.First(x => x.KeyId == 1).VCode);
            Assert.AreEqual("kot", result.First(x => x.KeyId == 2).VCode);

            await repo.DeleteKey(keyinfoid1);
            result = await repo.GetKeys(userid);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("kot", result.FirstOrDefault().VCode);

            await repo.DeleteKey(keyinfoid2);
            result = await repo.GetKeys(userid);
            Assert.AreEqual(0, result.Count);
        }
    }
}
