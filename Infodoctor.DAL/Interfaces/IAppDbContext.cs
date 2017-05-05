using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using Infodoctor.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Infodoctor.DAL.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Country> Countries { get; set; }
        DbSet<Clinic> Сlinics { get; set; }
        DbSet<ClinicPhone> ClinicPhones { get; set; }
        DbSet<CityAddress> CityAddresses { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<ClinicSpecialization> ClinicSpecializations { get; set; }
        DbSet<OwnerShip> OwnerShips { get; set; }
        DbSet<Article> Articles { get; set; }
        DbSet<ArticleComment> ArticleComments { get; set; }
        DbSet<ImageFile> Images { get; set; }
        DbSet<ClinicReview> ClinicReviews { get; set; }
        DbSet<DoctorReview> DoctorReviews { get; set; }
        DbSet<DoctorSpecialization> DoctorSpecializations { get; set; }
        DbSet<DoctorCategory> DoctorCategories { get; set; }
        DbSet<Doctor> Doctors { get; set; }
        DbSet<Resort> Resorts { get; set; }
        DbSet<ResortReview> ResortReviews { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
