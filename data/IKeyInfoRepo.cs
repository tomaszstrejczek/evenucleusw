using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ts.domain;

namespace ts.data
{
    public interface IKeyInfoRepo
    {
        Task<long> AddKey(long userid, long keyid, string vcode);
        Task DeleteKey(long keyinfoid);
        Task<ICollection<KeyInfo>> GetKeys(long userid);
        Task<KeyInfo> GetById(long keyinfoid);
    }
}
