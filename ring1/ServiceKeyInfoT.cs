using System;
using ts.api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy;
using Nancy.Responses.Negotiation;
using Nancy.Testing;


namespace ring1
{
    [TestClass]
    public class ServiceKeyInfoT: ServiceTestBase
    {
        [TestMethod]
        public void  AddKey()
        {
            var skey = GetSKey();

            var result = Browser.Post("http://localhost:8070/api/keyinfo/add", with => {
                with.HttpRequest();
                with.Header("jwt", skey);
                with.FormValue("KeyId", "1");
                with.FormValue("VCode", "123");
                with.Accept(new MediaRange("application/json"));
            });

            Assert.AreEqual(Nancy.HttpStatusCode.OK, result.StatusCode);
            var keyinfoid = result.Body.AsString();

            result = Browser.Post("http://localhost:8070/api/keyinfo/delete", with => {
                with.HttpRequest();
                with.Header("jwt", skey);
                with.FormValue("KeyInfoId", keyinfoid);
                with.Accept(new MediaRange("application/json"));
            });

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
