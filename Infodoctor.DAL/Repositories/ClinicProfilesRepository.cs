using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain;
using System;
using System.Linq;

namespace Infodoctor.DAL.Repositories
{
    public class ClinicProfilesRepository: IClinicProfilesRepository
    {
        private readonly IAppDbContext _context;

        public ClinicProfilesRepository(IAppDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
        }

        public IQueryable<ClinicProfile> GetAllClinicProfiles()
        {
            return _context.ClinicProfiles;
        }

        public ClinicProfile GetClinicProfileById(int id)
        {
            return _context.ClinicProfiles.First(s => s.Id == id); ;
        }

        public void Add(ClinicProfile clinicProfile)
        {
            _context.ClinicProfiles.Add(clinicProfile);
            _context.SaveChanges();
        }

        public void Update(ClinicProfile clinicProfile)
        {
            var edited = _context.ClinicProfiles.First(s => s.Id == clinicProfile.Id);
            edited = clinicProfile;
            _context.SaveChanges();
        }

        public void Delete(ClinicProfile clinicProfile)
        {
            _context.ClinicProfiles.Remove(clinicProfile);
            _context.SaveChanges();
        }
    }
}
