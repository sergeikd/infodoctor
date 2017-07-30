using System;
using System.Linq;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;


namespace Infodoctor.DAL.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<Doctor> GetAllDoctors()
        {
            return _context.Doctors.OrderBy(n => n.Id);
        }

        public IQueryable<Doctor> GetSortedDoctors(string sortBy, bool descending, string lang)
        {
            switch (sortBy)
            {
                default:
                    return descending ? _context.Doctors.OrderByDescending(d => d.Localized.FirstOrDefault(l => l.Language.Code.ToLower() == lang.ToLower()).Name) : _context.Doctors.OrderBy(d => d.Localized.FirstOrDefault(l => l.Language.Code.ToLower() == lang.ToLower()).Name);
                case "alphabet":
                    {
                        return descending ? _context.Doctors.OrderByDescending(d => d.Localized.FirstOrDefault(l => l.Language.Code.ToLower() == lang.ToLower()).Name) : _context.Doctors.OrderBy(d => d.Localized.FirstOrDefault(l => l.Language.Code.ToLower() == lang.ToLower()).Name);
                    }
                case "rate":
                    return descending ? _context.Doctors.OrderByDescending(d => d.RateAverage) : _context.Doctors.OrderBy(d => d.RateAverage);
                case "prof":
                    return descending ? _context.Doctors.OrderByDescending(d => d.RateProfessionalism) : _context.Doctors.OrderBy(d => d.RateProfessionalism);
            }
        }

        public Doctor GetDoctorById(int id)
        {
            return _context.Doctors.First(d => d.Id == id);
        }

        public void Add(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }

        public void Update(Doctor doctor)
        {
            var updated = _context.Doctors.First(d => d.Id == doctor.Id);
            updated = doctor;
            _context.SaveChanges();
        }

        public void Delete(Doctor doctor)
        {
            _context.Doctors.Remove(doctor);
            _context.SaveChanges();
        }
    }
}
