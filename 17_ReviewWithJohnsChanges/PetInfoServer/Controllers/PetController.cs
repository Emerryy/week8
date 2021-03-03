using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetInfoClient.Models;
using PetInfoServer.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetInfoServer.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private IPetDAO petDAO;

        public PetController(IPetDAO petDAO)
        {
            this.petDAO = petDAO;
        }

        [HttpGet]
        public ActionResult<List<Pet>> GetPets()
        {
            return Ok(petDAO.GetPets());
        }

        [HttpPost]
        public ActionResult AddPet(Pet pet)
        {

            bool result = petDAO.AddPet(pet);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
