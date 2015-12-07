using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Bootstrapper;
using Nancy;
using Ninject;
using Serilog;
using ts.shared;

namespace ts.api
{
    public class Bootstrapper: Nancy.Bootstrappers.Ninject.NinjectNancyBootstrapper
    {
        protected override void ApplicationStartup(IKernel container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            StaticConfiguration.EnableRequestTracing = true;
        }

        protected override void ConfigureApplicationContainer(IKernel existingContainer)
        {
            // Perform registation that should have an application lifetime
            existingContainer.Bind<IAccountContextProvider>().To<AccountContextProvider>();
            existingContainer.Bind<IMyConfiguration>().To<MyConfiguration>();
            existingContainer.Bind<IAccountRepo>().To<AccountRepo>();
            existingContainer.Bind<ICacheLocalProvider>().To<CacheLocalProvider>();
            existingContainer.Bind<ICharacterNameDict>().To<CharacterNameDict>();
            existingContainer.Bind<ICorporationRepo>().To<CorporationRepo>();
            existingContainer.Bind<IEveApi>().To<EveApi>();
            existingContainer.Bind<IJobRepo>().To<JobRepo>();
            existingContainer.Bind<IJobService>().To<JobsService>();
            existingContainer.Bind<IKeyInfoRepo>().To<KeyInfoRepo>();
            existingContainer.Bind<INotificationRepo>().To<NotificationRepo>();
            existingContainer.Bind<IPilotRepo>().To<PilotRepo>();
            existingContainer.Bind<IPilotService>().To<PilotService>();
            existingContainer.Bind<IRefTypeDict>().To<RefTypeDict>();
            existingContainer.Bind<ISkillRepo>().To<SkillRepo>();
            existingContainer.Bind<ITypeNameDict>().To<TypeNameDict>();

            var config = new LoggerConfiguration();
#if DEBUG
            config.MinimumLevel.Debug().WriteTo.Trace();
#else
            config.MinimumLevel.Debug();
#endif
            var log = config.CreateLogger();
            existingContainer.Bind<ILogger>().ToConstant(log);
        }

        protected override void RequestStartup(IKernel container, IPipelines pipelines, NancyContext context)
        {
            pipelines.OnError.AddItemToEndOfPipeline((z, a) => ErrorResponse.FromException(a));

            var accountRepo = container.Get<IAccountRepo>();
            pipelines.BeforeRequest.AddItemToEndOfPipeline((ctx, token) => BeforeRequest.BeforeRequestHandler(accountRepo, ctx, token));

            base.RequestStartup(container, pipelines, context);
        }
    }
}
