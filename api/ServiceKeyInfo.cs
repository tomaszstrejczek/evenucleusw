using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Routing;
using Serilog.Enrichers;
using ts.data;
using ts.dto;
using ts.services;

namespace ts.api
{
    public class ServiceKeyInfo: NancyModule
    {
        private readonly IKeyInfoService _keyInfoService;

        public ServiceKeyInfo(IKeyInfoService keyInfoService)
        {
            _keyInfoService = keyInfoService;
            Post["/keyinfo/add", runAsync:true] = async (_, ct) => await Add();
            Post["/keyinfo/delete", runAsync: true] = async (_, ct) => await DeleteKey();
            Get["/keyinfo", runAsync: true] = async (_, ct) => await GetAll();
        }

        struct AddModel
        {
            public long KeyId { get; set; }
            public string VCode { get; set; }
        }

        private async Task<string> Add()
        {
            var m = this.Bind<AddModel>();

            return (await _keyInfoService.Add((long) this.Context.Request.Session["UserId"], m.KeyId, m.VCode)).ToString();
        }

        struct DeleteModel
        {
            public long KeyInfoId { get; set; }
        }

        private async Task<Nancy.HttpStatusCode> DeleteKey()
        {
            var m = this.Bind<DeleteModel>();
            await _keyInfoService.Delete(m.KeyInfoId);

            return Nancy.HttpStatusCode.OK;
        }

        private async Task<List<KeyInfoDto>> GetAll()
        {
            var userid = (long) this.Context.Request.Session["UserId"];
            return await _keyInfoService.Get(userid);
        }
    }

}
