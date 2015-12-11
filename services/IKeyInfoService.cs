using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ts.dto;

namespace ts.services
{
    public interface IKeyInfoService
    {
        Task<long> Add(long userid, long keyid, string vcode);
        Task Delete(long keyinfoid);
        Task<List<KeyInfoDto>> Get(long userid);
    }
}
