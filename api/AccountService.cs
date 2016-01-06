using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Routing;
using ts.domain;
using ts.dto;
using ts.services;

namespace ts.api
{
    public class AccountService: NancyModule
    {
        private readonly IAccountService _accountService;

        public AccountService(IAccountService accountService)
        {
            _accountService = accountService;
            Post["/api/account/login", runAsync:true] = async (parameters, ct) => await Login();
            Post["/api/account/register", runAsync: true] = async (parameters, ct) => await Register();
            Post["/api/account/logout", runAsync: true] = async (parameters, ct) => await Logout();
        }

        struct LoginModel
        {
            public string email { get; set; }
            public string password { get; set; }
        }

        private async Task<SingleStringDto> Login()
        {
            var m = this.Bind<LoginModel>();

            return new SingleStringDto() {Value = await _accountService.Login(m.email, m.password)};
        }

        private async Task<SingleStringDto> Register()
        {
            var m = this.Bind<LoginModel>();

            return new SingleStringDto() { Value = await _accountService.Register(m.email, m.password)};
        }

        private async Task<SingleStringDto> Logout()
        {
            var skeys = this.Context.Request.Headers["jwt"].ToList();
            if (skeys.Count != 1)
                throw new UserException(strings.InvalidSessionKey);

            await _accountService.Logout(skeys[0]);
            return new SingleStringDto() {Value = "logged out"};
        }
    }

}
