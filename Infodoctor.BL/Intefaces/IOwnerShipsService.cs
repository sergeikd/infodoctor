using Infodoctor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infodoctor.BL.Intefaces
{
    public interface IOwnerShipsService
    {
        IEnumerable<OwnerShip> GetAllOwnerShips();
        OwnerShip GetOwnerShipById(int id);
        void Add(string name);
        void Update(int id, string name);
        void Delete(int id);
    }
}
