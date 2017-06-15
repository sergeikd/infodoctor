using Infodoctor.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Repositories
{
    public class ClinicRepository : IClinicRepository
    {
        private readonly AppDbContext _context;

        public ClinicRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<Clinic> GetAllСlinics()
        {
            return _context.Сlinics.OrderBy(n => n.Id);
        }

        public IQueryable<Clinic> GetSortedСlinics(string sortBy, bool descending, string lang)
        {
            switch (sortBy)
            {
                default:
                    return descending ? _context.Сlinics.OrderByDescending(c => c.Localized.FirstOrDefault(lc => lc.Language.Code.ToLower() == lang.ToLower()).Name) : _context.Сlinics.OrderBy(c => c.Localized.FirstOrDefault(lc => lc.Language.Code.ToLower() == lang.ToLower()).Name);             //TODO: check it
                case "alphabet":
                    return descending ? _context.Сlinics.OrderByDescending(c => c.Localized.FirstOrDefault(lc => lc.Language.Code.ToLower() == lang.ToLower()).Name) : _context.Сlinics.OrderBy(c => c.Localized.FirstOrDefault(lc => lc.Language.Code.ToLower() == lang.ToLower()).Name);             //TODO: check it
                case "rate":
                    return descending ? _context.Сlinics.OrderByDescending(c => c.RateAverage) : _context.Сlinics.OrderBy(c => c.RateAverage);
                case "price":
                    return descending ? _context.Сlinics.OrderByDescending(c => c.RatePrice) : _context.Сlinics.OrderBy(c => c.RatePrice);
            }
        }
        public Clinic GetClinicById(int id)
        {
            try
            {
                var result = _context.Сlinics.FirstOrDefault(s => s.Id == id);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Add(Clinic clinic)
        {

            _context.Сlinics.Add(clinic);
            _context.SaveChanges();
        }

        public void AddMany(IEnumerable<Clinic> clinics)
        {
            _context.Сlinics.AddRange(clinics);
            _context.SaveChanges();
        }
        public void Update(Clinic clinic)
        {
            var edited = _context.Сlinics.FirstOrDefault(s => s.Id == clinic.Id);
            edited = clinic;
        }

        public void Delete(Clinic clinic)
        {
            _context.Сlinics.Remove(clinic);
            _context.SaveChanges();
        }
    }
}
