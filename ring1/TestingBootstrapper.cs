using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ts.api;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Testing;
using Nancy.TinyIoc;
using Microsoft.Data.Entity;
using Serilog;
using ts.db;
using ts.shared;

namespace ring1
{
    public class TestingBootstrapper: ConfigurableBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            var config = new MyTestConfiguration();
            var context = new AccountContext(config);
            context.Database.EnsureCreated();

            base.ApplicationStartup(container, pipelines);
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            container.Register<IMyConfiguration, MyTestConfiguration>();
            container.Register<IAccountContextProvider, AccountContextProvider>();
            container.Register<IAccountRepo, AccountRepo>();

            var config = new LoggerConfiguration();
#if DEBUG
            config.MinimumLevel.Debug().WriteTo.Trace();
#else
            config.MinimumLevel.Debug();
#endif
            var log = config.CreateLogger();
            container.Register<ILogger>(log);
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            pipelines.OnError.AddItemToEndOfPipeline((z, a) => ErrorResponse.FromException(a));

            var accountRepo = container.Resolve<IAccountRepo>();
            pipelines.BeforeRequest.AddItemToStartOfPipeline((ctx, token) => ts.api.BeforeRequest.BeforeRequestHandler(accountRepo, ctx, token));

            base.RequestStartup(container, pipelines, context);
        }

        protected override IEnumerable<INancyModule> GetAllModules(TinyIoCContainer container)
        {
            return new INancyModule[]
            {
                new Account(container.Resolve<IAccountRepo>()),
                new Pilots(), 
            }.AsEnumerable();
        }
    }
}
