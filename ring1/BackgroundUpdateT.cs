using System;
using System.Collections.Generic;
using System.Linq;
using ts.api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy;
using Nancy.Responses.Negotiation;
using Nancy.Testing;
using ts.dto;


namespace ring1
{
    [TestClass]
    public class BackgroundUpdateT: ServiceTestBase
    {
        [TestMethod]
        public void  AddKey()
        {
            var skey = GetSKey();

            var result = Browser.Post("http://localhost:8070/keyinfo/add", with => {
                with.HttpRequest();
                with.Header("jwt", skey);
                with.FormValue("KeyId", "3645238");
                with.FormValue("VCode", "sLOD3pSHwuzKtml3inm59qvVWHiKA3rULJY7KRsuWmmHrZ0c8qAZlftLDQIHvxBq");
                with.Accept(new MediaRange("application/json"));
            });

            Assert.AreEqual(Nancy.HttpStatusCode.OK, result.StatusCode);
            var keyinfoid = result.Body.DeserializeJson<SingleLongDto>().Value;

            result = Browser.Post("http://localhost:8070/backgroundupdate", with => {
                with.HttpRequest();
                with.Header("jwt", skey);
                with.Accept(new MediaRange("application/json"));
            });
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            result = Browser.Get("http://localhost:8070/pilots", with => {
                with.HttpRequest();
                with.Header("jwt", skey);
                with.Accept(new MediaRange("application/json"));
            });
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            var pilots = result.Body.DeserializeJson<List<PilotDto>>();
            Assert.AreEqual(1, pilots.Count);
            Assert.AreEqual("MicioGatto", pilots[0].Name);
            Assert.IsTrue(pilots[0].SkillsInQueue.Count > 0);
            Assert.IsTrue(pilots[0].Skills.Count > 0);
            Assert.IsTrue(pilots[0].Skills.Any(x => x.SkillName == "Interceptors"));
            Assert.IsNotNull(pilots[0].CurrentTrainingNameAndLevel);
            Assert.IsTrue(pilots[0].CurrentTrainingNameAndLevel.Length > 0);
        }
    }
}
