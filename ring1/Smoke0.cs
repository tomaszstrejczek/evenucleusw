using System;
using ts.api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy;
using Nancy.Testing;


namespace ring1
{
    [TestClass]
    public class Smoke0Test
    {
        [TestMethod]
        public void  NormalFlow()
        {
			var bootstrapper = new TestingBootstrapper();
			var browser = new Browser(bootstrapper);

            var result = browser.Post("http://localhost:8070/api/account/login", with => {
                with.HttpRequest();
                with.FormValue("email", "a@a.a");
                with.FormValue("password", "123");                
            });

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            var key = result.Body.AsString();
            Assert.IsTrue(key.Length > 0);
        }

        [TestMethod]
        public void ErrorFlow()
        {
            var bootstrapper = new TestingBootstrapper();
            var browser = new Browser(bootstrapper);

            var result = browser.Post("http://localhost:8070/api/account/login", with => {
                with.HttpRequest();
                with.FormValue("email", "b@a.a");
                with.FormValue("password", "123");
            });

            Assert.AreEqual(HttpStatusCode.Accepted, result.StatusCode);

            var error = result.Body.DeserializeJson<ErrorResponse.Error>();
            Assert.AreEqual(strings.InvalidUserPassword, error.ErrorMessage);
        }
    }
}
