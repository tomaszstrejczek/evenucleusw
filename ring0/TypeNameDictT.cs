using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkIt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ts.data;

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

        [TestMethod]
        public async Task Performance()
        {
            var typeNameDict = new TypeNameDict(AccountContextProvider, EveCache, Logger);

            var ids = new List<long>();
            var ids2 = new List<long>();
            var dict = new Dictionary<long, string>();
            for (long i = 0; i < 400000; ++i)
            {
                ids.Add(i);
                ids2.Add(369547l);
                dict.Add(i, i.ToString());
            }

            var z = BenchmarkIt.Benchmark.This("multiple call", async () =>
            {
                    await typeNameDict.GetById(ids);
            }).Against.This("multiple call", async () =>
            {
                await typeNameDict.GetById(ids);
            }).Against.This("call last", async () =>
            {
                await typeNameDict.GetById(ids2);
            })
            .Against.This("dict call", () =>
            {
                ids.Select(x => new Tuple<long, string>(x, dict[x])).ToList();
            })
            .Against.This("dict call2", () =>
            {
                ids2.Select(x => new Tuple<long, string>(x, dict[x])).ToList();
            })
            .For(1).Iterations();

            z.PrintComparison();
        }
    }
}
