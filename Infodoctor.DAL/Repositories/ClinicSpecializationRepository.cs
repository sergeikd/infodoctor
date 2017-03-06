using Infodoctor.DAL.Interfaces;
using System;
using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Repositories
{
    public class ClinicSpecializationRepository: IClinicSpecializationRepository
    {
        private readonly IAppDbContext _context;

        public ClinicSpecializationRepository(IAppDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
        }

        public IQueryable<ClinicSpecialization> GetAllClinicSpecializations()
        {
            return _context.ClinicSpecializations;
        }

        public ClinicSpecialization GetClinicSpecializationById(int id)
        {
            return _context.ClinicSpecializations.First(s => s.Id == id); ;
        }

        public void Add(ClinicSpecialization clinicSpecialization)
        {
            _context.ClinicSpecializations.Add(clinicSpecialization);
            _context.SaveChanges();
        }

        public void Update(ClinicSpecialization clinicSpecialization)
        {
            var edited = _context.ClinicSpecializations.First(s => s.Id == clinicSpecialization.Id);
            edited = clinicSpecialization;
            _context.SaveChanges();
        }

        public void Delete(ClinicSpecialization clinicSpecialization)
        {
            _context.ClinicSpecializations.Remove(clinicSpecialization);
            _context.SaveChanges();
        }
    }
}
