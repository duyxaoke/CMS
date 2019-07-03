using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CMS.Application;
using CMS.Domain.Interfaces.Repositories;
using CMS.Infra.Data.Context;
using CMS.Infra.Data.Repositories;
using CMS.Presentation.Filters;
using CMS.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using CMS.Application.CMS;
using CMS.Application.Application.ICMS;
using CMS.Application.Application;
using AutoMapper;

namespace CMS.Presentation
{
    public class AutofacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();
            //bạn sẽ khai báo các class và interface tương ứng ở đây
            builder.RegisterControllers(Assembly.GetExecutingAssembly()); //Register MVC Controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()); //Register WebApi Controllers
            // MVC - OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();
            builder.RegisterModule(new AutofacWebTypesModule());

            builder.RegisterType<SampleContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationRoleManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ClaimedActionsProvider>().AsSelf().InstancePerRequest();
            builder.Register(c => new UserStore<ApplicationUser>(c.Resolve<ApplicationDbContext>())).AsImplementedInterfaces().InstancePerRequest();
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();
            builder.Register(c => new IdentityFactoryOptions<ApplicationUserManager>
            {
                DataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("Application​")
            });
            builder.Register(c => new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            .As<IDbConnection>().InstancePerLifetimeScope();

            builder.RegisterType<MenuRepository>().As<IMenuRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MenuAppService>().As<IMenuAppService>().InstancePerDependency();
            builder.RegisterType<MenuInRoleRepository>().As<IMenuInRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MenuInRoleAppService>().As<IMenuInRoleAppService>().InstancePerDependency();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryAppService>().As<ICategoryAppService>().InstancePerDependency();
            builder.RegisterType<LanguageRepository>().As<ILanguageRepository>().InstancePerLifetimeScope();
            builder.RegisterType<LanguageAppService>().As<ILanguageAppService>().InstancePerDependency();
            builder.RegisterType<ContentRepository>().As<IContentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ContentAppService>().As<IContentAppService>().InstancePerDependency();

            #region CMS
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ConfigAppService>().As<IConfigAppService>().InstancePerDependency();
            builder.RegisterType<PartnerRepository>().As<IPartnerRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PartnerAppService>().As<IPartnerAppService>().InstancePerDependency();
            #endregion

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container); //Set the WebApi DependencyResolver

        }
    }
}
