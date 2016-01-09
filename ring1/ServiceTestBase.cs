using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy.Responses.Negotiation;
using Nancy.Testing;
using ts.dto;

namespace ring1
{
    public class ServiceTestBase
    {
        public TestingBootstrapper Bootstrapper => new TestingBootstrapper();
        public Browser Browser => new Browser(Bootstrapper);

        public string GetSKey()
        {
            var provider = new RNGCryptoServiceProvider();
            byte[] skey = new byte[32];
            provider.GetBytes(skey);
            var email = Convert.ToBase64String(skey) + "@ala.kot";
            var password = "123";

            var result = Browser.Post("http://localhost:8070/api/account/register", with => {
                with.HttpRequest();
                with.FormValue("email", email);
                with.FormValue("password", password);
                with.Accept(new MediaRange("application/json"));
            });

            Assert.AreEqual(Nancy.HttpStatusCode.OK, result.StatusCode);
            var key = result.Body.DeserializeJson<SingleStringDto>().Value;

            return key;
        }
    }
}
