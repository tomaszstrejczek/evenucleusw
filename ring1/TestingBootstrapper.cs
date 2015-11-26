using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api;
using Nancy;
using Nancy.Testing;
using Nancy.TinyIoc;

namespace ring1
{
    public class TestingBootstrapper: ConfigurableBootstrapper
    {
        protected override IEnumerable<INancyModule> GetAllModules(TinyIoCContainer container)
        {
            return new Account[] {new Account()}.AsEnumerable();
        }
    }
}
