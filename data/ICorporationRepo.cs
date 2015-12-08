using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ts.domain;

namespace ts.data
{
    public interface ICorporationRepo
    {
        Task Update(long userid, UserDataDto data);
        Task<ICollection<Corporation>> GetAll(long userid);
        Task SimpleUpdateFromKey(long userid, long keyid, string vcode);
        Task DeleteByKey(long id);
    }
}
