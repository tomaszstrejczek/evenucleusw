using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ts.data;
using ts.domain;

namespace ring2
{
    [TestClass]
    public class AssemblyInit
    {
        [AssemblyInitialize]
        public static void Init(TestContext ctx)
        {
            Automapping.Init();
            AccountContext.UseInMemory = true;
        }
    }
}
