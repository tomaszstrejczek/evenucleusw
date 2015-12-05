using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ts.api;
using Serilog;

namespace ring0
{
    public class TestBase
    {
        public AccountContextProvider AccountContextProvider
        {
            get
            {
                var config = new MyTestConfiguration();
                var ctxprovider = new AccountContextProvider(config);

                return ctxprovider;
            }
        }

        public AccountRepo AccountRepo
        {
            get
            {
                var accountrepo = new AccountRepo(AccountContextProvider);

                return accountrepo;
            }
        }

        public static ILogger Logger
        {
            get
            {
                var log = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Trace().CreateLogger();
                return log;
            }
        }

    }
}
