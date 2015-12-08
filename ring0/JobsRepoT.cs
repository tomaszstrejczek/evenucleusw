using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ts.data;
using ts.domain;

namespace ring0
{
    [TestClass]
    public class JobsRepoT: TestBase
    {
        [TestMethod]
        public async Task JobsNotification()
        {
            var accrepo = AccountRepo;
            var skey = await accrepo.RegisterUser("JobsRepoTJobsNotification@kot.pies", "123");
            var userid = await accrepo.CheckSession(skey);

            var notificationRepo = new NotificationRepo(AccountContextProvider, Logger);
            var skillRepo = new SkillRepo(AccountContextProvider, Logger, notificationRepo);
            var pilotsRepo = new PilotRepo(AccountContextProvider, Logger, null);
            var jobsRepo = new JobRepo(AccountContextProvider, Logger, notificationRepo, pilotsRepo);

            // stage 1
            var userData = new UserDataDto()
            {
                Pilots = new Pilot[]
                {
                    new Pilot()
                    {
                        EveId = 1,
                        KeyInfoId = 1,
                        Name = "Pilot1",
                        Skills = new List<Skill>(),
                        MaxManufacturingJobs = 1,
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new List<Job>(),
            };

            await pilotsRepo.Update(userid, userData);
            await jobsRepo.Update(userid, userData);

            var notifications = await notificationRepo.GetAll(userid);
            Assert.AreEqual(1, notifications.Count);
            var n = notifications.First();
            Assert.AreEqual("Pilot1", n.Message);
            Assert.AreEqual("1 free manufacturing slots", n.Message2);
            await notificationRepo.Remove(n.NotificationId);

            // stage 2
            // Same info provided and expected no further notifications
            await pilotsRepo.Update(userid, userData);
            await jobsRepo.Update(userid, userData);

            notifications = await notificationRepo.GetAll(userid);
            Assert.AreEqual(0, notifications.Count);
            var pilots = await pilotsRepo.GetAll(userid);
            Assert.AreEqual(1, pilots.Count);
            Assert.AreEqual(1, pilots.First().FreeManufacturingJobsNofificationCount);

            // stage 3
            // First job started - still no notification - but notification flag shoud be reset
            var userData2 = new UserDataDto()
            {
                Pilots = new Pilot[]
                {
                    new Pilot()
                    {
                        EveId = 1,
                        KeyInfoId = 1,
                        Name = "Pilot1",
                        Skills = new List<Skill>(),
                        MaxManufacturingJobs = 1,
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new Job[]
                {
                    new Job() {IsManufacturing = true, Owner="Pilot1"}
                }.ToList(),
            };
            await pilotsRepo.Update(userid, userData2);
            await jobsRepo.Update(userid, userData2);
            notifications = await notificationRepo.GetAll(userid);
            Assert.AreEqual(0, notifications.Count());
            pilots = await pilotsRepo.GetAll(userid);
            Assert.AreEqual(1, pilots.Count);
            Assert.AreEqual(0, pilots.First().FreeManufacturingJobsNofificationCount);

            // stage 4
            // No running jobs - notification expected
            await pilotsRepo.Update(userid, userData);
            await jobsRepo.Update(userid, userData);

            notifications = await notificationRepo.GetAll(userid);
            Assert.AreEqual(1, notifications.Count());
            n = notifications.First();
            Assert.AreEqual("Pilot1", n.Message);
            Assert.AreEqual("1 free manufacturing slots", n.Message2);
            await notificationRepo.Remove(n.NotificationId);
        }

        [TestMethod]
        public async Task ResearchJobsNotification()
        {
            var accrepo = AccountRepo;
            var skey = await accrepo.RegisterUser("JobsRepoTResearchJobsNotification@kot.pies", "123");
            var userid = await accrepo.CheckSession(skey);

            var notificationRepo = new NotificationRepo(AccountContextProvider, Logger);
            var skillRepo = new SkillRepo(AccountContextProvider, Logger, notificationRepo);
            var pilotsRepo = new PilotRepo(AccountContextProvider, Logger, null);
            var jobsRepo = new JobRepo(AccountContextProvider, Logger, notificationRepo, pilotsRepo);

            // stage 1
            var userData = new UserDataDto()
            {
                Pilots = new Pilot[]
                {
                    new Pilot()
                    {
                        EveId = 1,
                        KeyInfoId = 1,
                        Name = "Pilot1",
                        Skills = new List<Skill>(),
                        MaxResearchJobs = 1,
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new List<Job>(),
            };

            await pilotsRepo.Update(userid, userData);
            await jobsRepo.Update(userid, userData);

            var notifications = await notificationRepo.GetAll(userid);
            Assert.AreEqual(1, notifications.Count);
            var n = notifications.First();
            Assert.AreEqual("Pilot1", n.Message);
            Assert.AreEqual("1 free research slots", n.Message2);
            await notificationRepo.Remove(n.NotificationId);

            // stage 2
            // Same info provided and expected no further notifications
            await pilotsRepo.Update(userid, userData);
            await jobsRepo.Update(userid, userData);

            notifications = await notificationRepo.GetAll(userid);
            Assert.AreEqual(0, notifications.Count);
            var pilots = await pilotsRepo.GetAll(userid);
            Assert.AreEqual(1, pilots.Count);
            Assert.AreEqual(1, pilots.First().FreeResearchJobsNofificationCount);

            // stage 3
            // First job started - still no notification - but notification flag shoud be reset
            var userData2 = new UserDataDto()
            {
                Pilots = new Pilot[]
                {
                    new Pilot()
                    {
                        EveId = 1,
                        KeyInfoId = 1,
                        Name = "Pilot1",
                        Skills = new List<Skill>(),
                        MaxResearchJobs = 1,
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new Job[]
                {
                    new Job() {IsManufacturing = false, Owner="Pilot1"}
                }.ToList(),
            };

            await pilotsRepo.Update(userid, userData2);
            await jobsRepo.Update(userid, userData2);

            notifications = await notificationRepo.GetAll(userid);
            Assert.AreEqual(0, notifications.Count);
            pilots = await pilotsRepo.GetAll(userid);
            Assert.AreEqual(1, pilots.Count);
            Assert.AreEqual(0, pilots.First().FreeResearchJobsNofificationCount);

            // stage 4
            // No running jobs - notification expected
            await pilotsRepo.Update(userid, userData);
            await jobsRepo.Update(userid, userData);

            notifications = await notificationRepo.GetAll(userid);
            Assert.AreEqual(1, notifications.Count());
            n = notifications.First();
            Assert.AreEqual("Pilot1", n.Message);
            Assert.AreEqual("1 free research slots", n.Message2);
            await notificationRepo.Remove(n.NotificationId);
        }
    }
}
