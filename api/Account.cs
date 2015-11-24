using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Routing;

namespace api
{
    public class Account: NancyModule
    {
        public Account()
        {
            Post["/login"] = Login;
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
    }

}
