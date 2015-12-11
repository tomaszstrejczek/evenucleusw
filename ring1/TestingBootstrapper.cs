using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eZet.EveLib.Core.Cache;
using ts.api;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Testing;
using Nancy.TinyIoc;
using Microsoft.Data.Entity;
using Serilog;
using ts.data;
using ts.shared;
using ts.services;

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
            container.Register<ICacheLocalProvider,CacheLocalProvider>();
            container.Register<ICharacterNameDict,CharacterNameDict>();
            container.Register<ICorporationRepo,CorporationRepo>();
            container.Register<IEveApi,EveApi>();
            container.Register<IJobRepo,JobRepo>();
            container.Register<IJobService,JobsService>();
            container.Register<IKeyInfoRepo,KeyInfoRepo>();
            container.Register<INotificationRepo,NotificationRepo>();
            container.Register<IPilotRepo,PilotRepo>();
            container.Register<IPilotService,PilotService>();
            container.Register<IRefTypeDict,RefTypeDict>();
            container.Register<ISkillRepo,SkillRepo>();
            container.Register<ITypeNameDict,TypeNameDict>();
            container.Register<IAccountService, AccountService>();
            container.Register<IKeyInfoService, ts.services.KeyInfoService>();
            container.Register<IEveLibCache, EveSqlServerCache>();

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
            pipelines.BeforeRequest.AddItemToEndOfPipeline((ctx, token) => ts.api.BeforeRequest.BeforeRequestHandler(accountRepo, ctx, token));

            base.RequestStartup(container, pipelines, context);
        }

        protected override IEnumerable<INancyModule> GetAllModules(TinyIoCContainer container)
        {
            return new INancyModule[]
            {
                new ServiceAccount(container.Resolve<IAccountService>()),
                new Pilots(),
                new ServiceKeyInfo(container.Resolve<IKeyInfoService>()),
            }.AsEnumerable();
        }
    }
}
