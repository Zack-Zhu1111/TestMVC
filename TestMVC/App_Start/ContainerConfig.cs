using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using TestMVC.Data.Contexts;
using TestMVC.Provider;
using TestMVC.Data.Repositories;
using System.Web.Http;
using System.Web.Mvc;
using NLog;
using System.Reflection;

namespace TestMVC.App_Start
{
    public static class ContainerConfig
    {
        public static IContainer Container { get; set; }
        public static void RegisterComponents()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MedicineUsageContext>().As<IMedicineUsageContext>().InstancePerRequest();
            builder.RegisterType<UserLoginContext>().As<IUserLoginContext>().InstancePerRequest();
            builder.RegisterType<MedicineCategoryContext>().As<IMedicineCategoryContext>().InstancePerRequest();
            builder.RegisterType<InfoMedicineContext>().As<IInfoMedicineContext>().InstancePerRequest();
            builder.RegisterType<InfoAndCategoryContext>().As<IInfoAndCategoryContext>().InstancePerRequest();
            builder.RegisterType<InformationContext>().As<IInformationContext>().InstancePerRequest();

            //builder.RegisterType<MedicineUsageRepository>().As<IMedicineUsageRepository>().InstancePerRequest();
            //builder.RegisterType<UserLoginRepository>().As<IUserLoginRepository>().InstancePerRequest();
            //builder.RegisterType<MedicineCategoryRepository>().As<IMedicineCategoryRepository>().InstancePerRequest();
            //builder.RegisterType<InfoMedicineRepository>().As<IInfoMedicineRepository>().InstancePerRequest();

            builder.RegisterType<MedicineUsageProvider>().As<IMedicineUsageProvider>();
            builder.RegisterType<UserLoginProvider>().As<IUserLoginProvider>();
            builder.RegisterType<MedicineCategoryProvider>().As<IMedicineCategoryProvider>();
            builder.RegisterType<InfoMedicineProvider>().As<IInfoMedicineProvider>();
            builder.RegisterType<InfoAndCategoryProvider>().As<IInfoAndCategoryProvider>();
            builder.RegisterType<InformationProvider>().As<IInformationProvider>();

            builder.Register(x => LogManager.GetCurrentClassLogger()).As<ILogger>();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var config = GlobalConfiguration.Configuration;
            builder.RegisterWebApiFilterProvider(config);
            Container = builder.Build();

            var dependencyResolver = new AutofacDependencyResolver(Container);
            DependencyResolver.SetResolver(dependencyResolver);

            //for web api
            config.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
        }
    }
}