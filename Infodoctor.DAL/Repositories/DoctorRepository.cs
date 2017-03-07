using System;
using System.Linq;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain.Entities;


namespace Infodoctor.DAL.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly IAppDbContext _context;

        public DoctorRepository(IAppDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
        }

        public IQueryable<Doctor> GetAllDoctors()
        {
            return _context.Doctors;
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
