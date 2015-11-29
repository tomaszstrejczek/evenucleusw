using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ts.db;

namespace ts.api
{
    public interface IAccountContextProvider
    {
        AccountContext Context { get; }
    }
}
