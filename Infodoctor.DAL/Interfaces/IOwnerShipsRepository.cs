using Infodoctor.Domain;
using System;
using System.Linq;

namespace Infodoctor.DAL.Interfaces
{
    public interface IOwnerShipsRepository
    {
        IQueryable<OwnerShip> GetAllOwnerShip();
        OwnerShip GetOwnerShipById(int id);
        void Add(OwnerShip ownerShip);
        void Update(OwnerShip ownerShip);
        void Delete(OwnerShip ownerShip);
    }
}
