using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using Infodoctor.Domain;

namespace Infodoctor.DAL.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Country> Countries { get; set; }
        DbSet<Clinic> Сlinics { get; set; }
        DbSet<ClinicPhone> ClinicPhones { get; set; }
        DbSet<ClinicAddress> ClinicAddresses { get; set; }
        DbSet<ClinicProfile> ClinicProfiles { get; set; }
        DbSet<ClinicSpecialization> ClinicSpecializations { get; set; }
        DbSet<OwnerShip> OwnerShips { get; set; }
        DbSet<Article> Articles { get; set; }
        //DbSet<ArticleTheme> ArticleThemes { get; set; }
        DbSet<ImageFile> Images { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
