using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    [Route("[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase   
    {
        private IOwnerDAO dao ;

        public OwnerController(IOwnerDAO dao)
        {
            this.dao = dao;
        }

        [HttpGet]
        public ActionResult<List<Owner>> GetOwners()
        {
            return Ok(dao.GetOwners());
        }
    }
}
