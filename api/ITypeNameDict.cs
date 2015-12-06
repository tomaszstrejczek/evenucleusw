using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ts.api
{
    public interface ITypeNameDict
    {
        Task<List<Tuple<long, string>>> GetById(IEnumerable<long> ids);
    }
}
