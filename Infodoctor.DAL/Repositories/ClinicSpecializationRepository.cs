using Infodoctor.DAL.Interfaces;
using System;
using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Repositories
{
    public class ClinicSpecializationRepository: IClinicSpecializationRepository
    {
        private readonly AppDbContext _context;

        public ClinicSpecializationRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<ClinicSpecialization> GetAllClinicSpecializations()
        {
            throw new NotImplementedException();
            //return _context.ClinicSpecializations;
        }

        public ClinicSpecialization GetClinicSpecializationById(int id)
        {
            throw new NotImplementedException();
            // return _context.ClinicSpecializations.First(s => s.Id == id); ;
        }

        public void Add(ClinicSpecialization clinicSpecialization)
        {
            throw new NotImplementedException();
            // _context.ClinicSpecializations.Add(clinicSpecialization);
            // _context.SaveChanges();
        }

        public void Update(ClinicSpecialization clinicSpecialization)
        {
            throw new NotImplementedException();
            // var edited = _context.ClinicSpecializations.First(s => s.Id == clinicSpecialization.Id);
            // edited = clinicSpecialization;
            // _context.SaveChanges();
        }

        public void Delete(ClinicSpecialization clinicSpecialization)
        {
            throw new NotImplementedException();
            // _context.ClinicSpecializations.Remove(clinicSpecialization);
            // _context.SaveChanges();
        }
    }
}
