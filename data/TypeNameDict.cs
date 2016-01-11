using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using eZet.EveLib.Core.Cache;
using eZet.EveLib.EveXmlModule;
using eZet.EveLib.EveXmlModule.Models.Misc;
using Microsoft.Data.Entity;
using Serilog;

using ts.domain;
using ts.staticcontent;

namespace ts.data
{
    public class TypeNameDict : ITypeNameDict
    {
        private readonly ILogger _logger;

        public TypeNameDict(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<List<Tuple<long, string>>> GetById(IEnumerable<long> ids)
        {
            _logger.Debug("{method} ids count: {count}", "TypeNameDict::GetById", ids.Count());

            return ids.Select(x => new Tuple<long, string>(x, TypeFromYaml.FromTypeId(x))).ToList();
        }

        public async Task<List<Tuple<long, string>>> GetByName(IEnumerable<string> ids)
        {
            _logger.Debug("{method} names count: {count}", "TypeNameDict::GetByName", ids.Count());

             return ids.Select(x => new Tuple<long, string>(TypeFromYaml.FromTypeName(x), x)).ToList();
        }
    }
}
