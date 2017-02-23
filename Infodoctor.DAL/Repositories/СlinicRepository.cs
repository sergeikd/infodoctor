using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain;
using System;
using System.Linq;

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
            return _context.Сlinics;
        }

        public Clinic GetClinicById(int id)
        {
            return _context.Сlinics.First(s => s.Id == id); ;
        }

        public void Add(Clinic clinic)
        {
            _context.Сlinics.Add(clinic);
        }

        public void Update(Clinic clinic)
        {
            var edited = _context.Сlinics.First(s => s.Id == clinic.Id);
            edited = clinic;
        }

        public void Delete(Clinic clinic)
        {
            _context.Сlinics.Remove(clinic);
        }
    }
}
