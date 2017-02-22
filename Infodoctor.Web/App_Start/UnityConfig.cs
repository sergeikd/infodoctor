using System.Web.Http;
using Infodoctor.BL.Intefaces;
using Infodoctor.BL.Services;
using Infodoctor.DAL;
using Infodoctor.DAL.Interfaces;
using Infodoctor.DAL.Repositories;
using Microsoft.Practices.Unity;
using Unity.WebApi;
using System.Data.Entity;

namespace Infodoctor.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            //container.RegisterType<IAppDbContext, AppDbContext>();
            container.RegisterType<IAppDbContext, AppDbContext>(new HierarchicalLifetimeManager(), new InjectionConstructor());//for keep the same instance dbContext per request
            container.RegisterType<ICountryRepository, CountryRepository>();
            container.RegisterType<ICountryService, CountryService>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}