using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy;
using Nancy.Responses.Negotiation;
using Nancy.Testing;
using ts.api;
using ts.domain;
using ts.dto;


namespace ring1
{
    [TestClass]
    public class AccountT
    {
        [TestMethod]
        public void  NormalFlow()
        {
			var bootstrapper = new TestingBootstrapper();
			var browser = new Browser(bootstrapper);

            var result = browser.Post("http://localhost:8070/account/register", with => {
                with.HttpRequest();
                with.FormValue("email", "a@a.a");
                with.FormValue("password", "123");
                with.Accept(new MediaRange("application/json"));
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

            var result = browser.Post("http://localhost:8070/account/login", with => {
                with.HttpRequest();
                with.FormValue("email", "b@a.a");
                with.FormValue("password", "123");
                with.Accept(new MediaRange("application/json"));
            });

            Assert.AreEqual(HttpStatusCode.Accepted, result.StatusCode);

            var error = result.Body.DeserializeJson<ts.dto.Error>();
            Assert.AreEqual(strings.InvalidUserPassword, error.ErrorMessage);
        }

        [TestMethod]
        public void WithAuthorization()
        {
            var bootstrapper = new TestingBootstrapper();
            var browser = new Browser(bootstrapper);

            var result = browser.Post("http://localhost:8070/account/register", with => {
                with.HttpRequest();
                with.FormValue("email", "a@a2.a");
                with.FormValue("password", "123");
                with.Accept(new MediaRange("application/json"));
            });

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            var skey = result.Body.DeserializeJson<SingleStringDto>().Value;

            result = browser.Get("http://localhost:8070/pilots/1", with => {
                with.Header("jwt", skey);
                with.Accept(new MediaRange("application/json"));
            });

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void FailedAuthorization1()
        {
            var bootstrapper = new TestingBootstrapper();
            var browser = new Browser(bootstrapper);

            var result = browser.Get("http://localhost:8070/pilots/1", with => {
                with.Header("jwt", "dummy");
                with.Accept(new MediaRange("application/json"));
            });

            Assert.AreEqual(HttpStatusCode.Accepted, result.StatusCode);
            var error = result.Body.DeserializeJson<ts.dto.Error>();
            Assert.AreEqual(strings.InvalidSessionKey, error.ErrorMessage);
        }

        [TestMethod]
        public void FailedAuthorization2()
        {
            var bootstrapper = new TestingBootstrapper();
            var browser = new Browser(bootstrapper);

            var result = browser.Get("http://localhost:8070/pilots/1", with => {
                with.Accept(new MediaRange("application/json"));
            });

            Assert.AreEqual(HttpStatusCode.Accepted, result.StatusCode);
            var error = result.Body.DeserializeJson<ts.dto.Error>();
            Assert.AreEqual(strings.InvalidSessionKey, error.ErrorMessage);
        }

    }
}
