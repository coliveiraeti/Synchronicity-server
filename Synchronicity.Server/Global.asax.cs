using Autofac;
using Autofac.Integration.SignalR;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.SignalR;
using Synchronicity.Server.Data.SQLite;
using Synchronicity.Server.Interfaces.Data;
using System.Reflection;
using System.Web.Http;

namespace Synchronicity.Server
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Autofac registration
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            ConfigureRepository(builder);
            var container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            GlobalHost.DependencyResolver = new AutofacDependencyResolver(container);
        }

        private void ConfigureRepository(ContainerBuilder builder, string provider = "SQLite")
        {
            builder.RegisterType<HistoryRepository>().As<IHistoryRepository>();
            builder.RegisterType<VirtualMachineRepository>().As<IVirtualMachineRepository>();
        }
    }
}
