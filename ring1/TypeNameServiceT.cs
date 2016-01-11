using System;
using System.Collections.Generic;
using ts.api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy;
using Nancy.Responses.Negotiation;
using Nancy.Testing;
using ts.dto;


namespace ring1
{
    [TestClass]
    public class TypeNameServiceT: ServiceTestBase
    {
        [TestMethod]
        public void  ValidType()
        {
            var skey = GetSKey();

            var result = Browser.Get("http://localhost:8070/api/typeid/tengu", with => {
                with.HttpRequest();
                with.Header("jwt", skey);
                with.Accept(new MediaRange("application/json"));
            });

            Assert.AreEqual(Nancy.HttpStatusCode.OK, result.StatusCode);
            var typeid = result.Body.DeserializeJson<SingleLongDto>().Value;
            Assert.IsTrue(typeid > 0);
        }

        [TestMethod]
        public void InvalidType()
        {
            var skey = GetSKey();

            var result = Browser.Get("http://localhost:8070/api/typeid/alamakota", with => {
                with.HttpRequest();
                with.Header("jwt", skey);
                with.Accept(new MediaRange("application/json"));
            });

            Assert.AreEqual(Nancy.HttpStatusCode.OK, result.StatusCode);
            var typeid = result.Body.DeserializeJson<SingleLongDto>().Value;
            Assert.AreEqual(-1, typeid);
        }

    }
}
