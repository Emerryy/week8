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
    public class TransferController : ControllerBase
    {
        private ITransferDAO transferDAO;
        

        [HttpGet]
        public ActionResult<List<Transfer>> SentTransfers()
        {
            //userId = int.Parse(User.FindFirst("sub").Value);
            return Ok(transferDAO.SentTransfers());
        }

        [HttpGet]

        public ActionResult<List<Transfer>> ReceivedTransfers()
        {
            return Ok(transferDAO.ReceivedTransfers());
        }


    }
}
