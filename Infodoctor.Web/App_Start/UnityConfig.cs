﻿using System.Web.Http;
using Infodoctor.BL.Intefaces;
using Infodoctor.BL.Services;
using Infodoctor.DAL;
using Infodoctor.DAL.Interfaces;
using Infodoctor.DAL.Repositories;
using Microsoft.Practices.Unity;
using Unity.WebApi;
using Infodoctor.Domain;
using Infodoctor.Web.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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

            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            container.RegisterType<UserManager<ApplicationUser>>();
            container.RegisterType<ApplicationUserManager>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            //container.RegisterType<IAppDbContext, AppDbContext>();
            container.RegisterType<IAppDbContext, AppDbContext>(new HierarchicalLifetimeManager(), new InjectionConstructor());//for keep the same dbContext instance per request

            //register all your services and reps
            container.RegisterType<ICountryRepository, CountryRepository>();
            container.RegisterType<ICountryService, CountryService>();
            container.RegisterType<IClinicProfilesRepository, ClinicProfilesRepository>();
            container.RegisterType<IClinicProfilesService, ClinicProfilesService>();
            container.RegisterType<IСlinicRepository, СlinicRepository>();
            container.RegisterType<IClinicService, ClinicService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}