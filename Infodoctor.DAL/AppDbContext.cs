using System.Data.Entity;
using Infodoctor.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Infodoctor.DAL
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Clinic> Сlinics { get; set; }
        public DbSet<LocalizedClinic> LocalizedClinic { get; set; }

        public DbSet<Address> ClinicAddresses { get; set; }
        public DbSet<LocalizedAddress> LocalizedClinicAddress { get; set; }

        public DbSet<City> Cities { get; set; }
        public DbSet<LocalizedCity> LocalisedCity { get; set; }

        public DbSet<Phone> ClinicPhones { get; set; }
        public DbSet<LocalizedPhone> LocalizedClinicPhone { get; set; }

        public DbSet<ClinicReview> ClinicReviews { get; set; }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<LocalizedDoctor> LocalizedDoctors { get; set; }

        public DbSet<DoctorCategory> DoctorCategories { get; set; }
        public DbSet<LocalizedDoctorCategory> LocalizedDoctorCategories { get; set; }

        public DbSet<DoctorReview> DoctorReviews { get; set; }


        public DbSet<Resort> Resorts { get; set; }
        public DbSet<LocalizedResort> LocalizedResorts { get; set; }

        public DbSet<ResortAddress> ResortAddresses { get; set; }
        public DbSet<LocalizedResortAddress> LocalizedResortAddresses { get; set; }

        public DbSet<ResortPhone> ResortPhones { get; set; }
        public DbSet<LocalizedResortPhone> LocalizedResortPhones { get; set; }

        public DbSet<ResortReview> ResortReviews { get; set; }

        public DbSet<OwnerShip> OwnerShips { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<ArticleComment> ArticleComments { get; set; }

        public DbSet<ImageFile> Images { get; set; }


        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}
