﻿using System;
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
using ts.services;

namespace ts.api
{
    public class ServiceAccount: NancyModule
    {
        private readonly IAccountService _accountService;

        public ServiceAccount(IAccountService accountService)
        {
            _accountService = accountService;
            Post["/account/login", runAsync:true] = async (parameters, ct) => await Login();
            Post["/account/register", runAsync: true] = async (parameters, ct) => await Register();
            Post["/account/logout", runAsync: true] = async (parameters, ct) => await Logout();
        }

        struct LoginModel
        {
            public string email { get; set; }
            public string password { get; set; }
        }

        private async Task<string> Login()
        {
            var m = this.Bind<LoginModel>();

            return await _accountService.Login(m.email, m.password);
        }

        private async Task<string> Register()
        {
            var m = this.Bind<LoginModel>();

            return await _accountService.Register(m.email, m.password);
        }

        private async Task<string> Logout()
        {
            var skeys = this.Context.Request.Headers["jwt"].ToList();
            if (skeys.Count != 1)
                throw new UserException(strings.InvalidSessionKey);

            await _accountService.Logout(skeys[0]);
            return "logged out";
        }
    }

}
