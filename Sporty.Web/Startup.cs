using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Autofac.Integration.Owin;
using Autofac;
using Sporty.DAL.Infrastructure;
using Autofac.Integration.Mvc;
using System.Reflection;
using Autofac.Integration.WebApi;
using System.Web.Mvc;
using System.Web.Http;
using Microsoft.Extensions.DependencyInjection;

[assembly: OwinStartup(typeof(Sporty.Web.Startup))]

namespace Sporty.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Models.Mapping.Configuration.Configure();
            DependencyInjection.Configuration.Configure();

            ConfigureAuth(app);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //
        }
    }
}
