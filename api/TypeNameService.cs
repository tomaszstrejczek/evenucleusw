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
    public class TypeNameService: NancyModule
    {
        private readonly ITypeNameDict _typeNameDict;

        public TypeNameService(ITypeNameDict typeNameDict)
        {
            _typeNameDict = typeNameDict;
            Get["/api/typeid/{name}", runAsync: true] = async (_, ct) => await GetId(_.name);
        }


        private async Task<SingleLongDto> GetId(string name)
        {
            var found = await _typeNameDict.GetByName(new string[] {name}.ToArray());
            return new SingleLongDto() {Value = found[0].Item1};
        }
    }

}
