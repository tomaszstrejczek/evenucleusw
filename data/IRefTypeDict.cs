using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ts.data
{
    public interface IRefTypeDict
    {
        Task<string> GetById(long id);
    }
}
