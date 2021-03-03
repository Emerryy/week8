using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetInfoServer.DAL.Interfaces;
using PetInfoServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetInfoServer.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {

        private IOwnerDAO ownerDAO;

        public OwnerController(IOwnerDAO ownerDAO)
        {
            this.ownerDAO = ownerDAO;
        }

        [HttpGet]
        public ActionResult<List<Owner>> GetOwners()
        {
            List<Owner> owners = new List<Owner>();

            owners = ownerDAO.GetOwners();
            if (owners.Count > 0)
            {
                return Ok(owners);
            }
            else

            {
                return NoContent();
            }
        }
    }
}
