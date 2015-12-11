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
    public class KeyInfoService: ServiceTestBase
    {
        [TestMethod]
        public void  AddKey()
        {
            var skey = GetSKey();

            var result = Browser.Post("http://localhost:8070/keyinfo/add", with => {
                with.HttpRequest();
                with.Header("jwt", skey);
                with.FormValue("KeyId", "3483492");
                with.FormValue("VCode", "ZwML01eU6aQUVIEC7gedCEaySiNxRTJxgWo2qoVnxd5duN4tt4CWgMuYMSVNWIUG");
                with.Accept(new MediaRange("application/json"));
            });

            Assert.AreEqual(Nancy.HttpStatusCode.OK, result.StatusCode);
            var keyinfoid = result.Body.AsString();

            result = Browser.Post("http://localhost:8070/keyinfo/delete", with => {
                with.HttpRequest();
                with.Header("jwt", skey);
                with.FormValue("KeyInfoId", keyinfoid);
                with.Accept(new MediaRange("application/json"));
            });

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void GetAll()
        {
            var skey = GetSKey();

            var result = Browser.Post("http://localhost:8070/keyinfo/add", with => {
                with.HttpRequest();
                with.Header("jwt", skey);
                with.FormValue("KeyId", "3483492");
                with.FormValue("VCode", "ZwML01eU6aQUVIEC7gedCEaySiNxRTJxgWo2qoVnxd5duN4tt4CWgMuYMSVNWIUG");
                with.Accept(new MediaRange("application/json"));
            });
            Assert.AreEqual(Nancy.HttpStatusCode.OK, result.StatusCode);
            var keyinfoid = result.Body.AsString();

            var pilots = Browser.Get("http://localhost:8070/keyinfo", with => {
                with.HttpRequest();
                with.Header("jwt", skey);
                with.Accept(new MediaRange("application/json"));
            });

            var info = pilots.Body.DeserializeJson<List<KeyInfoDto>>();
            Assert.AreEqual(1, info.Count);
            Assert.AreEqual(1, info[0].Pilots.Count);
            Assert.AreEqual("MicioGatto", info[0].Pilots[0].Name);
        }
    }
}
