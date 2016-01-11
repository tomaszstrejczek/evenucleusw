using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ts.data
{
    public interface ITypeNameDict
    {
        Task<List<Tuple<long, string>>> GetById(IEnumerable<long> ids);
        Task<List<Tuple<long, string>>> GetByName(IEnumerable<string> name);
    }
}
