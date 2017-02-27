using System.Data.Entity;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain;
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
        public DbSet<ClinicPhone> ClinicPhones { get; set; }
        public DbSet<ClinicAddress> ClinicAddresses { get; set; }
        public DbSet<ClinicProfile> ClinicProfiles { get; set; }
        public DbSet<ClinicSpecialization> ClinicSpecializations { get; set; }
        public DbSet<OwnerShip> OwnerShips { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleTheme> ArticleThemes { get; set; }
        public DbSet<ImageFile> Images { get; set; }
        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}
