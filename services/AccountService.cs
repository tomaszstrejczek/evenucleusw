using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ts.data;

namespace ts.services
{
    public class AccountService: IAccountService
    {
        private readonly IAccountRepo _accountRepo;
        public AccountService(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public async Task<string> Login(string email, string password)
        {
            return await _accountRepo.Login(email, password);
        }

        public async Task<string> Register(string email, string password)
        {
            return await _accountRepo.RegisterUser(email, password);
        }

        public async Task Logout(string skey)
        {
            await _accountRepo.Logout(skey);
        }
    }
}
