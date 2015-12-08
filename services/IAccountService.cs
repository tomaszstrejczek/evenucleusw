using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ts.services
{
    public interface IAccountService
    {
        Task<string> Login(string email, string password);
        Task<string> Register(string email, string password);
        Task Logout(string skey);
    }
}
