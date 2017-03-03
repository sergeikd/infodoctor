using Infodoctor.DAL.Interfaces;
using System;
using System.Linq;
using Infodoctor.Domain.Entities;

namespace Infodoctor.DAL.Repositories
{
    class OwnerShipsRepository : IOwnerShipsRepository
    {
        private readonly IAppDbContext _context;

        public OwnerShipsRepository(IAppDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context;
        }

        public void Add(OwnerShip ownerShip)
        {
            _context.OwnerShips.Add(ownerShip);
            _context.SaveChanges();
        }

        public void Update(OwnerShip ownerShip)
        {
            var edited = _context.OwnerShips.First(s => s.Id == ownerShip.Id);
            edited = ownerShip;
        }

        public void Delete(OwnerShip ownerShip)
        {
            _context.OwnerShips.Remove(ownerShip);
            _context.SaveChanges();
        }

        public IQueryable<OwnerShip> GetAllOwnerShip()
        {
            return _context.OwnerShips;
        }

        public OwnerShip GetOwnerShipById(int id)
        {
            return _context.OwnerShips.First(o => o.Id == id);
        }
    }
}
