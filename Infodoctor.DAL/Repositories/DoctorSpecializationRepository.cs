using System;
using System.Linq;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Repositories
{
    public class DoctorSpecializationRepository : IDoctorSpecializationRepository
    {
        private readonly AppDbContext _context;

        public DoctorSpecializationRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<DoctorSpecializationMultiLang> GetAllSpecializations()
        {
            return _context.DoctorSpecializations;
        }

        public DoctorSpecializationMultiLang GetSpecializationById(int id)
        {
            return _context.DoctorSpecializations.First(ds => ds.Id == id);
        }

        public void Add(DoctorSpecializationMultiLang specializationMultiLang)
        {
            _context.DoctorSpecializations.Add(specializationMultiLang);
            _context.SaveChanges();
        }

        public void Update(DoctorSpecializationMultiLang specializationMultiLang)
        {
            var updated = _context.DoctorSpecializations.First(ds => ds.Id == specializationMultiLang.Id);
            updated = specializationMultiLang;
            _context.SaveChanges();
        }

        public void Delete(DoctorSpecializationMultiLang specializationMultiLang)
        {
            _context.DoctorSpecializations.Remove(specializationMultiLang);
            _context.SaveChanges();
        }
    }
}
