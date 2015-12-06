using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ts.api
{
    public interface IRefTypeDict
    {
        Task<string> GetById(long id);
    }
}
