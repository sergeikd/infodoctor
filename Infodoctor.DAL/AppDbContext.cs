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
        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}
