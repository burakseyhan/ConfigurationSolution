using Autofac;
using Autofac.Integration.Mvc;
using Configuration.BL.Helper;
using Configuration.BL.Repository;
using Configuration.BL.Repository.Application;
using Configuration.DAL.Entity;
using ConfigurationUI.Helper;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ConfigurationUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<ConfigHelper>().As<IConfigHelper>();
            builder.RegisterType<ApplicationRepository>().As<IBaseRepository<ApplicationEntity>>();
            builder.RegisterType<ConfigurationRepository>().As<IBaseRepository<ConfigurationEntity>>();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            //log the error!
        
        }
    }
}
