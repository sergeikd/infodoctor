﻿using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using Infodoctor.Domain;

namespace Infodoctor.DAL.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Country> Countries { get; set; }
        DbSet<Clinic> Сlinics { get; set; }
        DbSet<ClinicProfile> ClinicProfiles { get; set; }
        DbSet<OwnerShip> OwnerShips { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
