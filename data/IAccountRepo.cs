using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ts.data
{
    public interface IAccountRepo
    {
        Task<string> RegisterUser(string email, string password);
        Task<string> Login(string email, string password);
        Task Logout(string skey);
        // returns userid
        Task<long> CheckSession(string skey);
    }
}
