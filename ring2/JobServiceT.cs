using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using ring0;
using ts.api;
using ts.db;


namespace ring2
{
    [TestClass]
    public class JobsLocalServiceT : TestBase
    {
        [TestMethod]
        public async Task SimpleList()
        {
            long code = 3645238;
            string vcode = "sLOD3pSHwuzKtml3inm59qvVWHiKA3rULJY7KRsuWmmHrZ0c8qAZlftLDQIHvxBq";

            var keyRepoMock = new Mock<IKeyInfoRepo>();
            keyRepoMock.Setup(x => x.GetKeys(1))
                .Returns(Task.FromResult((ICollection<KeyInfo>)(new KeyInfo[] { new KeyInfo() { KeyId = code, VCode = vcode, KeyInfoId = code + 1 } }.ToList())));

            var api = EveApi;
            var service = new JobsService(keyRepoMock.Object, api, Logger, TypeNameDict, CharacterNameDict);

            var result = await service.Get(1);
        }

        [TestMethod]
        public async Task SimpleListCorp()
        {
            int code = 4865846;
            String vcode = "t99aNY7KfAMRUxCz7S29ZkPvBEwVjrYwtWdgGVXFYKq0lHAWtIlpwuiVqxZpnwBT";

            var keyRepoMock = new Mock<IKeyInfoRepo>();
            keyRepoMock.Setup(x => x.GetKeys(1))
                .Returns(Task.FromResult((ICollection<KeyInfo>)(new KeyInfo[] { new KeyInfo() { KeyId = code, VCode = vcode, KeyInfoId = code + 1 } }.ToList())));

            var api = EveApi;
            var service = new JobsService(keyRepoMock.Object, api, Logger, TypeNameDict, CharacterNameDict);

            var result = await service.Get(1);
        }

        [TestMethod]
        public async Task MultipleKeys()
        {
            int code = 4865846;
            String vcode = "t99aNY7KfAMRUxCz7S29ZkPvBEwVjrYwtWdgGVXFYKq0lHAWtIlpwuiVqxZpnwBT";
            long code2 = 3645238;
            string vcode2 = "sLOD3pSHwuzKtml3inm59qvVWHiKA3rULJY7KRsuWmmHrZ0c8qAZlftLDQIHvxBq";
            long code3 = 3227994;
            string vcode3 = "MMIm45tf04n2xlhK0DQNO8FaL3CdYO0KOCnVobAajkwtJIvGXyvbI4DNf0BziWH8";

            var keyRepoMock = new Mock<IKeyInfoRepo>();
            keyRepoMock.Setup(x => x.GetKeys(1))
                .Returns(Task.FromResult((ICollection<KeyInfo>)(new KeyInfo[] { new KeyInfo() { KeyId = code, VCode = vcode, KeyInfoId = code + 1 }, new KeyInfo() { KeyId = code2, VCode = vcode2, KeyInfoId = code2 + 1 }, new KeyInfo() { KeyId = code3, VCode = vcode3, KeyInfoId = code3 + 1 } }.ToList())));

            var api = EveApi;
            var service = new JobsService(keyRepoMock.Object, api, Logger, TypeNameDict, CharacterNameDict);

            var result = await service.Get(1);
        }
    }
}
