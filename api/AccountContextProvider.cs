using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ts.db;
using ts.shared;

namespace ts.api
{
    public class AccountContextProvider : IAccountContextProvider
    {
        private readonly IMyConfiguration _configuration;

        public AccountContextProvider(IMyConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AccountContext Context => new AccountContext(_configuration);
    }
}
