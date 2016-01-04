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
    public class SkillInQueueRepoT : TestBase
    {
        [TestMethod]
        public async Task SkillInQueueOperations()
        {
            var accrepo = AccountRepo;
            var skey = await accrepo.RegisterUser("SkillsInQueueAdded@kot.pies", "123");
            var userid = await accrepo.CheckSession(skey);

            var skillInQueueRepo = new SkillInQueueRepo(AccountContextProvider, Logger);
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
                        Skills = new List<Skill>(),
                        SkillsInQueue =
                            new SkillInQueue[]
                            {
                                new SkillInQueue() {SkillName = "skilla", Level = 1, Order = 1},
                                new SkillInQueue() {SkillName = "skillb", Level = 2, Order = 2},
                                new SkillInQueue() {SkillName = "skillc", Level = 3, Order = 3}
                            }.ToList(),
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new List<Job>(),
            };

            await pilotRepo.Update(userid, userData);
            await skillInQueueRepo.Update(userid, userData);

            var skills = await skillInQueueRepo.GetForPilot(userData.Pilots[0].PilotId);
            Assert.AreEqual(3, skills.Count);
            foreach(var s in skills)
                Assert.IsTrue(s.Order > 0);

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
                        SkillsInQueue =
                            new SkillInQueue[]
                            {
                                new SkillInQueue() {SkillName = "skilld", Level = 1},
                            }.ToList(),
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new List<Job>(),
            };

            await pilotRepo.Update(userid, userData2);
            await skillInQueueRepo.Update(userid, userData2);

            skills = await skillInQueueRepo.GetForPilot(userData.Pilots[0].PilotId);
            Assert.AreEqual(1, skills.Count);
            Assert.AreEqual("skilld", skills[0].SkillName);
        }

        [TestMethod]
        public async Task MultipleLevels()
        {
            var accrepo = AccountRepo;
            var skey = await accrepo.RegisterUser("SkillsInQueueMultipleLevels@kot.pies", "123");
            var userid = await accrepo.CheckSession(skey);

            var skillInQueueRepo = new SkillInQueueRepo(AccountContextProvider, Logger);
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
                        Skills = new List<Skill>(),
                        SkillsInQueue =
                            new SkillInQueue[]
                            {
                                new SkillInQueue() {SkillName = "skilla", Level = 1},
                                new SkillInQueue() {SkillName = "skilla", Level = 2},
                                new SkillInQueue() {SkillName = "skilla", Level = 3}
                            }.ToList(),
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new List<Job>(),
            };

            await pilotRepo.Update(userid, userData);
            await skillInQueueRepo.Update(userid, userData);

            var skills = await skillInQueueRepo.GetForPilot(userData.Pilots[0].PilotId);
            Assert.AreEqual(3, skills.Count);

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
                        SkillsInQueue =
                            new SkillInQueue[]
                            {
                                new SkillInQueue() {SkillName = "skillb", Level = 1},
                            }.ToList(),
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new List<Job>(),
            };

            await pilotRepo.Update(userid, userData2);
            await skillInQueueRepo.Update(userid, userData2);

            skills = await skillInQueueRepo.GetForPilot(userData.Pilots[0].PilotId);
            Assert.AreEqual(1, skills.Count);
            Assert.AreEqual("skillb", skills[0].SkillName);
        }
        [TestMethod]
        public async Task MultipleLevels2()
        {
            var accrepo = AccountRepo;
            var skey = await accrepo.RegisterUser("SkillsInQueueMultipleLevels2@kot.pies", "123");
            var userid = await accrepo.CheckSession(skey);

            var skillInQueueRepo = new SkillInQueueRepo(AccountContextProvider, Logger);
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
                        Skills = new List<Skill>(),
                        SkillsInQueue =
                            new SkillInQueue[]
                            {
                                new SkillInQueue() {SkillName = "skilla", Level = 1},
                                new SkillInQueue() {SkillName = "skilla", Level = 2},
                                new SkillInQueue() {SkillName = "skilla", Level = 3}
                            }.ToList(),
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new List<Job>(),
            };

            await pilotRepo.Update(userid, userData);
            await skillInQueueRepo.Update(userid, userData);

            var skills = await skillInQueueRepo.GetForPilot(userData.Pilots[0].PilotId);
            Assert.AreEqual(3, skills.Count);

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
                        SkillsInQueue =
                            new SkillInQueue[]
                            {
                                new SkillInQueue() {SkillName = "skilla", Level = 3},
                            }.ToList(),
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new List<Job>(),
            };

            await pilotRepo.Update(userid, userData2);
            await skillInQueueRepo.Update(userid, userData2);

            skills = await skillInQueueRepo.GetForPilot(userData.Pilots[0].PilotId);
            Assert.AreEqual(1, skills.Count);
            Assert.AreEqual("skilla", skills[0].SkillName);
        }

        [TestMethod]
        public async Task MultipleLevels3()
        {
            var accrepo = AccountRepo;
            var skey = await accrepo.RegisterUser("SkillsInQueueMultipleLevels3@kot.pies", "123");
            var userid = await accrepo.CheckSession(skey);

            var skillInQueueRepo = new SkillInQueueRepo(AccountContextProvider, Logger);
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
                        Skills = new List<Skill>(),
                        SkillsInQueue =
                            new SkillInQueue[]
                            {
                                new SkillInQueue() {SkillName = "skilla", Level = 1},
                                new SkillInQueue() {SkillName = "skilla", Level = 2},
                                new SkillInQueue() {SkillName = "skilla", Level = 3}
                            }.ToList(),
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new List<Job>(),
            };

            await pilotRepo.Update(userid, userData);
            await skillInQueueRepo.Update(userid, userData);

            var skills = await skillInQueueRepo.GetForPilot(userData.Pilots[0].PilotId);
            Assert.AreEqual(3, skills.Count);

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
                        SkillsInQueue =
                            new SkillInQueue[]
                            {
                                new SkillInQueue() {SkillName = "skilla", Level = 1},
                                new SkillInQueue() {SkillName = "skilla", Level = 2},
                                new SkillInQueue() {SkillName = "skilla", Level = 3},
                                new SkillInQueue() {SkillName = "skilla", Level = 4}
                            }.ToList(),
                    }
                }.ToList(),
                Corporations = new List<Corporation>(),
                Jobs = new List<Job>(),
            };

            await pilotRepo.Update(userid, userData2);
            await skillInQueueRepo.Update(userid, userData2);

            skills = await skillInQueueRepo.GetForPilot(userData.Pilots[0].PilotId);
            Assert.AreEqual(4, skills.Count);
        }
    }
}
