using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ts.api
{
    public interface ICacheLocalProvider
    {
        Task<T> Get<T>(string key, Func<Task<Tuple<DateTime, T>>> provider) where T : class;
    }
}
