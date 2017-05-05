using System.Data.Entity;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Infodoctor.DAL
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>, IAppDbContext
    {
        public AppDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Clinic> Сlinics { get; set; }
        public DbSet<Phone> ClinicPhones { get; set; }
        public DbSet<CityAddress> CityAddresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ClinicSpecialization> ClinicSpecializations { get; set; }
        public DbSet<OwnerShip> OwnerShips { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleComment> ArticleComments { get; set; }
        public DbSet<ImageFile> Images { get; set; }
        public DbSet<ClinicReview> ClinicReviews { get; set; }
        public DbSet<DoctorReview> DoctorReviews { get; set; }
        public DbSet<DoctorSpecialization> DoctorSpecializations { get; set; }
        public DbSet<DoctorCategory> DoctorCategories { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Resort> Resorts { get; set; }
        public DbSet<ResortReview> ResortReviews { get; set; }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}
