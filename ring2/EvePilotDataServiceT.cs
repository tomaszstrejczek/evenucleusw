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
    public class EvePilotDataServiceT : TestBase
    {
        [TestMethod]
        public async Task SinglePilot()
        {
            long code = 3327094;
            string vcode = "yb6Yi3CzuAW248DqcFY9zEwThGFsMe8EEkcqLgfFd8d4smXvv8N7Fv1I6Ei37rc5";

            var keyRepoMock = new Mock<IKeyInfoRepo>();
            keyRepoMock.Setup(x => x.GetKeys(1))
                .Returns(Task.FromResult( (ICollection<KeyInfo>) (new KeyInfo[] {new KeyInfo() {KeyId = code, VCode = vcode, KeyInfoId = code+1}}.ToList())));

            var api = new EveApi(Logger, EveCache);
            var pilotLocalService = new EvePilotDataService(keyRepoMock.Object, api, Logger, TypeNameDict);

            var result = await pilotLocalService.Get(1);
            Assert.AreEqual(1, result.Item1.Count);
            //Assert.IsTrue((result.Item1[0].CurrentTrainingEnd - DateTime.UtcNow).TotalMilliseconds >= 0);
            //Assert.IsTrue((result.Item1[0].TrainingQueueEnd-DateTime.UtcNow).TotalMilliseconds >= 0);
            Assert.AreEqual("Tomek 3", result.Item1[0].Name);
            Assert.AreEqual(1, result.Item1[0].MaxManufacturingJobs);
            Assert.AreEqual(1, result.Item1[0].MaxResearchJobs);

            Assert.AreEqual(code+1, result.Item1[0].KeyInfoId);
            Assert.AreEqual(0, result.Item2.Count);
        }

        [TestMethod]
        public async Task SinglePilot2()
        {
            long code = 3483492;
            string vcode = "ZwML01eU6aQUVIEC7gedCEaySiNxRTJxgWo2qoVnxd5duN4tt4CWgMuYMSVNWIUG";

            var keyRepoMock = new Mock<IKeyInfoRepo>();
            keyRepoMock.Setup(x => x.GetKeys(1))
                .Returns(Task.FromResult((ICollection<KeyInfo>)(new KeyInfo[] { new KeyInfo() { KeyId = code, VCode = vcode, KeyInfoId = code+1} }.ToList())));

            var api = new EveApi(Logger, EveCache);
            var pilotLocalService = new EvePilotDataService(keyRepoMock.Object, api, Logger, TypeNameDict);

            var result = await pilotLocalService.Get(1);
            Assert.AreEqual(1, result.Item1.Count);
            //Assert.IsFalse(result.Item1[0].CloneNotUpToDate);
            Assert.IsTrue((result.Item1[0].CurrentTrainingEnd - DateTime.UtcNow).TotalMilliseconds >= 0);
            Assert.IsTrue((result.Item1[0].TrainingQueueEnd - DateTime.UtcNow).TotalMilliseconds >= 0);
            Assert.AreEqual("MicioGatto", result.Item1[0].Name);
            Assert.AreEqual(10, result.Item1[0].MaxManufacturingJobs);
            Assert.AreEqual(10, result.Item1[0].MaxResearchJobs);

            Assert.AreEqual(code+1, result.Item1[0].KeyInfoId);
            Assert.AreEqual(0, result.Item2.Count);
        }

        [TestMethod]
        public async Task MultiplePilots()
        {
            long code = 3483492;
            string vcode = "ZwML01eU6aQUVIEC7gedCEaySiNxRTJxgWo2qoVnxd5duN4tt4CWgMuYMSVNWIUG";
            long code2 = 3192744;
            string vcode2 = "OOYpSnhWTsgJpRVo475E67q9XSeHx42KgDy13DYAogFptaA8uidOrhbrCLfy31MC";

            var keyRepoMock = new Mock<IKeyInfoRepo>();
            keyRepoMock.Setup(x => x.GetKeys(1))
                .Returns(Task.FromResult((ICollection<KeyInfo>)(new KeyInfo[] { new KeyInfo() { KeyId = code, VCode = vcode }, new KeyInfo() { KeyId = code2, VCode = vcode2 } }.ToList())));

            var api = new EveApi(Logger, EveCache);
            var pilotLocalService = new EvePilotDataService(keyRepoMock.Object, api, Logger, TypeNameDict);

            var result = await pilotLocalService.Get(1);
            Assert.AreEqual(3, result.Item1.Count);
            Assert.AreEqual(3, result.Item1.Select(x => x.Name).Distinct().Count());

            Assert.AreEqual(0, result.Item2.Count);
        }

        [TestMethod]
        public async Task CreationMask()
        {
            long code = 3785350;
            string vcode = "Nm1mfKsbFukmkMgCoMsk75QzVwVVjRKLpS4o3Zbc4cFgY11TBa9i25cUDAljB2d4";

            var keyRepoMock = new Mock<IKeyInfoRepo>();
            keyRepoMock.Setup(x => x.GetKeys(1))
                .Returns(Task.FromResult((ICollection<KeyInfo>)(new KeyInfo[] { new KeyInfo() { KeyId = code, VCode = vcode, KeyInfoId=code+1 } }.ToList())));

            var api = new EveApi(Logger, EveCache);
            var pilotLocalService = new EvePilotDataService(keyRepoMock.Object, api, Logger, TypeNameDict);

            var result = await pilotLocalService.Get(1);
            Assert.AreEqual(3, result.Item1.Count);
            Assert.AreEqual(3, result.Item1.Select(x => x.Name).Distinct().Count());

            Assert.AreEqual(code+1, result.Item1[0].KeyInfoId);
            Assert.AreEqual(code+1, result.Item1[1].KeyInfoId);
            Assert.AreEqual(code+1, result.Item1[2].KeyInfoId);
        }

        [TestMethod]
        public async Task CorporateKey()
        {
            int code = 4865846;
            String vcode = "t99aNY7KfAMRUxCz7S29ZkPvBEwVjrYwtWdgGVXFYKq0lHAWtIlpwuiVqxZpnwBT";

            var keyRepoMock = new Mock<IKeyInfoRepo>();
            keyRepoMock.Setup(x => x.GetKeys(1))
                .Returns(Task.FromResult((ICollection<KeyInfo>)(new KeyInfo[] { new KeyInfo() { KeyId = code, VCode = vcode, KeyInfoId = code+1 } }.ToList())));

            var api = new EveApi(Logger, EveCache);
            var pilotLocalService = new EvePilotDataService(keyRepoMock.Object, api, Logger, TypeNameDict);

            var result = await pilotLocalService.Get(1);
            Assert.AreEqual(0, result.Item1.Count);

            Assert.AreEqual(1, result.Item2.Count);
            Assert.AreEqual("My Random Corporation", result.Item2[0].Name);
            Assert.AreEqual(code+1, result.Item2[0].KeyInfoId);
        }
    }
}
