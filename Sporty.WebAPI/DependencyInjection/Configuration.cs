using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Sporty.DAL.Infrastructure;
using Sporty.Services;

namespace Sporty.Web.DependencyInjection
{
    public class Configuration
    {
        public static void Configure()
        {
            // Autofac
            var builder = new ContainerBuilder();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            // Services
            builder.RegisterType<PlaceService>().As<IPlaceService>().InstancePerRequest();
            builder.RegisterType<FieldService>().As<IFieldService>().InstancePerRequest();
            builder.RegisterType<ScheduleOrderService>().As<IScheduleOrderService>().InstancePerRequest();
            // Controllers
            builder.RegisterControllers(Assembly.GetExecutingAssembly()); // Mvc
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()); // WebApi
            //


            IContainer container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container)); // Mvc
            GlobalConfiguration.Configuration.DependencyResolver
                = new AutofacWebApiDependencyResolver(container); // WebApi
        }
    }
}