using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserDAO userDAO;

        public UsersController(IUserDAO userDAO)
        {
            this.userDAO = userDAO;
        }

        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            //userId = int.Parse(User.FindFirst("sub").Value);
            return Ok(userDAO.GetUsers());
        }
    }
}
