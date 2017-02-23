using System;
using System.Collections.Generic;
using System.Web.Http;
using Infodoctor.BL.Intefaces;
using Infodoctor.Domain;

namespace Infodoctor.Web.Controllers
{
    [Authorize]
    public class OwnerShipsController : ApiController
    {
        private readonly IOwnerShipsService _ownerShipsService;

        public OwnerShipsController(IOwnerShipsService ownerShipsService)
        {
            if (ownerShipsService == null)
                throw new ArgumentNullException(nameof(ownerShipsService));
            _ownerShipsService = ownerShipsService;
        }

        // GET api/ownerships
        [AllowAnonymous]
        public IEnumerable<OwnerShip> Get()
        {
            return _ownerShipsService.GetAllOwnerShips();
        }

        // GET api/ownerships/5
        [AllowAnonymous]
        public OwnerShip Get(int id)
        {
            return _ownerShipsService.GetOwnerShipById(id);
        }

        // POST api/ownerships
        [Authorize(Roles = "admin, moder")]
        public void Post([FromBody]string value)
        {
            _ownerShipsService.Add(value);
        }

        // PUT api/ownerships/5
        [Authorize(Roles = "admin, moder")]
        public void Put(int id, [FromBody]string value)
        {
            _ownerShipsService.Update(id, value);
        }

        // DELETE api/ownerships/5
        [Authorize(Roles = "admin, moder")]
        public void Delete(int id)
        {
            _ownerShipsService.Delete(id);
        }
    }
}