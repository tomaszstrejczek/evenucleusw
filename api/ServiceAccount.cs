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

namespace ts.api
{
    public class ServiceAccount: NancyModule
    {
        private readonly IAccountRepo _accountRepo;

        public ServiceAccount(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
            Post["/api/account/login", runAsync:true] = async (parameters, ct) => await Login(parameters, ct);
            Post["/api/account/register", runAsync: true] = async (parameters, ct) => await Register(parameters, ct);
            Post["/api/account/logout", runAsync: true] = async (parameters, ct) => await Logout(parameters, ct);
        }

        struct LoginModel
        {
            public string email;
            public string password;
        }

        private async Task<string> Login(dynamic parameters, CancellationToken ct)
        {
            var m = this.Bind<LoginModel>();

            return await _accountRepo.Login(m.email, m.password);
        }

        private async Task<string> Register(dynamic parameters, CancellationToken ct)
        {
            var m = this.Bind<LoginModel>();

            return await _accountRepo.RegisterUser(m.email, m.password);
        }

        private async Task<string> Logout(dynamic parameters, CancellationToken ct)
        {
            var skeys = this.Context.Request.Headers["jwt"].ToList();
            if (skeys.Count != 1)
                throw new UserException(strings.InvalidSessionKey);

            await _accountRepo.Logout(skeys[0]);
            return "logged out";
        }
    }

}
