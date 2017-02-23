using Infodoctor.BL.Intefaces;
using Infodoctor.DAL.Interfaces;
using Infodoctor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infodoctor.BL.Services
{
    public class OwnerShipsService : IOwnerShipsService
    {
        public readonly IOwnerShipsRepository _ownerShipsRepository;

        public OwnerShipsService(IOwnerShipsRepository ownerShipsRepository)
        {
            if (ownerShipsRepository == null)
            {
                throw new ArgumentNullException(nameof(ownerShipsRepository));
            }
            _ownerShipsRepository = ownerShipsRepository;
        }

        public IEnumerable<OwnerShip> GetAllOwnerShips()
        {
            return _ownerShipsRepository.GetAllOwnerShip().ToList();
        }

        public OwnerShip GetOwnerShipById(int id)
        {
            return _ownerShipsRepository.GetOwnerShipById(id);
        }

        public void Add(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            var os = new OwnerShip() { Name = name };
            _ownerShipsRepository.Add(os);
        }

        public void Update(int id, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            var os = _ownerShipsRepository.GetOwnerShipById(id);
            if (os != null)
            {
                os.Name = name;
                _ownerShipsRepository.Update(os);
            }
        }

        public void Delete(int id)
        {
            var os = _ownerShipsRepository.GetOwnerShipById(id);
            if (os != null)
                _ownerShipsRepository.Delete(os);
        }

    }
}
