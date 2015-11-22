using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Routing;

namespace api
{
    public class Account: NancyModule
    {
        public Account()
        {
            Post["/login"] = parameters => Login(parameters);
        }
        private string Login(dynamic parameters)
        {
            if (parameters.login == "a@a.a")
                return "new token";

            throw new Exception("Invalid user/password");
        }
    }

}
