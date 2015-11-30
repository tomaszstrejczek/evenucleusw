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
            pipelines.BeforeRequest.AddItemToStartOfPipeline((ctx, token) => BeforeRequest.BeforeRequestHandler(accountRepo, ctx, token));

            base.RequestStartup(container, pipelines, context);
        }
    }
}
