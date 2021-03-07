using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    //[Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountDAO accountDAO;

        public AccountController(IAccountDAO accountDAO)
        {
            this.accountDAO = accountDAO;
        }


        [HttpGet("{userId}")]
        public ActionResult<Account> GetAccount(int userId)
        {
            return Ok(accountDAO.GetAccount(userId));
        }


        [HttpPut("{userId}")]
        public ActionResult<Account> UpdateBalance(Account accountFrom)
        {
            Account result = accountDAO.UpdateBalance(accountFrom);
            return Ok(result);
        }



    }
}

