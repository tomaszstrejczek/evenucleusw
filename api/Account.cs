using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Routing;

namespace ts.api
{
    public class Account: NancyModule
    {
        private readonly IAccountRepo _accountRepo;

        public Account(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
            Post["/api/account/login"] = Login;
            Post["/api/account/logout"] = Logout;
        }

        struct LoginModel
        {
            public string email;
            public string password;
        }

        private string Login(dynamic parameters)
        {
            var m = this.Bind<LoginModel>();

            if (m.email == "a@a.a")
                return "new token";

            throw new UserException(strings.InvalidUserPassword);
        }

        private string Logout(dynamic parameters)
        {
            return "logged out";
        }
    }

}
