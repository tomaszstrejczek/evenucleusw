using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ts.domain;

namespace ts.data
{
    public class AccountContextProvider : IAccountContextProvider
    {
        public AccountContext Context => new AccountContext();
    }
}
