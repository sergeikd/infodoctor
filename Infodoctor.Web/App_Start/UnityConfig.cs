using System;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Infodoctor.BL.Interfaces;
using Infodoctor.BL.Services;
using Infodoctor.DAL;
using Infodoctor.DAL.Interfaces;
using Infodoctor.DAL.Repositories;
using Unity.WebApi;
using Infodoctor.Domain.Entities;
using Infodoctor.Web.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Infodoctor.Web.Infrastructure;

namespace Infodoctor.Web.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            container.RegisterType<UserManager<ApplicationUser>>();
            container.RegisterType<ApplicationUserManager>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<AppDbContext>(new PerRequestLifetimeManager(), new InjectionConstructor());//for keep the same dbContext instance per request
            //container.RegisterType<AppDbContext>(new InjectionConstructor());
            container.RegisterType<ConfigService>(new InjectionConstructor());

            //register all your services and reps
            //container.RegisterType<IConfigService, ConfigService>();
            container.RegisterType<IMailService, MailService>();
            container.RegisterType<ICountryRepository, CountryRepository>();
            container.RegisterType<ICountryService, CountryService>();
            container.RegisterType<IClinicSpecializationRepository, ClinicSpecializationRepository>();
            container.RegisterType<IClinicSpecializationService, ClinicSpecializationService>();
            container.RegisterType<IClinicRepository, ClinicRepository>();
            container.RegisterType<IClinicService, ClinicService>();
            container.RegisterType<IArticleThemesRepository, ArticleThemesRepository>();
            container.RegisterType<IArticleThemesService, ArticleThemesService>();
            container.RegisterType<IImagesRepository, ImagesRepository>();
            container.RegisterType<IImagesService, ImagesService>();
            container.RegisterType<IArticlesRepository, ArticlesRepository>();
            container.RegisterType<IArticlesService, ArticlesService>();
            container.RegisterType<ICitiesRepository, CitiesRepository>();
            container.RegisterType<ICitiesService, CitiesService>();
            container.RegisterType<ISearchService, SearchService>();
            container.RegisterType<IClinicReviewRepository, ClinicReviewRepository>();
            container.RegisterType<IClinicReviewService, ClinicReviewService>();
            container.RegisterType<IDoctorSpecializationRepository, DoctorSpecializationRepository>();
            container.RegisterType<IDoctorSpecializationService, DoctorSpecializationService>();
            container.RegisterType<IDoctorCategoryRepository, DoctorCategoryRepository>();
            container.RegisterType<IDoctorCategoryService, DoctorCategoryService>();
            container.RegisterType<IDoctorRepository, DoctorRepository>();
            container.RegisterType<IDoctorService, DoctorService>();
            container.RegisterType<IDoctorReviewRepository, DoctorReviewRepository>();
            container.RegisterType<IDoctorReviewService, DoctorReviewService>();
            container.RegisterType<IArticleCommentsRepository, ArticleCommentsRepository>();
            container.RegisterType<IArticleCommentsService, ArticleCommentsService>();
            container.RegisterType<IResortRepository, ResortRepository>();
            container.RegisterType<IResortService, ResortService>();
            container.RegisterType<IResortReviewRepository, ResortReviewRepository>();
            container.RegisterType<IResortReviewService, ResortReviewService>();
            container.RegisterType<ITestService, TestService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

        }
    }
}
