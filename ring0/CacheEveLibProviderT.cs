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
    public class CacheEveLibProviderT : TestBase
    {
        [TestMethod]
        public async Task WrongArgument()
        {
            var evecache = new EveSqlServerCache(Logger, AccountContextProvider);
            var provider = new CacheEveLibProvider(evecache);

            try
            {
                var result = await provider.Get<string>("ala", null);
                Assert.IsTrue(false, "Unreachable");
            }
            catch (ArgumentNullException)
            {
            }
        }

        [TestMethod]
        public async Task NormalFlow()
        {
            var evecache = new EveSqlServerCache(Logger, AccountContextProvider);
            var provider = new CacheEveLibProvider(evecache);

            var result = await provider.Get<string>("ala", () => Task.FromResult(new Tuple<DateTime, string>(DateTime.UtcNow.AddMilliseconds(10), "kot")));
            Assert.AreEqual("kot", result);

            await Task.Delay(TimeSpan.FromMilliseconds(50)).ConfigureAwait(false);
            var result2 = await provider.Get<string>("ala", () => Task.FromResult(new Tuple<DateTime, string>(DateTime.UtcNow.AddMilliseconds(500), "kot2")));

            Assert.AreEqual("kot2", result2);
        }
    }
}
