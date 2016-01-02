using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using ring0;
using ts.data;
using ts.domain;
using ts.services;

namespace ring2
{
    [TestClass]
    public class BackgroundUpdateT: TestBase
    {
        [TestMethod]
        public async Task SinglePilot()
        {
            // MicioGatto key
            long code = 3645238;
            string vcode = "sLOD3pSHwuzKtml3inm59qvVWHiKA3rULJY7KRsuWmmHrZ0c8qAZlftLDQIHvxBq";

            var accrepo = AccountRepo;
            var skey = await accrepo.RegisterUser("backgroundupdatesinglepilot@kot.pies", "123");
            var userid = await accrepo.CheckSession(skey);

            var keyRepo = new KeyInfoRepo(Logger, AccountContextProvider);
            await keyRepo.AddKey(userid, code, vcode);

            var pilotRepo = new PilotRepo(AccountContextProvider, Logger, EveApi);
            var corpoRepo = new CorporationRepo(AccountContextProvider, Logger, EveApi);
            var notificationRepo = new NotificationRepo(AccountContextProvider, Logger);
            var skillRepo = new SkillRepo(AccountContextProvider, Logger, notificationRepo);
            var skillInQueueRepo = new SkillInQueueRepo(AccountContextProvider, Logger);
            var evePilotDataService = new EvePilotDataService(keyRepo, EveApi, Logger, TypeNameDict);

            var backgroundUpdate = new BackgroundUpdate(evePilotDataService, pilotRepo, corpoRepo, skillRepo, skillInQueueRepo);

            await backgroundUpdate.Update(userid);

            var pilots = await pilotRepo.GetAll(userid);
            Assert.AreEqual(1, pilots.Count);

            var p = pilots.First();
            Assert.AreEqual("MicioGatto", p.Name);
            Assert.IsTrue(p.Skills.Count > 0);
            Assert.IsTrue(p.SkillsInQueue.Count > 0);

            Assert.IsTrue(p.Skills.Any(x => x.SkillName == "Interceptors"));
        }
    }
}
