using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ts.db;
using ts.api;

namespace ring0
{
    [TestClass]
    public class PilotRepoT: TestBase
    {
        [TestMethod]
        public async Task UpdatePilots1()
        {
            var accrepo = AccountRepo;
            var skey = await accrepo.RegisterUser("pilotRepo1@kot.pies", "123");
            var userid = await accrepo.CheckSession(skey);

            var notificationRepo = new NotificationRepo(AccountContextProvider, Logger);
            var skillRepo = new SkillRepo(AccountContextProvider, Logger, notificationRepo);
            var repo = new PilotRepo(AccountContextProvider, Logger, null);


            // stage 1
            var userData = new UserDataDto()
            {
                Pilots = new Pilot[]
                {
                    new Pilot()
                    {
                        EveId = 1,
                        KeyInfoId =  2,
                        Name = "Pilot1",
                        Skills = new List<Skill>()
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new List<Job>(),
            };

            await repo.Update(userid, userData);
            Assert.AreNotEqual(0, userData.Pilots[0].PilotId);

            var pilots = await repo.GetAll(userid);
            Assert.AreEqual(1, pilots.Count);
            Assert.AreEqual("Pilot1", pilots.First().Name);

            // stage 2
            var userData2 = new UserDataDto()
            {
                Pilots = new Pilot[]
                {
                    new Pilot()
                    {
                        EveId = 2,
                        KeyInfoId =  3,
                        Name = "Pilot2",
                        Skills = new List<Skill>()
                    },
                    new Pilot()
                    {
                        EveId = 3,
                        KeyInfoId =  3,
                        Name = "Pilot3",
                        Skills = new List<Skill>()
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new List<Job>(),
            };

            await repo.Update(userid, userData2);
            Assert.AreNotEqual(0, userData2.Pilots[0].PilotId);
            Assert.AreNotEqual(0, userData2.Pilots[1].PilotId);
            Assert.AreNotEqual(userData2.Pilots[0].PilotId, userData2.Pilots[1].PilotId);

            pilots = await repo.GetAll(userid);
            Assert.AreEqual(2, pilots.Count);
            Assert.AreEqual("Pilot2", pilots.First(x => x.Name == "Pilot2").Name);
            Assert.AreEqual("Pilot3", pilots.First(x => x.Name == "Pilot3").Name);

            // No notifications expected
            var notifications = await notificationRepo.GetAll(userid);
            Assert.AreEqual(0, notifications.Count);
        }

        [TestMethod]
        public async Task UpdateCorporations()
        {
            var accrepo = AccountRepo;
            var skey = await accrepo.RegisterUser("UpdateCorporations@kot.pies", "123");
            var userid = await accrepo.CheckSession(skey);
            var repo = new CorporationRepo(AccountContextProvider, Logger, null);

            // stage 1
            var userData = new UserDataDto()
            {
                Pilots = new List<Pilot>(),
                Corporations = new Corporation[]
                {
                    new Corporation() {EveId = 1, Name = "Corpo1", KeyInfoId = 1}
                }.ToList(),
                Jobs = new List<Job>(),
            };

            await repo.Update(userid, userData);
            Assert.AreNotEqual(0, userData.Corporations[0].CorporationId);

            var corporations = await repo.GetAll(userid);
            Assert.AreEqual(1, corporations.Count);
            Assert.AreEqual("Corpo1", corporations.First().Name);

            // stage 2
            var userData2 = new UserDataDto()
            {
                Pilots = new List<Pilot>(),
                Corporations = new Corporation[]
                {
                    new Corporation()  {EveId = 2, Name = "Corpo2", KeyInfoId = 2},
                    new Corporation()  {EveId = 3, Name = "Corpo3", KeyInfoId = 3}
                }.ToList(),
                Jobs = new List<Job>(),
            };

            await repo.Update(userid, userData2);
            Assert.AreNotEqual(0, userData2.Corporations[0].CorporationId);
            Assert.AreNotEqual(0, userData2.Corporations[1].CorporationId);
            Assert.AreNotEqual(userData2.Corporations[0].CorporationId, userData2.Corporations[1].CorporationId);

            corporations = await repo.GetAll(userid);
            Assert.AreEqual(2, corporations.Count);
            Assert.AreEqual("Corpo2", corporations.First(x => x.Name == "Corpo2").Name);
            Assert.AreEqual("Corpo3", corporations.First(x => x.Name == "Corpo3").Name);
        }

        [TestMethod]
        public async Task UpdateCorporations2()
        {
            var accrepo = AccountRepo;
            var skey = await accrepo.RegisterUser("UpdateCorporations2@kot.pies", "123");
            var userid = await accrepo.CheckSession(skey);

            var repo = new CorporationRepo(AccountContextProvider, Logger, null);

            // stage 1
            var userData = new UserDataDto()
            {
                Pilots = new List<Pilot>(),
                Corporations = new Corporation[]
                {
                    new Corporation() {EveId = 1, Name = "Corpo1", KeyInfoId = 1}
                }.ToList(),
                Jobs = new List<Job>(),
            };

            await repo.Update(userid, userData);
            var corporations = await repo.GetAll(userid);
            Assert.AreEqual(1, corporations.Count);
            Assert.AreEqual("Corpo1", corporations.First().Name);

            // stage 2
            var userData2 = new UserDataDto()
            {
                Pilots = new List<Pilot>(),
                Corporations = new Corporation[]
                {
                }.ToList(),
                Jobs = new List<Job>(),
            };

            await repo.Update(userid, userData2);
            corporations = await repo.GetAll(userid);
            Assert.AreEqual(0, corporations.Count);
        }

        [TestMethod]
        public async Task UpdatePilotsAndCorporations2()
        {
            var accrepo = AccountRepo;
            var skey = await accrepo.RegisterUser("UpdatePilotsAndCorporations2@kot.pies", "123");
            var userid = await accrepo.CheckSession(skey);
            var repo = new CorporationRepo(AccountContextProvider, Logger, null);

            var notificationRepo = new NotificationRepo(AccountContextProvider, Logger);
            var skillRepo = new SkillRepo(AccountContextProvider, Logger, notificationRepo);
            var pilotrepo = new PilotRepo(AccountContextProvider, Logger, null);
            var corporepo = new CorporationRepo(AccountContextProvider, Logger, null);

            // stage 1
            var userData = new UserDataDto()
            {
                Pilots = new Pilot[]
                {
                    new Pilot()
                    {
                        EveId = 1,
                        KeyInfoId =  1,
                        Name = "Pilot1",
                        Skills = new List<Skill>()
                    }
                }.ToList(),
                Corporations = new Corporation[]
                {
                    new Corporation() {EveId = 1, Name = "Corpo1", KeyInfoId = 2}
                }.ToList(),
                Jobs = new List<Job>(),
            };

            await pilotrepo.Update(userid, userData);
            await corporepo.Update(userid, userData);
            var pilots = await pilotrepo.GetAll(userid);
            Assert.AreEqual(1, pilots.Count);
            Assert.AreEqual("Pilot1", pilots.First().Name);

            var corporations = await corporepo.GetAll(userid);
            Assert.AreEqual(1, corporations.Count);
            Assert.AreEqual("Corpo1", corporations.First().Name);

            // stage 2
            var userData2 = new UserDataDto()
            {
                Pilots = new Pilot[]
                {
                }.ToList(),
                Corporations = new Corporation[]
                {
                }.ToList(),
                Jobs = new List<Job>(),
            };

            await pilotrepo.Update(userid, userData2);
            await corporepo.Update(userid, userData2);
            pilots = await pilotrepo.GetAll(userid);
            Assert.AreEqual(0, pilots.Count);
            corporations = await corporepo.GetAll(userid);
            Assert.AreEqual(0, corporations.Count);

            // No notifications expected
            var notifications = await notificationRepo.GetAll(userid);
            Assert.AreEqual(0, notifications.Count);
        }

        [TestMethod]
        public async Task SetMethods()
        {
            var accrepo = AccountRepo;
            var skey = await accrepo.RegisterUser("pilotRepo2@kot.pies", "123");
            var userid = await accrepo.CheckSession(skey);

            var notificationRepo = new NotificationRepo(AccountContextProvider, Logger);
            var skillRepo = new SkillRepo(AccountContextProvider, Logger, notificationRepo);
            var repo = new PilotRepo(AccountContextProvider, Logger, null);

            // stage 1
            var userData = new UserDataDto()
            {
                Pilots = new Pilot[]
                {
                    new Pilot()
                    {
                        Name = "Pilot1",
                        Skills = new List<Skill>(),
                        EveId = 1,
                        KeyInfoId =  10,
                    },
                    new Pilot()
                    {
                        EveId = 2,
                        Name = "Pilot2",
                        Skills = new List<Skill>(),
                        KeyInfoId =  11
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new List<Job>(),
            };

            await repo.Update(userid, userData);
            Assert.AreNotEqual(0, userData.Pilots[0].PilotId);

            await repo.SetFreeManufacturingJobsNofificationCount(userData.Pilots[0].PilotId, 10);
            await repo.SetFreeResearchJobsNofificationCount(userData.Pilots[1].PilotId, 20);

            var pilots = await repo.GetAll(userid);
            Assert.AreEqual(2, pilots.Count);

            var p1 = pilots.FirstOrDefault(x => x.PilotId == userData.Pilots[0].PilotId);
            var p2 = pilots.FirstOrDefault(x => x.PilotId == userData.Pilots[1].PilotId);

            Assert.IsNotNull(p1);
            Assert.IsNotNull(p2);
            Assert.AreEqual(10, p1.FreeManufacturingJobsNofificationCount);
            Assert.AreEqual(0, p1.FreeResearchJobsNofificationCount);
            Assert.AreEqual(20, p2.FreeResearchJobsNofificationCount);
            Assert.AreEqual(0, p2.FreeManufacturingJobsNofificationCount);
        }

        [TestMethod]
        public async Task PilotDeleteByKey()
        {
            var accrepo = AccountRepo;
            var skey = await accrepo.RegisterUser("PilotDeleteByKey@kot.pies", "123");
            var userid = await accrepo.CheckSession(skey);

            var notificationRepo = new NotificationRepo(AccountContextProvider, Logger);
            var skillRepo = new SkillRepo(AccountContextProvider, Logger, notificationRepo);
            var repo = new PilotRepo(AccountContextProvider, Logger, null);
            var keyRepo = new KeyInfoRepo(Logger, AccountContextProvider);

            long keyid1 = await keyRepo.AddKey(userid, 1, "");
            long keyid2 = await keyRepo.AddKey(userid, 2, "");

            // stage 1
            var userData = new UserDataDto()
            {
                Pilots = new Pilot[]
                {
                    new Pilot()
                    {
                        EveId = 1,
                        KeyInfoId =  keyid1,
                        Name = "Pilot1",
                        Skills = new List<Skill>()
                    },
                    new Pilot()
                    {
                        EveId = 2,
                        KeyInfoId =  keyid2,
                        Name = "Pilot2",
                        Skills = new List<Skill>()
                    },
                    new Pilot()
                    {
                        EveId = 3,
                        KeyInfoId = keyid2,
                        Name = "Pilot3",
                        Skills = new List<Skill>()
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new List<Job>(),
            };

            await repo.Update(userid, userData);

            var pilots = await repo.GetAll(userid);
            Assert.AreEqual(3, pilots.Count);

            await repo.DeleteByKey(keyid2);
            pilots = await repo.GetAll(userid);
            Assert.AreEqual(1, pilots.Count);
            Assert.AreEqual("Pilot1", pilots.First().Name);

            // delete using non existing key
            await repo.DeleteByKey(keyid1+keyid2+1);
            pilots = await repo.GetAll(userid);
            Assert.AreEqual(1, pilots.Count);
        }

        [TestMethod]
        public async Task CorporationDeleteByKey()
        {
            var accrepo = AccountRepo;
            var skey = await accrepo.RegisterUser("CorporationDeleteByKey@kot.pies", "123");
            var userid = await accrepo.CheckSession(skey);

            var repo = new CorporationRepo(AccountContextProvider, Logger, null);
            var keyRepo = new KeyInfoRepo(Logger, AccountContextProvider);
            var keyid1 = await keyRepo.AddKey(userid, 1, "");
            var keyid2 = await keyRepo.AddKey(userid, 2, "");
            var keyid3 = await keyRepo.AddKey(userid, 3, "");

            // stage 1
            var userData = new UserDataDto()
            {
                Pilots = new List<Pilot>(),
                Corporations = new Corporation[]
                {
                    new Corporation() {EveId = 1, Name = "Corpo1", KeyInfoId = keyid1},
                    new Corporation() {EveId = 2, Name = "Corpo2", KeyInfoId = keyid2},
                    new Corporation() {EveId = 3, Name = "Corpo3", KeyInfoId = keyid3}
                }.ToList(),
                Jobs = new List<Job>(),
            };

            await repo.Update(userid, userData);

            var corporations = await repo.GetAll(userid);
            Assert.AreEqual(3, corporations.Count);

            await repo.DeleteByKey(keyid2);
            corporations = await repo.GetAll(userid);
            Assert.AreEqual(2, corporations.Count);
            Assert.IsTrue(corporations.Any(x => x.Name=="Corpo1"));
            Assert.IsTrue(corporations.Any(x => x.Name == "Corpo3"));

            await repo.DeleteByKey(keyid3);
            corporations = await repo.GetAll(userid);
            Assert.AreEqual(1, corporations.Count);
            Assert.IsTrue(corporations.Any(x => x.Name == "Corpo1"));
        }


    }
}
