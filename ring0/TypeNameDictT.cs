using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ts.api;

namespace ring0
{
    [TestClass]
    public class TypeNameDictT : TestBase
    {
        [TestMethod]
        public async Task SimpleCall()
        {
            var typeNameDict = new TypeNameDict(AccountContextProvider, EveCache, Logger);

            var r = await typeNameDict.GetById(new long[] { 12345 });
            Assert.AreEqual("200mm Railgun I Blueprint", r[0].Item2);

            r = await typeNameDict.GetById(new long[] { 12345, 34317 });
            Assert.AreEqual("200mm Railgun I Blueprint", r[0].Item2);
            Assert.AreEqual("Confessor", r[1].Item2);
        }
    }
}
