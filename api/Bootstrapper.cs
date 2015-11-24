using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Bootstrapper;
using Nancy;
using Ninject;

namespace api
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
        }

        protected override void RequestStartup(IKernel container, IPipelines pipelines, NancyContext context)
        {
            pipelines.OnError.AddItemToEndOfPipeline((z, a) =>
            {
                //log.Error("Unhandled error on request: " + context.Request.Url + " : " + a.Message, a);
                return ErrorResponse.FromException(a);
            });

            base.RequestStartup(container, pipelines, context);
        }
    }
}
