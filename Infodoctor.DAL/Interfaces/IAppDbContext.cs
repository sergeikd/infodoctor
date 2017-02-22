using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infodoctor.Domain;

namespace Infodoctor.DAL.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Country> Countries { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
