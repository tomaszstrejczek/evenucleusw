using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ts.data;
using ts.domain;

namespace ring0
{
    [TestClass]
    public class SkillRepoT : TestBase
    {
        [TestMethod]
        public async Task SkillsNotificationAdded()
        {
            var accrepo = AccountRepo;
            var skey = await accrepo.RegisterUser("SkillRepoTSkillsNotificationAdded@kot.pies", "123");
            var userid = await accrepo.CheckSession(skey);

            var notificationRepo = new NotificationRepo(AccountContextProvider, Logger);
            var skillRepo = new SkillRepo(AccountContextProvider, Logger, notificationRepo);
            var pilotRepo = new PilotRepo(AccountContextProvider, Logger, null);

            // stage 1 - skills firstly seen - no notification expected
            var userData = new UserDataDto()
            {
                Pilots = new Pilot[]
                {
                    new Pilot()
                    {
                        EveId = 1,
                        KeyInfoId = 1,
                        Name = "Pilot1",
                        Skills = new Skill[] { new Skill() {SkillName = "skilla", Level=1}, new Skill() {SkillName = "skillb", Level = 2}}.ToList(),
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new List<Job>(),
            };

            await pilotRepo.Update(userid, userData);
            await skillRepo.Update(userid, userData);

            var skills = await skillRepo.GetForPilot(userData.Pilots[0].PilotId);
            Assert.AreEqual(2, skills.Count);

            var notifications = await notificationRepo.GetAll(userid);
            Assert.AreEqual(0, notifications.Count);

            await pilotRepo.Update(userid, userData);
            await skillRepo.Update(userid, userData);

            skills = await skillRepo.GetForPilot(userData.Pilots[0].PilotId);
            Assert.AreEqual(2, skills.Count);

            notifications = await notificationRepo.GetAll(userid);
            Assert.AreEqual(0, notifications.Count);

            // stage 3 - added a skill
            var userData2 = new UserDataDto()
            {
                Pilots = new Pilot[]
                {
                    new Pilot()
                    {
                        EveId = 1,
                        KeyInfoId = 1,
                        Name = "Pilot1",
                        Skills = new Skill[] { new Skill() {SkillName = "skilla", Level=1}, new Skill() {SkillName = "skillb", Level = 2}, new Skill() {SkillName = "skillc", Level = 3}}.ToList(),
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new List<Job>(),
            };

            await pilotRepo.Update(userid, userData2);
            await skillRepo.Update(userid, userData2);

            skills = await skillRepo.GetForPilot(userData.Pilots[0].PilotId);
            Assert.AreEqual(3, skills.Count);

            notifications = await notificationRepo.GetAll(userid);
            Assert.AreEqual(1, notifications.Count);
            var n = notifications.First();
            Assert.AreEqual("Pilot1", n.Message);
            Assert.AreEqual("skillc 3 trained", n.Message2);
            await notificationRepo.Remove(n.NotificationId);

            // stage 4 - added two skills
            var userData3 = new UserDataDto()
            {
                Pilots = new Pilot[]
                {
                    new Pilot()
                    {
                        EveId = 1,
                        KeyInfoId = 1,
                        Name = "Pilot1",
                        Skills = new Skill[] { new Skill() {SkillName = "skilla", Level=1}, new Skill() {SkillName = "skillb", Level = 2}, new Skill() {SkillName = "skillc", Level = 3}, new Skill() {SkillName = "skilld", Level = 4}, new Skill() {SkillName = "skille", Level = 5}}.ToList(),
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new List<Job>(),
            };

            await pilotRepo.Update(userid, userData3);
            await skillRepo.Update(userid, userData3);

            skills = await skillRepo.GetForPilot(userData.Pilots[0].PilotId);
            Assert.AreEqual(5, skills.Count);

            notifications = await notificationRepo.GetAll(userid);
            Assert.AreEqual(2, notifications.Count);
            n = notifications.First(x => x.Message2.Contains("skilld 4"));
            Assert.AreEqual("Pilot1", n.Message);
            Assert.AreEqual("skilld 4 trained", n.Message2);
            await notificationRepo.Remove(n.NotificationId);

            n = notifications.First(x => x.Message2.Contains("skille 5"));
            Assert.AreEqual("Pilot1", n.Message);
            Assert.AreEqual("skille 5 trained", n.Message2);
            await notificationRepo.Remove(n.NotificationId);
        }

        [TestMethod]
        public async Task SkillsNotificationRemoved()
        {
            var accrepo = AccountRepo;
            var skey = await accrepo.RegisterUser("SkillRepoTSkillsNotificationRemoved@kot.pies", "123");
            var userid = await accrepo.CheckSession(skey);

            var notificationRepo = new NotificationRepo(AccountContextProvider, Logger);
            var skillRepo = new SkillRepo(AccountContextProvider, Logger, notificationRepo);
            var pilotRepo = new PilotRepo(AccountContextProvider, Logger, null);

            // stage 1 - skills firstly seen - no notification expected
            var userData = new UserDataDto()
            {
                Pilots = new Pilot[]
                {
                    new Pilot()
                    {
                        EveId = 1,
                        KeyInfoId = 1,
                        Name = "Pilot1",
                        Skills = new Skill[] { new Skill() {SkillName = "skilla", Level=1}, new Skill() {SkillName = "skillb", Level = 2}, new Skill() {SkillName = "skillc", Level = 3}}.ToList(),
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new List<Job>(),
            };

            await pilotRepo.Update(userid, userData);
            await skillRepo.Update(userid, userData);

            var skills = await skillRepo.GetForPilot(userData.Pilots[0].PilotId);
            Assert.AreEqual(3, skills.Count);

            var notifications = await notificationRepo.GetAll(userid);
            Assert.AreEqual(0, notifications.Count());

            // stage 2 - removed a skill
            var userData2 = new UserDataDto()
            {
                Pilots = new Pilot[]
                {
                    new Pilot()
                    {
                        EveId = 1,
                        KeyInfoId = 1,
                        Name = "Pilot1",
                        Skills = new Skill[] { new Skill() {SkillName = "skilla", Level=1}, new Skill() {SkillName = "skillb", Level = 2}}.ToList(),
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new List<Job>(),
            };

            await pilotRepo.Update(userid, userData2);
            await skillRepo.Update(userid, userData2);

            skills = await skillRepo.GetForPilot(userData.Pilots[0].PilotId);
            Assert.AreEqual(2, skills.Count);

            notifications = await notificationRepo.GetAll(userid);
            Assert.AreEqual(1, notifications.Count());
            var n = notifications.First();
            Assert.AreEqual("Pilot1", n.Message);
            Assert.AreEqual("skillc 3 removed", n.Message2);
            await notificationRepo.Remove(n.NotificationId);

            // stage 3 - removed two skills
            var userData3 = new UserDataDto()
            {
                Pilots = new Pilot[]
                {
                    new Pilot()
                    {
                        EveId = 1,
                        KeyInfoId = 1,
                        Name = "Pilot1",
                        Skills = new List<Skill>(),
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new List<Job>(),
            };

            await pilotRepo.Update(userid, userData3);
            await skillRepo.Update(userid, userData3);

            skills = await skillRepo.GetForPilot(userData.Pilots[0].PilotId);
            Assert.AreEqual(0, skills.Count);

            notifications = await notificationRepo.GetAll(userid);
            Assert.AreEqual(2, notifications.Count());
            n = notifications.First(x => x.Message2.Contains("skilla 1"));
            Assert.AreEqual("Pilot1", n.Message);
            Assert.AreEqual("skilla 1 removed", n.Message2);
            await notificationRepo.Remove(n.NotificationId);

            n = notifications.First(x => x.Message2.Contains("skillb 2"));
            Assert.AreEqual("Pilot1", n.Message);
            Assert.AreEqual("skillb 2 removed", n.Message2);
            await notificationRepo.Remove(n.NotificationId);
        }

        [TestMethod]
        public async Task SkillsNotificationAddedRemoved()
        {
            var accrepo = AccountRepo;
            var skey = await accrepo.RegisterUser("SkillRepoTSkillsNotificationAddedRemoved@kot.pies", "123");
            var userid = await accrepo.CheckSession(skey);

            var notificationRepo = new NotificationRepo(AccountContextProvider, Logger);
            var skillRepo = new SkillRepo(AccountContextProvider, Logger, notificationRepo);
            var pilotRepo = new PilotRepo(AccountContextProvider, Logger, null);

            // stage 1 - skills firstly seen - no notification expected
            var userData = new UserDataDto()
            {
                Pilots = new Pilot[]
                {
                    new Pilot()
                    {
                        EveId = 1,
                        KeyInfoId = 1,
                        Name = "Pilot1",
                        Skills = new Skill[] { new Skill() {SkillName = "skilla", Level=1}, new Skill() {SkillName = "skillb", Level = 2}, new Skill() {SkillName = "skillc", Level = 3}}.ToList(),
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new List<Job>(),
            };

            await pilotRepo.Update(userid, userData);
            await skillRepo.Update(userid, userData);

            var skills = await skillRepo.GetForPilot(userData.Pilots[0].PilotId);
            Assert.AreEqual(3, skills.Count);

            var notifications = await notificationRepo.GetAll(userid);
            Assert.AreEqual(0, notifications.Count());

            // stage 2 - added and removed a skill
            var userData2 = new UserDataDto()
            {
                Pilots = new Pilot[]
                {
                    new Pilot()
                    {
                        EveId = 1,
                        KeyInfoId = 1,
                        Name = "Pilot1",
                        Skills = new Skill[] { new Skill() {SkillName = "skilla", Level=1}, new Skill() {SkillName = "skillb", Level = 2}, new Skill() {SkillName = "skillx", Level = 4}}.ToList(),
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new List<Job>(),
            };

            await pilotRepo.Update(userid, userData2);
            await skillRepo.Update(userid, userData2);

            skills = await skillRepo.GetForPilot(userData.Pilots[0].PilotId);
            Assert.AreEqual(3, skills.Count);

            notifications = await notificationRepo.GetAll(userid);
            Assert.AreEqual(2, notifications.Count());
            var n = notifications.First(x => x.Message2.Contains("skillc 3"));
            Assert.AreEqual("Pilot1", n.Message);
            Assert.AreEqual("skillc 3 removed", n.Message2);
            await notificationRepo.Remove(n.NotificationId);

            n = notifications.First(x => x.Message2.Contains("skillx 4"));
            Assert.AreEqual("Pilot1", n.Message);
            Assert.AreEqual("skillx 4 trained", n.Message2);
            await notificationRepo.Remove(n.NotificationId);
        }

        [TestMethod]
        public async Task SkillsNotificationLevelUp()
        {
            var accrepo = AccountRepo;
            var skey = await accrepo.RegisterUser("SkillRepoTSkillsNotificationLevelUp@kot.pies", "123");
            var userid = await accrepo.CheckSession(skey);

            var notificationRepo = new NotificationRepo(AccountContextProvider, Logger);
            var skillRepo = new SkillRepo(AccountContextProvider, Logger, notificationRepo);
            var pilotRepo = new PilotRepo(AccountContextProvider, Logger, null);

            // stage 1 - skills firstly seen - no notification expected
            var userData = new UserDataDto()
            {
                Pilots = new Pilot[]
                {
                    new Pilot()
                    {
                        EveId = 1,
                        KeyInfoId = 1,
                        Name = "Pilot1",
                        Skills = new Skill[] { new Skill() {SkillName = "skilla", Level=1}, new Skill() {SkillName = "skillb", Level = 2}, new Skill() {SkillName = "skill ", Level = 3}}.ToList(),
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new List<Job>(),
            };

            await pilotRepo.Update(userid, userData);
            await skillRepo.Update(userid, userData);

            var skills = await skillRepo.GetForPilot(userData.Pilots[0].PilotId);
            Assert.AreEqual(3, skills.Count);

            var notifications = await notificationRepo.GetAll(userid);
            Assert.AreEqual(0, notifications.Count());

            // stage 2 - level up 
            var userData2 = new UserDataDto()
            {
                Pilots = new Pilot[]
                {
                    new Pilot()
                    {
                        EveId = 1,
                        KeyInfoId = 1,
                        Name = "Pilot1",
                        Skills = new Skill[] { new Skill() {SkillName = "skilla", Level=1}, new Skill() {SkillName = "skillb", Level = 2}, new Skill() {SkillName = "skill ", Level = 4}}.ToList(),
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new List<Job>(),
            };

            await pilotRepo.Update(userid, userData2);
            await skillRepo.Update(userid, userData2);

            skills = await skillRepo.GetForPilot(userData.Pilots[0].PilotId);
            Assert.AreEqual(3, skills.Count);

            notifications = await notificationRepo.GetAll(userid);

            Assert.AreEqual(1, notifications.Count);
            var n = notifications.First(x => x.Message2.Contains("skill  4"));
            Assert.AreEqual("Pilot1", n.Message);
            Assert.AreEqual("skill  4 trained", n.Message2);
            await notificationRepo.Remove(n.NotificationId);
        }

        [TestMethod]
        public async Task DeleteKey()
        {
            var accrepo = AccountRepo;
            var skey = await accrepo.RegisterUser("SkillRepoTDeleteKey@kot.pies", "123");
            var userid = await accrepo.CheckSession(skey);

            var notificationRepo = new NotificationRepo(AccountContextProvider, Logger);
            var skillRepo = new SkillRepo(AccountContextProvider, Logger, notificationRepo);
            var pilotRepo = new PilotRepo(AccountContextProvider, Logger, null);
            var keyRepo = new KeyInfoRepo(Logger, AccountContextProvider);

            long keyid1 = await keyRepo.AddKey(userid, 1, "");
            long keyid2 = await keyRepo.AddKey(userid, 2, "");

            // stage 1 - skills firstly seen - no notification expected
            var userData = new UserDataDto()
            {
                Pilots = new Pilot[]
                {
                    new Pilot()
                    {
                        EveId = 1,
                        KeyInfoId = keyid1,
                        Name = "Pilot1",
                        Skills = new Skill[] { new Skill() {SkillName = "skill", Level=1}, new Skill() {SkillName = "skill", Level = 2}}.ToList(),
                    },
                    new Pilot()
                    {
                        EveId = 2,
                        KeyInfoId = keyid2,
                        Name = "Pilot2",
                        Skills = new Skill[] { new Skill() {SkillName = "skill", Level=1}, new Skill() {SkillName = "skill", Level = 2}}.ToList(),
                    },
                    new Pilot()
                    {
                        EveId = 3,
                        KeyInfoId = keyid2,
                        Name = "Pilot3",
                        Skills = new Skill[] { new Skill() {SkillName = "skilla", Level=1}, new Skill() {SkillName = "skilla", Level = 2}}.ToList(),
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new List<Job>(),
            };

            await pilotRepo.Update(userid, userData);
            await skillRepo.Update(userid, userData);

            var skills = await skillRepo.GetForPilot(userData.Pilots[0].PilotId);
            Assert.AreEqual(2, skills.Count);

            await pilotRepo.DeleteByKey(keyid2);
            skills = await skillRepo.GetForPilot(userData.Pilots[0].PilotId);
            Assert.AreEqual(2, skills.Count);

            using (var ctx = AccountContextProvider.Context)
            {
                var c = ctx.Skills.Count(p => p.PilotId == userData.Pilots[1].PilotId);
                Assert.AreEqual(0, c);
                c = ctx.Skills.Count(p => p.PilotId == userData.Pilots[2].PilotId);
                Assert.AreEqual(0, c);
            }
        }
    }
}
