using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        

        public List<JoinedTransfer> GetTransfers();

        

        bool AddTransfer(Transfer transfer);

    }
}
