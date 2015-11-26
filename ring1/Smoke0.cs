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

            var result = browser.Post("/api/login", with => {
                with.HttpRequest();
            });

            // Then
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
