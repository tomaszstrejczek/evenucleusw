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
    public class EveSqlServerCacheT : TestBase
    {
        [TestMethod]
        public void CacheNoKey()
        {
            var cache = new EveSqlServerCache(Logger, AccountContextProvider);

            DateTime dt;
            var result = cache.TryGetExpirationDate(new Uri("file://ala"), out dt);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task AddKey()
        {
            var cache = new EveSqlServerCache(Logger, AccountContextProvider);

            await cache.StoreAsync(new Uri("file://ala"), DateTime.UtcNow.AddDays(1), "ala ma kota");
            await cache.StoreAsync(new Uri("file://kot"), DateTime.UtcNow.AddDays(1), "kot ma kota");

            var result = await cache.LoadAsync(new Uri("file://ala"));
            Assert.AreEqual("ala ma kota", result);

            result = await cache.LoadAsync(new Uri("file://kot"));
            Assert.AreEqual("kot ma kota", result);
        }
    }
}
