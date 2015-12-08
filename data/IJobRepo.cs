using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ts.domain;

namespace ts.data
{
    public interface IJobRepo
    {
        Task Update(long userid, UserDataDto data);
        Task<ICollection<Job>> GetAll(long userid);
    }
}
