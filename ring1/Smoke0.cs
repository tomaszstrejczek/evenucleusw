using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy;
using Nancy.Testing;


namespace ring1
{
    [TestClass]
    public class Smoke0Test
    {
        [TestMethod]
        public void TestMethod1()
        {
			var bootstrapper = new TestingBootstrapper();
			var browser = new Browser(bootstrapper);

            var result = browser.Post("http://localhost:8070/api/login", with => {
                with.HttpRequest();
                with.FormValue("email", "a@a.a");
                with.FormValue("password", "123");
            });

            // Then
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
