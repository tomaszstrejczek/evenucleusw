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

namespace ts.api
{
    public class ServiceKeyInfo: NancyModule
    {
        private readonly IKeyInfoRepo _keyInfoRepo;

        public ServiceKeyInfo(IKeyInfoRepo keyInfoRepo)
        {
            _keyInfoRepo = keyInfoRepo;
            Post["/api/keyinfo/add", runAsync:true] = async (parameters, ct) => 
                await Add(parameters);
            Post["/api/keyinfo/delete", runAsync: true] = async (parameters, ct) => await DeleteKey(parameters);
        }

        struct AddModel
        {
            public long KeyId;
            public string VCode;
        }

        private async Task<string> Add(dynamic parameters)
        {
            var m = this.Bind<AddModel>();

            return (await _keyInfoRepo.AddKey(1, m.KeyId, m.VCode)).ToString();
        }

        struct DeleteModel
        {
            public long KeyInfoId;
        }

        private async Task<Nancy.HttpStatusCode> DeleteKey(dynamic parameter)
        {
            var m = this.Bind<DeleteModel>();
            await _keyInfoRepo.DeleteKey(m.KeyInfoId);

            return Nancy.HttpStatusCode.OK;
        }
    }

}
