using Infodoctor.DAL.Interfaces;
using System;
using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Repositories
{
    public class СlinicRepository: IСlinicRepository
    {
        private readonly IAppDbContext _context;

        public СlinicRepository(IAppDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
        }

        public IQueryable<Clinic> GetAllСlinics()
        {
            return _context.Сlinics.OrderBy(n => n.Id);
        }

        public Clinic GetClinicById(int id)
        {
            try
            {
                var result = _context.Сlinics.First(s => s.Id == id);
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

        public void Update(Clinic clinic)
        {
            var edited = _context.Сlinics.First(s => s.Id == clinic.Id);
            edited = clinic;
        }

        public void Delete(Clinic clinic)
        {
            _context.Сlinics.Remove(clinic);
            _context.SaveChanges();
        }
    }
}
