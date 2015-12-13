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
    public class KeyInfoService: NancyModule
    {
        private readonly IKeyInfoService _keyInfoService;

        public KeyInfoService(IKeyInfoService keyInfoService)
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

        private async Task<SingleLongDto> Add()
        {
            var m = this.Bind<AddModel>();

            var val = await _keyInfoService.Add((long) this.Context.Request.Session["UserId"], m.KeyId, m.VCode);
            return new SingleLongDto() {Value = val};
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
