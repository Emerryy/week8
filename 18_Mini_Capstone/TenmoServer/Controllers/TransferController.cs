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

        public TransferController(ITransferDAO transferDAO)
        {
            this.transferDAO = transferDAO;
        }


        //[HttpGet]
        //public ActionResult<List<Transfer>> GetTransfers()
        //{
        //    //userId = int.Parse(User.FindFirst("sub").Value);
        //    return Ok(transferDAO.GetTransfers());
        //}

        [HttpPost]
        public ActionResult AddTransfer(Transfer transfer)
        {
            bool result = transferDAO.AddTransfer(transfer);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet/*("{userId}")*/]
        public ActionResult<List<JoinedTransfer>> GetTransfers()
        {
            return Ok(transferDAO.GetTransfers());
        }



    }
}
